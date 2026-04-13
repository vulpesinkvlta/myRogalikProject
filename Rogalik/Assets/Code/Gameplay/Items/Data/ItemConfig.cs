using UnityEngine;

[CreateAssetMenu(fileName = "ItemConfig", menuName = "GamePlayData/ItemConfig")]
public class ItemConfig : ScriptableObject
{
    public string Id;
    public string Name;
    public string Description;
    
    public Sprite Icon;

    public ItemEffectType EffectType;
}

public enum ItemEffectType
{
    None,
    Heal,
    Damage,
    Buff,
    Debuff
}   
