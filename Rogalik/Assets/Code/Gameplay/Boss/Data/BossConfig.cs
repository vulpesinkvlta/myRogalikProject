using UnityEngine;

[CreateAssetMenu(fileName = "BossConfig", menuName = "GamePlayData/BossConfig")]
public class BossConfig : ScriptableObject
{
    public string Id;
    public SinsConfig Sin;

    public int MaxHealth;
    public int Damage;

    public string DialogueId;
}
