using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WildTerraDashboard
{
    internal static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            int portaEscuta = 0;
            int portaEnvio = 0;
            bool possuiArgumentosValidos = false;

            if (args != null && args.Length >= 2)
            {
                int listenArg;
                int sendArg;

                if (int.TryParse(args[0], out listenArg) && int.TryParse(args[1], out sendArg)
                    && listenArg > 0 && sendArg > 0)
                {
                    portaEscuta = listenArg;
                    portaEnvio = sendArg;
                    possuiArgumentosValidos = true;
                }
            }

            if (possuiArgumentosValidos)
                Application.Run(new Form1(portaEscuta, portaEnvio));
            else
                Application.Run(new Form1());
        }

    }
}
