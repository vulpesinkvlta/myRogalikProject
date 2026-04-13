using UnityEngine;

[CreateAssetMenu(fileName = "DialogueConfig", menuName = "GamePlayData/DialogueConfig")]
public class DialogueConfig : ScriptableObject
{
    public string Id;

    [TextArea]
    public string Text;
}
