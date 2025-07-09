using HarmonyLib;
using UnityEngine;

namespace TooManyPlayers.Patches
{
    [HarmonyPatch(typeof(GorillaNot), "LogErrorCount")]
    public class NoLogErrorCount
    {
        private static bool Prefix(string logString, string stackTrace, LogType type) =>
            false;
    }
}
