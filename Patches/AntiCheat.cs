using HarmonyLib;

namespace TooManyPlayers.Patches
{
    [HarmonyPatch(typeof(GorillaNot), "SendReport")]
    public class AntiCheat
    {
        private static bool Prefix(string susReason, string susId, string susNick)
        {
            return false;
        }
    }
}
