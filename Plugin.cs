using System;
using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.Mono;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OHVTrainer;

[BepInPlugin("com.dewol2.ohvtrainer", "OHV Trainer", "0.0.1")]
[BepInProcess("OHV.exe")]
public class Plugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;

    AssetBundle trainer;

    private void Awake()
    {
        Logger = base.Logger;
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene name, LoadSceneMode mode)
    {
        if (IsMainGameSceneLoaded())
        {
            LoadResources();
        }
    }

    private bool IsMainGameSceneLoaded()
    {
        return SceneManager.GetActiveScene().name == "MainGame";
    }

    private void LoadResources()
    {
        trainer = AssetBundle.LoadFromFile(GetResourcesLocation() + "TrainerRes.unity");
        if (trainer == null) { Logger.LogError("Cannot Load OHVTrainer Resources!"); }
    }

    private string GetResourcesLocation()
    {
        return Paths.PluginPath + "/Resources/OHVTrainer/";
    }
}
