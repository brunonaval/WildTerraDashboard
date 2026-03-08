using static System.Net.Mime.MediaTypeNames;
using System;
using System.Linq;

namespace WildTerraDashboard
{
    // Modo de treino de cura (independente do bot principal). Apenas monta comandos UDP.
    public class BotHealTrainer
    {
        public bool IsAtivo { get; private set; }

        public string WeaponName { get; set; } = "";
        public string TargetMode { get; set; } = "PET"; // PET | SELF | PLAYER_BY_NAME
        public int TargetRadius { get; set; } = 18;

        // Multilinhas -> payload com '~'
        public string SkillsText { get; set; } = "";
        public string TargetNamesText { get; set; } = "";

        public void Start() => IsAtivo = true;
        public void Stop() => IsAtivo = false;

        public string BuildOnCommand()
        {
            string weapon = (WeaponName ?? "").Trim();
            string mode = (TargetMode ?? "PET").Trim();
            int radius = Math.Max(1, TargetRadius);

            // Skills em ordem (prioridade fixa): uma por linha
            string skillsPayload = NormalizeMultiLineToPayload(SkillsText);
            string targetsPayload = NormalizeMultiLineToPayload(TargetNamesText);

            // Formato:
            // HEALTRAIN;ON;weapon;targetMode;radius;skills(~);targets(~)
            return $"HEALTRAIN;ON;{weapon};{mode};{radius};{skillsPayload};{targetsPayload}";
        }

        public string BuildOffCommand() => "HEALTRAIN;OFF";

        private static string NormalizeMultiLineToPayload(string text)
        {
                if (string.IsNullOrWhiteSpace(text)) return "";
                var parts = text
                    .Replace("\r\n", "\n")
                    .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => (x ?? "").Trim())
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToArray();
                return string.Join("~", parts);
        }
    }
}