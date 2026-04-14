using UnityEngine;

[CreateAssetMenu(fileName = "VirtueConfig", menuName = "GamePlayData/VirtueConfig")]
public class VirtueConfig : ScriptableObject
{
    public string Id;
    public string Name;
    public string Description;

    public EffectConfig EffectConfig;
}

[System.Serializable]
public struct StatModifier
{
    public StatType Stat;
    public ModifierType ModifierType;
    public float Value;
}

public enum StatType
{
    MaxHP,
    Damage,
    MoveSpeed,
    AttackSpeed,
    AttackRange
}

public enum ModifierType
{
    Additive,
    Multiplicative
}