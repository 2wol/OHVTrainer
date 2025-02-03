using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.Mono;

namespace OHVTrainer;

[BepInPlugin("com.dewol2.ohvtrainer", "OHV Trainer", "0.0.1")]
[BepInProcess("OHV.exe")]
public class Plugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;

    private void Awake()
    {
        // Plugin startup logic
        Logger = base.Logger;
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
    }
}
