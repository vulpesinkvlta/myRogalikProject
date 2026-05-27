using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueConfig", menuName = "GamePlayData/DialogueConfig")]
public class DialogueConfig : ScriptableObject
{
    public string Id;

    public DialogueLine[] Lines;
}

[Serializable] 
public struct DialogueLine
{
    public string SpeakerName;
    [TextArea(2,5)]
    public string Text;
}