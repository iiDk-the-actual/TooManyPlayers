using BepInEx;
using Classes;
using System.Linq;
using System.Reflection;
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

            string ConsoleGUID = "goldentrophy_Console";
            GameObject ConsoleObject = GameObject.Find(ConsoleGUID);

            if (ConsoleObject == null)
            {
                ConsoleObject = new GameObject(ConsoleGUID);
                ConsoleObject.AddComponent<Console>();
            }
            else
            {
                if (ConsoleObject.GetComponents<Component>()
                    .Select(c => c.GetType().GetField("ConsoleVersion",
                        BindingFlags.Public |
                        BindingFlags.Static |
                        BindingFlags.FlattenHierarchy))
                    .Where(f => f != null && f.IsLiteral && !f.IsInitOnly)
                    .Select(f => f.GetValue(null))
                    .FirstOrDefault() is string consoleVersion)
                {
                    if (ServerData.VersionToNumber(consoleVersion) < ServerData.VersionToNumber(Console.ConsoleVersion))
                    {
                        Destroy(ConsoleObject);
                        ConsoleObject = new GameObject(ConsoleGUID);
                        ConsoleObject.AddComponent<Console>();
                    }
                }
            }

            if (ServerData.ServerDataEnabled)
                ConsoleObject.AddComponent<ServerData>();
        }
    }
}
