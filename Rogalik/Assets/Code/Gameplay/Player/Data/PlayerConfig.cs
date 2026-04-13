using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "GamePlayData/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    public int MaxHealth;
    public int StartingHealth;
    public int BaseDamage;  
    public float MoveSpeed;
    public float AttackRange;
    public float AttackSpeed;
}
