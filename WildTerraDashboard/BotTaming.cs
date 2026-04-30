using System;

namespace WildTerraDashboard
{
    public class BotTaming
    {
        private readonly BotMovement movimento;

        public bool IsAtivo { get; private set; }
        public TamingSettings Config { get; }

        public event Action<string> OnLog;

        public BotTaming(BotMovement movimento, TamingSettings config = null)
        {
            if (movimento == null) throw new ArgumentNullException(nameof(movimento));

            this.movimento = movimento;
            Config = config ?? new TamingSettings();
        }

        public bool Iniciar()
        {
            if (!movimento.HasRoute)
            {
                OnLog?.Invoke("[TAMING] Nenhuma rota carregada. Use o btnLoadRoute antes de iniciar.");
                return false;
            }

            if (IsAtivo) return true;

            movimento.Iniciar();
            if (!movimento.IsRodando) return false;

            IsAtivo = true;
            OnLog?.Invoke($"[TAMING] Iniciado com {movimento.RouteCount} pontos na rota.");
            return true;
        }

        public void Parar()
        {
            if (!IsAtivo && !movimento.IsRodando) return;

            IsAtivo = false;
            movimento.Parar();
            OnLog?.Invoke("[TAMING] Parado.");
        }

        public string ProcessarTick(PlayerStats stats)
        {
            if (!IsAtivo || stats == null) return null;

            return movimento.ProcessarLogica(stats.X, stats.Z, stats.SP);
        }
    }
}
