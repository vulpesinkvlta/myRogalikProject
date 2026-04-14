using UnityEngine;

[CreateAssetMenu(fileName = "SinEncounterConfig", menuName = "GamePlayData/SinEncounter")]
public class SinEncounterConfig : ScriptableObject
{
    public SinType SinType;

    public EnemiesConfig[] RoomEnemies;
    public EnemiesConfig BossEnemy;
}