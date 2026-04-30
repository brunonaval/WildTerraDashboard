using System.Collections.Generic;

namespace WildTerraDashboard
{
    public class TamingSettings
    {
        public int TimerIntervalMs { get; set; } = 150;
        public string Mode { get; set; } = "Pacifico";
        public string TrapName { get; set; } = string.Empty;
        public List<string> TargetNames { get; set; } = new List<string>();
    }
}
