using System;
using System.IO;
using System.Globalization;

namespace WildTerraDashboard
{
    public class RouteRecorder
    {
        public bool IsGravando { get; private set; } = false;
        private string arquivoDestino;
        private Waypoint ultimoPontoGravado = null;

        // Configuração: Só grava se afastar X metros do último ponto
        private const float DISTANCIA_MINIMA = 3.0f;

        // Evento para avisar o Form (Log)
        public event Action<string> OnLog;

        public void IniciarGravacao(string caminhoArquivo)
        {
            try
            {
                arquivoDestino = caminhoArquivo;

                // Cria (ou zera) o arquivo e coloca um cabeçalho (opcional) ou deixa vazio
                File.WriteAllText(arquivoDestino, "");

                IsGravando = true;
                ultimoPontoGravado = null; // Reinicia

                OnLog?.Invoke($"REC: Gravação iniciada em: {Path.GetFileName(caminhoArquivo)}");
                OnLog?.Invoke("REC: Ande pelo jogo para traçar a rota...");
            }
            catch (Exception ex)
            {
                OnLog?.Invoke("REC Erro: " + ex.Message);
                IsGravando = false;
            }
        }

        public void PararGravacao()
        {
            IsGravando = false;
            OnLog?.Invoke("REC: Gravação finalizada e salva.");
        }

        public void ProcessarPosicao(float x, float z)
        {
            if (!IsGravando) return;

            // 1. Verifica se é o primeiro ponto ou se andou o suficiente
            bool deveGravar = false;

            if (ultimoPontoGravado == null)
            {
                deveGravar = true;
            }
            else
            {
                // Calcula distância do último ponto gravado
                float dx = x - ultimoPontoGravado.X;
                float dz = z - ultimoPontoGravado.Z;
                float dist = (float)Math.Sqrt(dx * dx + dz * dz);

                if (dist >= DISTANCIA_MINIMA)
                {
                    deveGravar = true;
                }
            }

            // 2. Se deve gravar, escreve no arquivo
            if (deveGravar)
            {
                SalvarPonto(x, z);
            }
        }

        private void SalvarPonto(float x, float z)
        {
            try
            {
                // Formato: 100.5;300.2
                string linha = $"{x.ToString(CultureInfo.InvariantCulture)};{z.ToString(CultureInfo.InvariantCulture)}{Environment.NewLine}";

                // AppendAllText é ótimo porque salva na hora (se o pc desligar, não perde nada)
                File.AppendAllText(arquivoDestino, linha);

                // Atualiza o último ponto
                ultimoPontoGravado = new Waypoint { X = x, Z = z };

                OnLog?.Invoke($"REC: Ponto adicionado ({x:F1}; {z:F1})");
            }
            catch (Exception ex)
            {
                OnLog?.Invoke("REC Erro ao salvar: " + ex.Message);
            }
        }
    }
}