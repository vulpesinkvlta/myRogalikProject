using UnityEngine;

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
    public float DetectionRange;
    public float WanderChangeDirectionTime;

    public GameObject ProjectilePrefab;
    public float ProjectileSpeed;
}

public enum EnemyType
{
    Melee,
    Ranged
}