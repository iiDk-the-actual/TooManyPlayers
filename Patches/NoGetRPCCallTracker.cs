using HarmonyLib;

namespace TooManyPlayers.Patches
{
    [HarmonyPatch(typeof(GorillaNot), "GetRPCCallTracker")]
    internal class NoGetRPCCallTracker
    {
        private static bool Prefix() =>
            false;
    }
}
