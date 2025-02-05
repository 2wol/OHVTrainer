using BepInEx;
using UnityEngine;

namespace OHVTrainer
{
    internal class UI
    {
        AssetBundle trainer;
        GameObject prefab;
        GameObject canvas;

        public delegate void OnAssetLoaded(bool isSuccessful);
        public static event OnAssetLoaded onAssetLoaded;

        public delegate void OnError(ErrorType type, string message);
        public static event OnError onError;

        public delegate void InstantiateTrainer(GameObject prefab);
        public static event InstantiateTrainer instantiateTrainer;

        public enum ErrorType
        {
            Message,
            Success,
            Error
        }

        // Load Asset Bundle (containing Canvas, Build in Unity3D) into the game.
        public void LoadResources()
        {
            trainer = AssetBundle.LoadFromFile(GetResourcesLocation() + "ohvtrainer.trainer");
            if (trainer == null) { onAssetLoaded?.Invoke(false); }
            else 
            { 
                onAssetLoaded?.Invoke(true);
                CreateMainWindow();
            }
        }

        // Create Trainer canvas in game world.
        public void CreateMainWindow()
        {
            prefab = trainer.LoadAsset<GameObject>("TrainerCanvas");

            if (prefab == null)
            {
                onError?.Invoke(ErrorType.Error, "Cannot Load Asset \"TrainerCanvas\"!");
                onError?.Invoke(ErrorType.Message, "Available Assets: ");
                int a = 1;
                foreach (var item in trainer.GetAllAssetNames())
                {
                    onError?.Invoke(ErrorType.Message, $"{a}. {item}");
                    a++;
                }
                return;
            }

            Plugin.onWindowInstantiated += Plugin_OnWindowInstantiated;
            instantiateTrainer?.Invoke(prefab);
        }

        private void Plugin_OnWindowInstantiated(GameObject prefab)
        {
            canvas = prefab;
            onError?.Invoke(ErrorType.Success, "Window Successfuly Created!");
        }

        private string GetResourcesLocation()
        {
            return Paths.PluginPath + "/Resources/OHVTrainer/";
        }
    }
}
