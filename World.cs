using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OHVTrainer
{
    internal class World
    {
        public void ToggleGuardDogs()
        {
            var dogs = GameObject.FindObjectsOfType<OHVGuardDog>();
            foreach (var dog in dogs)
            {
                dog.gameObject.SetActive(!dog.gameObject.activeSelf);
            }
        }

        public void OpenAllLockpickableObjects()
        {
            var lockpickables = GameObject.FindObjectsOfType<OHVLockpickable>();

            foreach (var lockpickable in lockpickables)
            {
                lockpickable.Sucess();
            }
        }

        public void SaveGame()
        {
            OHVSaveLoadManager.I.SaveGame(false, OHVPlayerManager.Data.SAVED_AT.HOME);
        }
    }
}
