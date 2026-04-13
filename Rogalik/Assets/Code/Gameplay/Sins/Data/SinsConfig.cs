using UnityEngine;

[CreateAssetMenu(fileName = "SinsConfig", menuName = "GamePlayData/SinsConfig")]
public class SinsConfig : ScriptableObject
{
    public string Id;
    public string Name;
    public string Description;

    public SinType Type;

    public SinEffectType EffectType;
}

public enum SinType
{
    Pride,
    Envy,
    Wrath,
    Sloth,
    Greed,
    Gluttony,
    Lust
}   

public enum SinEffectType
{
    None,
    Damage,
    Heal,
    Buff,
    Debuff
}