using HarmonyLib;
using GorillaNetworking;

namespace TooManyPlayers.Patches
{
    [HarmonyPatch(typeof(CosmeticsV2Spawner_Dirty))]
    [HarmonyPatch("_Step4_PopulateAllArrays")]
    internal class _Step4_PopulateAllArrays
    {
        private static bool Prefix(CosmeticsV2Spawner_Dirty __instance)
        {
            return false;
        }
    }

    [HarmonyPatch(typeof(CosmeticsV2Spawner_Dirty))]
    [HarmonyPatch("_Step5_InitializeVRRigsAndCosmeticsControllerFinalize")]
    internal class _Step5_InitializeVRRigsAndCosmeticsControllerFinalize
    {
        private static bool Prefix(CosmeticsV2Spawner_Dirty __instance)
        {
            return false;
        }
    }

    [HarmonyPatch(typeof(CosmeticsV2Spawner_Dirty))]
    [HarmonyPatch("StartInstantiatingPrefabs")]
    internal class StartInstantiatingPrefabs
    {
        private static bool Prefix(CosmeticsV2Spawner_Dirty __instance)
        {
            return false;
        }
    }

    [HarmonyPatch(typeof(CosmeticsController))]
    [HarmonyPatch("InitializeCosmeticStands")]
    internal class InitializeCosmeticStands
    {
        private static bool Prefix(CosmeticsController __instance)
        {
            return false;
        }
    }

    [HarmonyPatch(typeof(CosmeticsController))]
    [HarmonyPatch("UpdateWardrobeModelsAndButtons")]
    internal class UpdateWardrobeModelsAndButtons
    {
        private static bool Prefix(CosmeticsController __instance)
        {
            return false;
        }
    }
}
