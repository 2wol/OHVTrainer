using System;
using BepInEx;
using BepInEx.Logging;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OHVTrainer;

[BepInPlugin("com.dewol2.ohvtrainer", "OHV Trainer", "0.0.1")]
[BepInProcess("OHV.exe")]
public class Plugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;

    private UI UIManager;

    public delegate void OnWindowInstantiated(GameObject prefab);
    public static event OnWindowInstantiated onWindowInstantiated;

    private void Awake()
    {
        UIManager = new UI();

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
        UI.onError -= UI_OnError;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (IsMainGameSceneLoaded(scene.name))
        {
            UI.onAssetLoaded += UIManager_OnAssetLoaded;
            UI.onError += UI_OnError;
            UI.instantiateTrainer += UI_InstantiateTrainer;
            UIManager.LoadResources();
        }
    }

    private void UI_InstantiateTrainer(GameObject prefab)
    {
        CreateWindow(prefab);
    }

    private void UI_OnError(UI.ErrorType type, string message)
    {
        switch (type)
        {
            case UI.ErrorType.Success:
                Logger.LogInfo($"[OHVTrainer] {message}");
                break;
            case UI.ErrorType.Error:
                Logger.LogError($"[OHVTrainer] {message}");
                break;
            case UI.ErrorType.Message:
                Logger.LogWarning($"[OHVTrainer] {message}");
                break;
        }
    }

    private void UIManager_OnAssetLoaded(bool isSuccessful)
    {
        if (!isSuccessful)
        {
            Logger.LogError("[OHVTrainer] Cannot load AssetBundle!");
        }

        UI.onAssetLoaded -= UIManager_OnAssetLoaded;
    }

    private bool IsMainGameSceneLoaded(string name)
    {
        return name.Equals("MainGame");
    }

    private void CreateWindow(GameObject prefab)
    {
        Instantiate(prefab);
    }
}
