using System;
using BepInEx;
using BepInEx.Logging;
using NWH.VehiclePhysics2.Powertrain;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Whilefun.FPEKit;

namespace OHVTrainer;

[BepInPlugin("com.dewol2.ohvtrainer", "OHV Trainer", "0.0.1")]
[BepInProcess("OHV.exe")]
public class Plugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;

    private GameObject trainerWindow;
    private GameObject playerGameObject;

    private UI UIManager = new UI();
    private Player player = new Player();
    private EngineComponent nysaEngine;

    private TMPro.TMP_Text playerPositionText;
    private TMPro.TMP_Text nysaDetailsText;

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

    private void Update()
    {
        if (Keyboard.current.f5Key.wasPressedThisFrame)
        {
            if (trainerWindow != null)
            {
                trainerWindow.SetActive(!trainerWindow.activeSelf);

                Cursor.visible = !Cursor.visible;
                Cursor.lockState = trainerWindow.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
                player.TogglePlayerActions(FPEFirstPersonController.Instance, FPEMouseLook.I);
            }
        }

        if (Keyboard.current.f10Key.wasPressedThisFrame)
        {
            var nyysa = FindObjectOfType<NysaCarController>()._vehicleController.powertrain;
            nyysa.transmission.finalGearRatio = 1.2f;
            nysaEngine = nyysa.engine;
            nysaEngine.maxPower = 160;
            nysaEngine.maxRPM = 9000;
        }

        if (Keyboard.current.f11Key.wasPressedThisFrame)
        {
            nysaEngine.maxPower = 160;
            nysaEngine.maxRPM = 9000;
        }

        // Update Player Position Text
        if (playerGameObject != null)
        {
            playerPositionText.text = $"Position:\nX: {playerGameObject.transform.position.x}\nY: {playerGameObject.transform.position.y}\n Z: {playerGameObject.transform.position.z}";
        }
        
        if (nysaEngine != null)
        {
            nysaDetailsText.text =
                $"NYSA:\n" +
                $"IsRunning: {nysaEngine.IsRunning}\n" +
                $"Engine Type: {nysaEngine.engineType.ToString()}\n" +
                $"Power: {nysaEngine.generatedPower}\n\n" +
                $"MAX RPM: {nysaEngine.maxRPM}" +
                $"RPM: {nysaEngine.RPM}";
        } else
        {
            nysaDetailsText.text = "ERROR";
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (IsMainGameSceneLoaded(scene.name))
        {
            LoadAsset();
        }
    }

    private void LoadAsset()
    {
        AssetBundle bundle = AssetBundle.LoadFromFile(GetResourcesLocation() + "ohvtrainer.trainer");
        if (bundle ==  null)
        {
            Logger.LogError("Cannot load Trainer AssetBundle!");
            return;
        }

        GameObject prefab = bundle.LoadAsset<GameObject>("TrainerCanvas");

        if (prefab == null)
        {
            Logger.LogError("Cannot load \"TrainerCanvas\" from AssetBundle!");
            return;
        }

        trainerWindow = Instantiate(prefab);

        if (trainerWindow == null)
        {
            Logger.LogError("Cannot instantiate trainerWindow!");
        }

        trainerWindow.SetActive(false);

        // Setup Variables
        nysaDetailsText = UIManager.GetNysaDetailsText(trainerWindow);
        playerGameObject = GameObject.FindGameObjectWithTag("Player").gameObject;
        playerPositionText = UIManager.GetPlayerPositionText(trainerWindow);
        UIManager.GetMoneyInputField(trainerWindow).text = player.GetMoney().ToString();

        SetupAllListeners();

        prefab = null;
        bundle = null;
    }

    private void SetupAllListeners()
    {
        UIManager.GetInfiniteHealthToggle(trainerWindow).onValueChanged.AddListener(OnHealthToggleChanged);
        UIManager.GetInfiniteStaminaToggle(trainerWindow).onValueChanged.AddListener(OnStaminaToggleChanged);

        UIManager.GetMoneyButton(trainerWindow).onClick.AddListener(OnSetMoneyButtonPressed);
    }

    private void OnHealthToggleChanged(bool value)
    {
        player.SetHealthInfinite(value);
    }

    private void OnStaminaToggleChanged(bool value)
    {
        player.SetStaminaInfinite(value);
    }

    private void OnSetMoneyButtonPressed()
    {
        string moneyText = UIManager.GetMoneyInputField(trainerWindow).text;

        if (int.TryParse(moneyText, out int money))
        {
            player.SetMoney(money);
        }
    }

    private bool IsMainGameSceneLoaded(string name)
    {
        return name.Equals("MainGame");
    }

    private string GetResourcesLocation()
    {
        return Paths.PluginPath + "/Resources/OHVTrainer/";
    }
}
