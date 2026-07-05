using UnityEngine;

[CreateAssetMenu(fileName = "BossConfig", menuName = "GamePlayData/BossConfig")]
public class BossConfig : ScriptableObject
{
    public string Id;
    public string Name;

    public DialogueConfig OfferDialogue;

    public float Health;
    public float Damage;
}