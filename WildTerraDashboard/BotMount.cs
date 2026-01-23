using System;

namespace WildTerraDashboard
{
    public class BotMount
    {
        public bool IsAtivo { get; set; } = false;
        public float DistanciaMinima { get; set; } = 15.0f; // Padrão 15 metros

        public event Action<string> OnLog;

        public void AtualizarConfig(bool ativo)
        {
            if (IsAtivo != ativo)
            {
                IsAtivo = ativo;
                OnLog?.Invoke($"[MONTARIA] Sistema {(IsAtivo ? "Ativado" : "Desativado")}. Distância mín: {DistanciaMinima}m.");
            }
        }
    }
}