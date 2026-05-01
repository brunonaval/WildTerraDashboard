using System;
using System.Collections.Generic;
using System.Linq;

namespace WildTerraDashboard
{
    public class BotTrainingMode
    {
        public bool IsAtivo { get; private set; }
        public TrainingModeConfig CurrentConfig { get; private set; } = new TrainingModeConfig();

        public void Start(TrainingModeConfig config)
        {
            CurrentConfig = config ?? new TrainingModeConfig();
            IsAtivo = true;
        }

        public void Stop()
        {
            IsAtivo = false;
        }

        public string BuildOnCommand()
        {
            var cfg = (CurrentConfig ?? new TrainingModeConfig()).CloneNormalized();

            string skillsEnabled = cfg.EnableSkills ? "1" : "0";
            string buffEnabled = cfg.EnableBuffItems ? "1" : "0";
            string recoveryEnabled = cfg.EnableRecovery ? "1" : "0";
            string autoAttackEnabled = cfg.EnableAutoAttack ? "1" : "0";
            string skillsPayload = NormalizeMultiLineToPayload(cfg.SkillsText);
            string buffPayload = NormalizeMultiLineToPayload(cfg.BuffItemsText);
            string recoveryPayload = NormalizeRecoveryToPayload(cfg.RecoveryItemsText);
            string autoAttackTarget = NormalizeSingleValue(cfg.AutoAttackTargetName);

            return string.Format(
                "TRAINING;ON;{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10}",
                skillsEnabled,
                buffEnabled,
                recoveryEnabled,
                Math.Max(0, cfg.BuffRefreshSeconds),
                ClampPercent(cfg.HpThreshold),
                ClampPercent(cfg.SpThreshold),
                skillsPayload,
                buffPayload,
                recoveryPayload,
                autoAttackEnabled,
                autoAttackTarget);
        }

        public string BuildOffCommand()
        {
            return "TRAINING;OFF";
        }

        private static string NormalizeMultiLineToPayload(string text)
        {
            return string.Join("~", TrainingModeConfig.NormalizeMultiLineList(text));
        }

        private static string NormalizeRecoveryToPayload(string text)
        {
            var list = new TrainingModeConfig { RecoveryItemsText = text }.GetRecoveryItems();
            return string.Join("~", list.Select(x => x.ToString()).ToArray());
        }

        private static string NormalizeSingleValue(string text)
        {
            return (text ?? "").Trim().Replace(";", "").Replace("~", " ");
        }

        private static int ClampPercent(int value)
        {
            if (value < 1) return 1;
            if (value > 100) return 100;
            return value;
        }
    }

    public class TrainingModeConfig
    {
        public bool EnableSkills { get; set; }
        public bool EnableBuffItems { get; set; }
        public bool EnableRecovery { get; set; }
        public bool EnableAutoAttack { get; set; }

        public string SkillsText { get; set; } = "";
        public string BuffItemsText { get; set; } = "";
        public string RecoveryItemsText { get; set; } = "";
        public string AutoAttackTargetName { get; set; } = "";

        public int BuffRefreshSeconds { get; set; } = 1;
        public int HpThreshold { get; set; } = 50;
        public int SpThreshold { get; set; } = 50;

        public List<string> GetSkillNames()
        {
            return NormalizeMultiLineList(SkillsText);
        }

        public List<string> GetBuffItemNames()
        {
            return NormalizeMultiLineList(BuffItemsText);
        }

        public List<TrainingRecoveryItem> GetRecoveryItems()
        {
            var list = new List<TrainingRecoveryItem>();
            foreach (var line in NormalizeMultiLineList(RecoveryItemsText))
            {
                int idx = line.IndexOf(':');
                if (idx <= 0 || idx >= line.Length - 1)
                    continue;

                string kind = (line.Substring(0, idx) ?? "").Trim().ToUpperInvariant();
                string itemName = (line.Substring(idx + 1) ?? "").Trim();
                if (string.IsNullOrWhiteSpace(itemName))
                    continue;

                if (kind == "HP" || kind == "SP")
                {
                    list.Add(new TrainingRecoveryItem
                    {
                        ResourceType = kind,
                        ItemName = itemName
                    });
                }
            }

            return list;
        }

        public bool Validate(out string error)
        {
            error = null;

            if (!EnableSkills && !EnableBuffItems && !EnableRecovery && !EnableAutoAttack)
            {
                error = Properties.Resources.TrainingModeValidateEnableAtLeastOneBlock;
                return false;
            }

            if (EnableSkills && GetSkillNames().Count == 0)
            {
                error = Properties.Resources.TrainingModeValidateSkillsBlockEmpty;
                return false;
            }

            if (EnableBuffItems && GetBuffItemNames().Count == 0)
            {
                error = Properties.Resources.TrainingModeValidateBuffItemsBlockEmpty;
                return false;
            }

            if (EnableRecovery && GetRecoveryItems().Count == 0)
            {
                error = Properties.Resources.TrainingModeValidateRecoveryItemsInvalidFormat;
                return false;
            }

            if (EnableAutoAttack && string.IsNullOrWhiteSpace((AutoAttackTargetName ?? "").Trim()))
            {
                error = Properties.Resources.TrainingModeValidateAutoAttackTargetEmpty;
                return false;
            }

            AutoAttackTargetName = NormalizeSingleValue(AutoAttackTargetName);
            BuffRefreshSeconds = Math.Max(0, BuffRefreshSeconds);
            HpThreshold = ClampPercent(HpThreshold);
            SpThreshold = ClampPercent(SpThreshold);
            return true;
        }

        public string BuildSummary()
        {
            string autoAtk = EnableAutoAttack ? $"on({NormalizeSingleValue(AutoAttackTargetName)})" : "off";
            return $"skills={GetSkillNames().Count}, buffs={GetBuffItemNames().Count}, recovery={GetRecoveryItems().Count}, autoatk={autoAtk}, hp<{HpThreshold}%, sp<{SpThreshold}%, refresh={BuffRefreshSeconds}s";
        }

        public TrainingModeConfig CloneNormalized()
        {
            var clone = new TrainingModeConfig
            {
                EnableSkills = EnableSkills,
                EnableBuffItems = EnableBuffItems,
                EnableRecovery = EnableRecovery,
                EnableAutoAttack = EnableAutoAttack,
                SkillsText = SkillsText ?? "",
                BuffItemsText = BuffItemsText ?? "",
                RecoveryItemsText = RecoveryItemsText ?? "",
                AutoAttackTargetName = NormalizeSingleValue(AutoAttackTargetName),
                BuffRefreshSeconds = Math.Max(0, BuffRefreshSeconds),
                HpThreshold = ClampPercent(HpThreshold),
                SpThreshold = ClampPercent(SpThreshold)
            };

            return clone;
        }

        public static List<string> NormalizeMultiLineList(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return new List<string>();

            return text
                .Replace("\r\n", "\n")
                .Replace('\r', '\n')
                .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => (x ?? "").Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        private static string NormalizeSingleValue(string text)
        {
            return (text ?? "").Trim().Replace(";", "").Replace("~", " ");
        }

        private static int ClampPercent(int value)
        {
            if (value < 1) return 1;
            if (value > 100) return 100;
            return value;
        }
    }

    public class TrainingRecoveryItem
    {
        public string ResourceType { get; set; } = ""; // HP | SP
        public string ItemName { get; set; } = "";

        public bool IsHp
        {
            get { return string.Equals(ResourceType, "HP", StringComparison.OrdinalIgnoreCase); }
        }

        public bool IsSp
        {
            get { return string.Equals(ResourceType, "SP", StringComparison.OrdinalIgnoreCase); }
        }

        public override string ToString()
        {
            return $"{ResourceType}:{ItemName}";
        }
    }
}
