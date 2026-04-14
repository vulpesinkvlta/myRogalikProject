using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(fileName = "EnemiesConfig", menuName = "GamePlayData/EnemiesConfig")]
public class EnemiesConfig : ScriptableObject
{
    public EnemyType EnemyType;
    public float Health;
    public float Speed;
    public float AttackDamage;
    public float AttackRange;
    public float AttackCooldown;
    public Sprite EnemySprite;
    public Material EnemyMaterial;
    public Light2D EnemyLight;
}

public enum EnemyType
{
    Melee,
    Ranged
}