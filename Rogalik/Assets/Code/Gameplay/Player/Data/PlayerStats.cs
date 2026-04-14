using System;
using System.Collections.Generic;

namespace Core
{
    public class PlayerStats
    {
        private Dictionary<StatType, float> _baseStats = new();
        private Dictionary<StatType, float> _additiveModifiers = new();
        private Dictionary<StatType, float> _multiplicativeModifiers = new();

        public PlayerStats(PlayerConfig playerConfig)
        {
            _baseStats[StatType.MaxHP] = playerConfig.MaxHealth;
            _baseStats[StatType.Damage] = playerConfig.BaseDamage;
            _baseStats[StatType.MoveSpeed] = playerConfig.MoveSpeed;
            _baseStats[StatType.AttackRange] = playerConfig.AttackRange;
            _baseStats[StatType.AttackSpeed] = playerConfig.AttackSpeed;
        }

        public float GetStat(StatType statType)
        {
            float baseValue = _baseStats.GetValueOrDefault(statType);
            float additiveModifier = _additiveModifiers.GetValueOrDefault(statType);
            float multiplicativeModifier = _multiplicativeModifiers.GetValueOrDefault(statType);
            return (baseValue + additiveModifier) * (1 + multiplicativeModifier);
        }
        public void AddModifiers(IEnumerable<StatModifier> modifiers)
        {
            foreach (StatModifier modifier in modifiers)
            {
                if(modifier.ModifierType == ModifierType.Additive)
                {
                    _additiveModifiers[modifier.Stat] = _additiveModifiers.GetValueOrDefault(modifier.Stat) + modifier.Value;
                }
                else if (modifier.ModifierType == ModifierType.Multiplicative)
                {
                    _multiplicativeModifiers[modifier.Stat] = _multiplicativeModifiers.GetValueOrDefault(modifier.Stat) + modifier.Value;
                }
            }
        }

        public void RemoveModifiers(IEnumerable<StatModifier> modifiers)
        {
            foreach (StatModifier modifier in modifiers)
            {
                if(modifier.ModifierType == ModifierType.Additive)
                {
                    _additiveModifiers[modifier.Stat] = _additiveModifiers.GetValueOrDefault(modifier.Stat) - modifier.Value;
                }
                else if (modifier.ModifierType == ModifierType.Multiplicative)
                {
                    _multiplicativeModifiers[modifier.Stat] = _multiplicativeModifiers.GetValueOrDefault(modifier.Stat) - modifier.Value;
                }
            }
        }
    }
}
