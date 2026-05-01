using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WildTerraDashboard
{
    public partial class Form1
    {
        private Button btnInspectPlayerRef;
        private TextBox txtInspectPlayerNameRef;

        private readonly StringBuilder _inspectBase64Buffer = new StringBuilder();
        private readonly string _inspectExportDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Exports", "PlayerInspect");

        private string _inspectRequestedName = "";
        private string _inspectResolvedName = "";

        private void InitializeInspectModule()
        {
            btnInspectPlayerRef = FindControl<Button>("btnInspectPlayer");
            txtInspectPlayerNameRef = FindControl<TextBox>("txtInspectPlayerName");

            if (btnInspectPlayerRef != null)
            {
                btnInspectPlayerRef.Click -= BtnInspectPlayer_Click;
                btnInspectPlayerRef.Click += BtnInspectPlayer_Click;
            }
        }

        private void BtnInspectPlayer_Click(object sender, EventArgs e)
        {
            string playerName = (txtInspectPlayerNameRef != null ? txtInspectPlayerNameRef.Text : "").Trim();
            if (string.IsNullOrWhiteSpace(playerName))
            {
                MessageBox.Show(Properties.Resources.Form1InspectMessageFillPlayerName);
                return;
            }

            _inspectBase64Buffer.Clear();
            _inspectRequestedName = playerName;
            _inspectResolvedName = playerName;

            if (btnInspectPlayerRef != null)
                btnInspectPlayerRef.Enabled = false;

            EnviarComandoJogo("INSPECT_PLAYER;" + SanitizeUdpValue(playerName));
            LogarMensagem(string.Format(
                Properties.Resources.Form1InspectLogRequestedFormat,
                playerName));
        }

        private void HandleInspectBegin(string[] partes)
        {
            _inspectBase64Buffer.Clear();
            _inspectRequestedName = partes.Length >= 2 ? partes[1] : _inspectRequestedName;
            _inspectResolvedName = partes.Length >= 3 ? partes[2] : _inspectRequestedName;

            LogarMensagem(string.Format(
                Properties.Resources.Form1InspectLogCollectingDataFormat,
                _inspectResolvedName));
        }

        private void HandleInspectChunk(string[] partes)
        {
            if (partes.Length >= 2)
                _inspectBase64Buffer.Append(partes[1]);
        }

        private void HandleInspectEnd(string[] partes)
        {
            try
            {
                string base64 = _inspectBase64Buffer.ToString();
                if (string.IsNullOrWhiteSpace(base64))
                {
                    LogarMensagem(Properties.Resources.Form1InspectLogNoDataReceived);
                    return;
                }

                string texto = Encoding.UTF8.GetString(Convert.FromBase64String(base64));

                Directory.CreateDirectory(_inspectExportDir);

                string resolvedName = partes.Length >= 3 ? partes[2] : _inspectResolvedName;
                if (string.IsNullOrWhiteSpace(resolvedName))
                    resolvedName = _inspectRequestedName;

                string fileName = $"{SanitizeFileName(resolvedName)}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                string fullPath = Path.Combine(_inspectExportDir, fileName);

                File.WriteAllText(fullPath, texto, Encoding.UTF8);

                LogarMensagem(string.Format(
                    Properties.Resources.Form1InspectLogFileSavedFormat,
                    fullPath));
            }
            catch (Exception ex)
            {
                LogarMensagem(string.Format(
                    Properties.Resources.Form1InspectLogSaveFailedFormat,
                    ex.Message));
                MessageBox.Show(
                    string.Format(Properties.Resources.Form1InspectMessageSaveFailedFormat, ex.Message),
                    Properties.Resources.Form1InspectMessageBoxTitle);
            }
            finally
            {
                _inspectBase64Buffer.Clear();
                if (btnInspectPlayerRef != null)
                    btnInspectPlayerRef.Enabled = true;
            }
        }

        private void HandleInspectError(string[] partes)
        {
            string erro = partes.Length >= 2
                ? string.Join(";", partes, 1, partes.Length - 1)
                : Properties.Resources.Form1InspectUnknownError;

            LogarMensagem(string.Format(
                Properties.Resources.Form1InspectLogErrorFormat,
                erro));
            MessageBox.Show(
                erro,
                Properties.Resources.Form1InspectMessageBoxTitle);

            _inspectBase64Buffer.Clear();

            if (btnInspectPlayerRef != null)
                btnInspectPlayerRef.Enabled = true;
        }

        private static string SanitizeFileName(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "PlayerInspect";

            foreach (char c in Path.GetInvalidFileNameChars())
                text = text.Replace(c, '_');

            return text.Trim();
        }

        private static string SanitizeUdpValue(string text)
        {
            return (text ?? "")
                .Replace(";", "")
                .Replace("\r", " ")
                .Replace("\n", " ")
                .Trim();
        }
    }
}
