using System;

namespace Core
{
    public class PlayerData 
    {
        public PlayerStats Stats { get;}
        public float CurrentHealth { get;  set; }

        public PlayerData(PlayerConfig playerConfig)
        {
            Stats = new PlayerStats(playerConfig);
            CurrentHealth = playerConfig.StartingHealth;
        }
    }
}
