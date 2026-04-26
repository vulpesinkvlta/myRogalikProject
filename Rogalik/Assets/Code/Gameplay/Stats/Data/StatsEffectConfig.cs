using Core;
using UnityEngine;
[CreateAssetMenu(fileName = "StatsEffectConfig", menuName = "GamePlayData/Effects/StatsEffectConfig", order = 1)]
public class StatsEffectConfig : EffectConfig
{
    public StatModifier[] Modifiers;
    public override void Apply(PlayerStats playerStats)
    {
        playerStats.AddModifiers(Modifiers);
    }

    public override void Remove(PlayerStats playerStats)
    {
        playerStats.RemoveModifiers(Modifiers);
    }
}

