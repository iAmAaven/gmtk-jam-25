using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string speakerName;
    [TextArea(2, 6)]
    public string rawText;
}
