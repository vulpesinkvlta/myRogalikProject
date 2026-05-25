using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "GamePlayData/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    public int LevelIndex;
    public BossConfig Boss;
    public SinsConfig Sin;
    public int RoomCount;

}
