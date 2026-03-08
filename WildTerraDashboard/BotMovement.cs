using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace WildTerraDashboard
{
    public class BotMovement
    {
        private List<Waypoint> rota = new List<Waypoint>();
        private int indiceAtual = 0;
        public bool IsRodando { get; private set; } = false;

        // --- NOVA VARIÁVEL DE CONTROLE ---
        public bool IsEmCombate { get; set; } = false;

        public event Action<string> OnLog;

        // --- CONFIGURAÇÕES ---
        private const float DISTANCIA_TROCA_ALVO = 3.0f; // Fluidez
        private const float DISTANCIA_TRAVADO = 1.0f;    // Se mover menos que isso...
        private const int LIMITE_TENTATIVAS = 20;        // ...por 20 ticks (aprox 3 segundos)...


        // --- NOVO (anti-waypoint bloqueado / território) ---
        // Detecta falta de PROGRESSO em direção ao destino (mesmo que o player esteja "andando em círculos").
        private const float PROGRESSO_MIN_DELTA = 0.35f;  // precisa reduzir a distância ao destino pelo menos isso
        private const int LIMITE_SEM_PROGRESSO = 60;      // 60 ticks ~ 9s (timer 150ms)
        private const float DISTANCIA_MIN_PARA_CHECAR_PROGRESSO = 6.0f; // evita falso positivo perto do ponto



        // --- VARIÁVEIS DE CONTROLE ---
        private int contadorTravado = 0;
        private Waypoint ultimaPosicaoConhecida = null;


        // --- NOVO: estado de progresso até o destino atual ---
        private int indiceRefProgresso = -1;
        private float melhorDistAoDestino = float.MaxValue;
        private int contadorSemProgresso = 0;
        private int pulosConsecutivos = 0;
        private const int MAX_PULOS_CONSECUTIVOS = 10; // proteção: evita loop infinito se a rota inteira estiver bloqueada

        public void Iniciar()
        {
            if (rota.Count > 0)
            {
                IsRodando = true;
                contadorTravado = 0;
                ultimaPosicaoConhecida = null;
                IsEmCombate = false; // Reseta estado de combate ao iniciar

                // progresso
                indiceRefProgresso = -1;
                melhorDistAoDestino = float.MaxValue;
                contadorSemProgresso = 0;
                pulosConsecutivos = 0;



                OnLog?.Invoke("Bot Iniciado (Com Sistema Anti-Travamento).");
            }
            else
            {
                OnLog?.Invoke("Erro: Rota vazia.");
            }
        }

        public void Parar()
        {
            IsRodando = false;
            OnLog?.Invoke("Bot Parado.");
        }

        public string ObterPontoRetomada(float xAtual, float zAtual, float distMinima)
        {
            if (rota == null || rota.Count == 0) return null;

            float menorDistanciaValida = 99999f;
            Waypoint melhorPonto = null;

            foreach (var ponto in rota)
            {
                float dx = ponto.X - xAtual;
                float dz = ponto.Z - zAtual;
                float dist = (float)Math.Sqrt(dx * dx + dz * dz);

                // Só aceita pontos longe o suficiente (para sair da base)
                if (dist > distMinima)
                {
                    // Pega o mais próximo dentre os que estão longe
                    if (dist < menorDistanciaValida)
                    {
                        menorDistanciaValida = dist;
                        melhorPonto = ponto;
                    }
                }
            }

            if (melhorPonto != null)
            {
                // Atualiza o índice atual para o ponto encontrado para a rota continuar de lá
                indiceAtual = rota.IndexOf(melhorPonto);
                return $"MOVE;{melhorPonto.X.ToString(CultureInfo.InvariantCulture)};{melhorPonto.Z.ToString(CultureInfo.InvariantCulture)}";
            }

            return null;
        }

        public void CarregarRota(string caminho)
        {
            try
            {
                rota.Clear();
                string[] linhas = File.ReadAllLines(caminho);
                foreach (string linha in linhas)
                {
                    string[] p = linha.Split(';');
                    if (p.Length >= 2)
                    {
                        float x = float.Parse(p[0].Replace(',', '.'), CultureInfo.InvariantCulture);
                        float z = float.Parse(p[1].Replace(',', '.'), CultureInfo.InvariantCulture);
                        rota.Add(new Waypoint { X = x, Z = z });
                    }
                }
                indiceAtual = 0;
                OnLog?.Invoke($"Rota: {rota.Count} pontos carregados.");
            }
            catch (Exception ex)
            {
                OnLog?.Invoke("Erro: " + ex.Message);
            }
        }

        public string ProcessarLogica(float meuX, float meuZ, int meuSP)
        {
            if (!IsRodando || rota.Count == 0) return null;

            // 0. CHECAGEM DE COMBATE (NOVO - PRIORIDADE MÁXIMA)
            if (IsEmCombate)
            {
                // Se estamos lutando, NÃO estamos travados.
                // Resetamos o contador para que o Unstuck não dispare.
                contadorTravado = 0;

                // Atualizamos a posição conhecida para que, quando o combate acabar,
                // ele não ache que travou porque ficou parado no mesmo lugar.
                ultimaPosicaoConhecida = new Waypoint { X = meuX, Z = meuZ };

                // Retorna null para NÃO enviar comando de andar.
                // A DLL (UDPRunner) é quem controla o movimento durante o combate.
                return null;
            }

            // 1. CHECAGEM DE STAMINA (Descanso)
            if (meuSP < 10)
            {
                // Se estamos descansando, resetamos o contador de travamento
                contadorTravado = 0;
                return null;
            }

            // 2. SISTEMA UNSTUCK (Anti-Travamento)
            VerificarSeEstaTravado(meuX, meuZ);

            // 3. MOVIMENTO NORMAL
            Waypoint destino = rota[indiceAtual];

            float dx = destino.X - meuX;
            float dz = destino.Z - meuZ;
            float distancia = (float)Math.Sqrt(dx * dx + dz * dz);

            // Troca de alvo antecipada (Fluidez)
            if (distancia < DISTANCIA_TROCA_ALVO)
            {
                AvancarProximoPonto();
                destino = rota[indiceAtual]; // Atualiza alvo imediatamente


                // reset de progresso ao trocar
                indiceRefProgresso = -1;
                melhorDistAoDestino = float.MaxValue;
                contadorSemProgresso = 0;
                pulosConsecutivos = 0;

            }
            else
            {

                // Se ficou preso tentando entrar em território/área bloqueada, a distância NÃO diminui de verdade.
                // Esse check pula o waypoint quando não há progresso por tempo suficiente.
                if (VerificarSemProgresso(distancia))
                {
                    destino = rota[indiceAtual];
                }


            }

            return $"MOVE;{destino.X.ToString(CultureInfo.InvariantCulture)};{destino.Z.ToString(CultureInfo.InvariantCulture)}";
        }

        private bool VerificarSemProgresso(float distanciaAtual)
        {
            // reset quando troca de waypoint
            if (indiceRefProgresso != indiceAtual)
            {
                indiceRefProgresso = indiceAtual;
                melhorDistAoDestino = distanciaAtual;
                contadorSemProgresso = 0;
                pulosConsecutivos = 0;
                return false;
            }

            // só checa se ainda estamos relativamente longe do ponto
            if (distanciaAtual < DISTANCIA_MIN_PARA_CHECAR_PROGRESSO) return false;

            // progrediu o suficiente?
            if (distanciaAtual <= (melhorDistAoDestino - PROGRESSO_MIN_DELTA))
            {
                melhorDistAoDestino = distanciaAtual;
                contadorSemProgresso = 0;
                pulosConsecutivos = 0;
                return false;
            }

            contadorSemProgresso++;
            if (contadorSemProgresso > LIMITE_SEM_PROGRESSO)
            {
                OnLog?.Invoke($"[UNSTUCK-PROGRESSO] Sem progresso até o ponto {indiceAtual + 1} (dist={distanciaAtual:0.0}, best={melhorDistAoDestino:0.0}). Pulando waypoint (possível território bloqueado).");
                AvancarProximoPonto();
                indiceRefProgresso = -1;
                melhorDistAoDestino = float.MaxValue;
                contadorSemProgresso = 0;
                pulosConsecutivos++;

                if (pulosConsecutivos >= MAX_PULOS_CONSECUTIVOS)
                {
                    OnLog?.Invoke("[UNSTUCK] Muitos pulos consecutivos (rota possivelmente inválida). Parando bot por segurança.");
                    Parar();
                }

                return true;
            }

            return false;
        }




        private void VerificarSeEstaTravado(float x, float z)
        {
            if (ultimaPosicaoConhecida == null)
            {
                ultimaPosicaoConhecida = new Waypoint { X = x, Z = z };
                return;
            }

            // Calcula quanto andou desde a última checagem
            float dx = x - ultimaPosicaoConhecida.X;
            float dz = z - ultimaPosicaoConhecida.Z;
            float dist = (float)Math.Sqrt(dx * dx + dz * dz);

            // Se andou muito pouco, aumenta o contador de "suspeita"
            if (dist < DISTANCIA_TRAVADO)
            {
                contadorTravado++;

                // Se o contador estourar o limite (aprox 3 segundos travado no mesmo metro)
                if (contadorTravado > LIMITE_TENTATIVAS)
                {
                    OnLog?.Invoke($"[UNSTUCK] Travado! Pulando o ponto {indiceAtual + 1}.");
                    AvancarProximoPonto();

                    // Reseta
                    contadorTravado = 0;
                }
            }
            else
            {
                // Se andou bem, zera o contador e atualiza a posição de referência
                contadorTravado = 0;
                ultimaPosicaoConhecida = new Waypoint { X = x, Z = z };
            }
        }

        private void AvancarProximoPonto()
        {
            indiceAtual++;
            if (indiceAtual >= rota.Count) indiceAtual = 0;
        }
    }
}