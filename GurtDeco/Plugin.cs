using System;
using System.Threading.Tasks;
using BepInEx;
using GurtDeco.Tools;
using UnityEngine;

namespace GurtDeco
{
    [BepInPlugin(Constants.GUID, Constants.Name, Constants.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Instance { get; private set; }
        private GameObject _fellasPrefab;
        private bool _initialized;

        private void Start()
        {
            Instance = this;
            GorillaTagger.OnPlayerSpawned(OnPlayerSpawned);
        }

        private void OnPlayerSpawned()
        {
            if (_initialized) return;
            _initialized = true;
            _ = SetupElligurt();
        }

        private async Task SetupElligurt()
        {
            try
            {
                _fellasPrefab = await AssetLoader.LoadAsset<GameObject>("GurtDeco");
                if (_fellasPrefab == null)
                {
                    Debug.LogError("[Fellas] Failed to load the silly waving fella");
                    return;
                }

                var fellasInstance = Instantiate(_fellasPrefab);
                fellasInstance.SetActive(true);
                fellasInstance.transform.position = new Vector3(-65.1718f, 12.051f, - 79.7311f);
                fellasInstance.transform.rotation = Quaternion.Euler(355.7651f, 206.2997f, 359.3513f);
                fellasInstance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }

            catch (Exception ex)
            {
                Debug.LogError("[Fellas] Error setting up the siliest: " + ex);
            }
        }
    }
}