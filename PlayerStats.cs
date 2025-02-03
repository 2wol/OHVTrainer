namespace OHVTrainer
{
    internal class PlayerStats
    {
        public void AddMoney(int amount)
        {
            OHVEconomyManager.instance.AddCash(amount);
        }

        public void RemoveMoney(int amount)
        {
            OHVEconomyManager.instance.RemoveCash(amount);
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
    }
}
