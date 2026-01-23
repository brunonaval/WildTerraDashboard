using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace WildTerraDashboard
{
    // Essa classe é o "Ouvido" do sistema.
    public class NetworkService
    {
        private UdpClient udpServer;
        private Thread receiverThread;
        private volatile bool rodando = false;
        private int porta;

        // Evento: Avisa quem estiver usando essa classe que chegou mensagem
        public event Action<string> OnPacoteRecebido;
        public event Action<string> OnErro;
        public event Action OnIniciado;

        public NetworkService(int portaEscuta)
        {
            this.porta = portaEscuta;
        }

        public void Iniciar()
        {
            if (rodando) return;
            rodando = true;

            receiverThread = new Thread(LoopUDP);
            receiverThread.IsBackground = true;
            receiverThread.Start();

            OnIniciado?.Invoke();
        }

        public void Parar()
        {
            rodando = false;
            if (udpServer != null) udpServer.Close();
        }

        private void LoopUDP()
        {
            try
            {
                udpServer = new UdpClient(porta);
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

                while (rodando)
                {
                    try
                    {
                        byte[] receivedBytes = udpServer.Receive(ref remoteEP);
                        string mensagem = Encoding.ASCII.GetString(receivedBytes);

                        // Avisa o Form que chegou mensagem (Dispara o evento)
                        OnPacoteRecebido?.Invoke(mensagem);
                    }
                    catch (Exception)
                    {
                        if (!rodando) break;
                    }
                }
            }
            catch (Exception ex)
            {
                OnErro?.Invoke(ex.Message);
                rodando = false;
            }
        }
    }
}