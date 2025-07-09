using HarmonyLib;

namespace TooManyPlayers.Patches
{
    [HarmonyPatch(typeof(GorillaNot), "DispatchReport")]
    public class NoDispatchReport
    {
        private static bool Prefix() =>
            false;
    }
}
