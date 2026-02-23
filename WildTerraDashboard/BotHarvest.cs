using System;
using System.Collections.Generic;

namespace WildTerraDashboard
{
    public class BotHarvest
    {
        public List<string> ItensParaColetar { get; private set; } = new List<string>();
        public bool IsAtivo { get; set; } = false;

        // Lista Negra: Itens que tentamos pegar e falharam (ex: Sem Picareta)
        // Eles ficam aqui e o bot ignora.
        private HashSet<string> Blacklist = new HashSet<string>();

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

            OnLog?.Invoke($"Lista de Coleta atualizada: {ItensParaColetar.Count} itens.");
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
            // Se já está ignorado, não faz nada
            if (Blacklist.Contains(nomeItem)) return;

            Blacklist.Add(nomeItem);
            OnLog?.Invoke($"[ALERTA] '{nomeItem}' adicionado à Lista Negra (Sem ferramenta). Ignorando...");
        }

        public string VerificarRadar(List<RadarEntity> radarEntities)
        {
            if (!IsAtivo || ItensParaColetar.Count == 0) return null;

            foreach (var entity in radarEntities)
            {
                //if (entity.Tipo != "R" && entity.Tipo != "D") continue; // Não tratar mobs (M) como colheita; mobs são tratados pela caça.
                if (entity.Tipo != "R") continue;

                foreach (string desejo in ItensParaColetar)
                {
                    // SE ESTIVER NA BLACKLIST, PULA!
                    if (Blacklist.Contains(desejo)) continue;

                    if (entity.Nome.IndexOf(desejo, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        if (entity.Distancia <= 40)
                        {
                            // Encontrou um válido que não está na blacklist
                            return $"HARVEST;{desejo}";
                        }
                    }
                }
            }

            return null;
        }

        // Adicione este método na classe BotHarvest
        public void LimparBlacklist()
        {
            if (Blacklist.Count > 0)
            {
                Blacklist.Clear();
                OnLog?.Invoke("[SISTEMA] Novos itens detectados! Lista Negra limpa. Tentando coletar novamente...");
            }
        }
    }
}