using Core;
using UnityEngine;

public abstract class EffectConfig : ScriptableObject
{
    public abstract void Apply(PlayerStats playerStats);
    public abstract void Remove(PlayerStats playerStats); 
}
