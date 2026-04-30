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


        // Novo submodo: Follow Heal (ativo apenas com PLAYER_BY_NAME + checkbox)
        public bool FollowTopTargetEnabled { get; set; } = false;
        public string FollowSkillName { get; set; } = "";
        public int FollowTargetHpPct { get; set; } = 75;
        public decimal FollowDistance { get; set; } = 4.5m;
        public string SelfRecoveryItemsText { get; set; } = "";
        public int SelfRecoveryHpPct { get; set; } = 40;
        public int SelfRecoveryResumeHpPct { get; set; } = 55;


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


            int followEnabled = FollowTopTargetEnabled ? 1 : 0;
            string followSkill = (FollowSkillName ?? "").Trim();
            int followTargetHpPct = Math.Max(1, Math.Min(100, FollowTargetHpPct));
            decimal followDistance = FollowDistance < 0 ? 0 : FollowDistance;
            string followDistancePayload = followDistance.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string selfRecoveryItemsPayload = NormalizeMultiLineToPayload(SelfRecoveryItemsText);
            int selfRecoveryHpPct = Math.Max(1, Math.Min(100, SelfRecoveryHpPct));
            int selfRecoveryResumeHpPct = Math.Max(1, Math.Min(100, SelfRecoveryResumeHpPct));


            // Formato:
            // HEALTRAIN;ON;weapon;targetMode;radius;skills(~);targets(~);followEnabled;followSkill;followTargetHpPct;followDistance;selfRecoveryItems(~);selfRecoveryHpPct;selfRecoveryResumeHpPct
            return $"HEALTRAIN;ON;{weapon};{mode};{radius};{skillsPayload};{targetsPayload};{followEnabled};{followSkill};{followTargetHpPct};{followDistancePayload};{selfRecoveryItemsPayload};{selfRecoveryHpPct};{selfRecoveryResumeHpPct}";
        
            
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