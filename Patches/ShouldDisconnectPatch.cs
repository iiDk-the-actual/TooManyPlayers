using HarmonyLib;

namespace TooManyPlayers.Patches
{
    [HarmonyPatch(typeof(GorillaNot), "ShouldDisconnectFromRoom")]
    public class NoShouldDisconnectFromRoom
    {
        private static bool Prefix() =>
            false;
    }
}