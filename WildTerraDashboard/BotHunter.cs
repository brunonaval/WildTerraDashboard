using System;
using System.Collections.Generic;
using System.Linq;

namespace WildTerraDashboard
{
    public class BotHunter
    {
        // Distância máxima (em metros) para engajar um alvo de caça.
        // Importante para o bot não "parar" tentando caçar um alvo muito longe enquanto deveria seguir a rota.
        private const float DIST_MAX_ENGAGE = 18f;

        public bool IsAtivo { get; set; } = false;
        private List<string> listaAlvos = new List<string>();

        // Cooldown para não ficar trocando de alvo igual louco
        private DateTime ultimoComando = DateTime.MinValue;

        public event Action<string> OnLog;

        public void DefinirLista(string textoBruto)
        {
            listaAlvos.Clear();
            if (string.IsNullOrEmpty(textoBruto)) return;

            string[] linhas = textoBruto.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var linha in linhas)
            {
                string limpo = linha.Trim();
                if (limpo.Length > 0) listaAlvos.Add(limpo);
            }
        }

        // [CORREÇÃO] Removi "minhaX" e "minhaZ" dos argumentos
        public string VerificarRadar(List<RadarEntity> entidades)
        {
            if (!IsAtivo) return null;
            if (listaAlvos.Count == 0) return null;
            if ((DateTime.Now - ultimoComando).TotalMilliseconds < 700) return null; // Evita spam

            RadarEntity melhorAlvo = null;
            float menorDistancia = 9999f;

            foreach (var ent in entidades)
            {
                // Filtra apenas Mobs (Tipo "M")
                if (ent.Tipo != "M") continue;

                // Verifica se o nome contém algo da nossa lista
                bool ehAlvo = false;
                foreach (var nomeCaça in listaAlvos)
                {
                    if (ent.Nome.IndexOf(nomeCaça, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        ehAlvo = true;
                        break;
                    }
                }

                if (!ehAlvo) continue;

                // [CORREÇÃO] Usamos a propriedade direta da sua classe RadarEntity
                float dist = ent.Distancia;

                // Lógica de Prioridade: Perto < Longe
                // Ignora mobs muito longe (> 60m)
                if (dist < menorDistancia && dist <= DIST_MAX_ENGAGE)
                {
                    menorDistancia = dist;
                    melhorAlvo = ent;
                }
            }

            if (melhorAlvo != null)
            {
                ultimoComando = DateTime.Now;
                // Manda comando especial: HUNT + Nome exato do mob
                return $"HUNT;{melhorAlvo.Nome}";
            }

            return null;
        }
    }
}