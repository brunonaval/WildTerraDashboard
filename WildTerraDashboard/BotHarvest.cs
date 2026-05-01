using System;
using System.Collections.Generic;

namespace WildTerraDashboard
{
    public class HarvestDecision
    {
        public string DesiredName { get; set; }
        public RadarEntity Entity { get; set; }
    }

    public class BotHarvest
    {
        public List<string> ItensParaColetar { get; private set; } = new List<string>();
        public bool IsAtivo { get; set; } = false;

        // Lista Negra: Itens que tentamos pegar e falharam (ex: Sem Picareta)
        // Eles ficam aqui e o bot ignora.
        private HashSet<string> Blacklist = new HashSet<string>();
        private readonly Dictionary<int, DateTime> _worldIdBlacklistUntil = new Dictionary<int, DateTime>();
        private static readonly TimeSpan WorldIdBlacklistTtl = TimeSpan.FromSeconds(90);

        public event Action<string> OnLog;

        public void DefinirLista(string textoMultilinha)
        {
            ItensParaColetar.Clear();
            string[] linhas = textoMultilinha.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in linhas)
            {
                if (!string.IsNullOrWhiteSpace(item)) ItensParaColetar.Add(item.Trim());
            }

            // Quando muda a lista, limpamos a blacklist para dar uma nova chance
            Blacklist.Clear();
            _worldIdBlacklistUntil.Clear();

            OnLog?.Invoke(string.Format(
                Properties.Resources.BotHarvestLogListUpdatedFormat,
                ItensParaColetar.Count));
        }


        // Utilitário para logs/diagnóstico: conta quantos itens válidos existem no texto da lista
        // (mesma regra de parsing da AtualizarLista, porém sem alterar o estado interno).
        public int ContarItensLista(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return 0;
            int count = 0;
            foreach (var linha in texto.Split('\n'))
            {
                string item = linha.Trim();
                if (!string.IsNullOrWhiteSpace(item)) count++;



            }
            return count;

        }


        public void AdicionarBlacklist(string nomeItem)
        {
            if (Blacklist.Contains(nomeItem)) return;

            Blacklist.Add(nomeItem);
            OnLog?.Invoke(string.Format(
                Properties.Resources.BotHarvestLogItemBlacklistedNoToolFormat,
                nomeItem));
        }

        public void AdicionarBlacklistWorldId(int worldId, TimeSpan? ttl = null, string reason = null)
        {
            if (worldId <= 0) return;
            _worldIdBlacklistUntil[worldId] = DateTime.Now + (ttl ?? WorldIdBlacklistTtl);
            string suffix = string.IsNullOrWhiteSpace(reason)
                ? ""
                : string.Format(Properties.Resources.BotHarvestLogWorldIdCooldownReasonSuffixFormat, reason);
            OnLog?.Invoke(string.Format(
                Properties.Resources.BotHarvestLogWorldIdCooldownFormat,
                worldId,
                suffix));
        }

        private bool IsWorldIdBlacklisted(int worldId)
        {
            if (worldId <= 0) return false;
            if (_worldIdBlacklistUntil.TryGetValue(worldId, out DateTime until))
            {
                if (until > DateTime.Now) return true;
                _worldIdBlacklistUntil.Remove(worldId);
            }
            return false;
        }

        public HarvestDecision VerificarRadar(List<RadarEntity> radarEntities)
        {
            if (!IsAtivo || ItensParaColetar.Count == 0) return null;

            foreach (var entity in radarEntities)
            {
                if (entity.Tipo != "R") continue;
                if (IsWorldIdBlacklisted(entity.WorldId)) continue;

                foreach (string desejo in ItensParaColetar)
                {
                    if (Blacklist.Contains(desejo)) continue;

                    if (entity.Nome.IndexOf(desejo, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        if (entity.Distancia <= 40)
                        {
                            return new HarvestDecision
                            {
                                DesiredName = desejo,
                                Entity = entity
                            };
                        }
                    }
                }
            }

            return null;
        }

        // Adicione este método na classe BotHarvest
        public void LimparBlacklist()
        {
            if (Blacklist.Count > 0 || _worldIdBlacklistUntil.Count > 0)
            {
                Blacklist.Clear();
                _worldIdBlacklistUntil.Clear();
                OnLog?.Invoke(Properties.Resources.BotHarvestLogBlacklistClearedNewItemsDetected);
            }
        }
    }
}
