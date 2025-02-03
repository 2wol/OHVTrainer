using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OHVTrainer
{
    internal class Objects
    {
        public void ScrewScrews()
        {
            Screw(true);
        }

        public void UnscrewScrew()
        {
            Screw(false);
        }

        private void Screw(bool isScrewed)
        {
            var screws = GameObject.FindObjectsOfType<OHVScrew>();
            foreach (var screw in screws)
            {
                if (isScrewed) screw.ScrewOn();
                else screw.ScrewOff();
            }
        }

        public void InfiniteLockpicksUsage()
        {
            var lockpicks = GameObject.FindObjectsOfType<OHVLockpick>();

            foreach (var lockpick in lockpicks)
            {
                lockpick.data.remainingUses = 1000000;
            }
        }
    }
}
