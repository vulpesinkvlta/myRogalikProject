using System;

namespace Core
{
    public class PlayerData
    {
        public PlayerStats Stats { get;}
        public PlayerData(PlayerConfig playerConfig)
        {
            Stats = new PlayerStats(playerConfig);
        }
    }
}
