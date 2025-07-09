using BepInEx;
using UnityEngine;

namespace TooManyPlayers
{
    //[BepInDependency("org.iidk.gorillatag.iimenu")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        void Awake()
        {
            HarmonyPatches.ApplyHarmonyPatches();
            GorillaTagger.OnPlayerSpawned(OnSpawn);
        }

        void OnSpawn()
        {
            GameObject Loader = new GameObject("TooManyPlayers");
            Loader.AddComponent<Main>();

            string ConsoleGUID = $"goldentrophy_Console_{Classes.Console.ConsoleVersion}";
            GameObject ConsoleObject = GameObject.Find(ConsoleGUID);

            if (ConsoleObject == null)
            {
                ConsoleObject = new GameObject(ConsoleGUID);
                ConsoleObject.AddComponent<Classes.CoroutineManager>();
                ConsoleObject.AddComponent<Classes.ServerData>();
                ConsoleObject.AddComponent<Classes.Console>();
            }

            DontDestroyOnLoad(Loader);
        }
    }
}
