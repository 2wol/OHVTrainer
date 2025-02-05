using System.Numerics;
using Whilefun.FPEKit;

namespace OHVTrainer
{
    internal class Player
    {
        public int GetMoney()
        {
            return OHVEconomyManager.instance.data.cash;
        }

        public void SetMoney(int amount)
        {
            OHVEconomyManager.instance.data.cash = amount;
            UpdateMoneyUI(amount);
        }

        public void AddMoney(int amount)
        {
            OHVEconomyManager.instance.AddCash(amount);
            UpdateMoneyUI(amount);
        }

        public void RemoveMoney(int amount)
        {
            OHVEconomyManager.instance.RemoveCash(amount);
            UpdateMoneyUI(amount);
        }

        private void UpdateMoneyUI(int cash)
        {
            OHVEconomyManager.instance.UI.SetCash(cash);
        }

        public void SetPlayerWeight(float weight)
        {
            OHVPlayerManagerPhysique.I.data.weight = weight;
        }

        public void SetHealthInfinite(bool infinite)
        {
            OHVPlayerManager.I.isImmortal = infinite;
        }

        public void SetStaminaInfinite(bool infinite)
        {
            OHVPlayerManagerPhysique.I.infiniteStamina = infinite;
        }

        public void TogglePlayerActions(FPEFirstPersonController controller, FPEMouseLook mouse)
        {
            controller.playerFrozen = !controller.playerFrozen;
            mouse.enableMouseLook = !mouse.enableMouseLook;
        }
    }
}
