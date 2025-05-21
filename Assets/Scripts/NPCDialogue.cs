using UnityEngine;


    
[System.Serializable]
public class ConditionalDialogue
{
    public string requiredFlag;
    public string[] lines;
    public string[] expectedAnswers;
    public bool[] requiresInput;
}
[CreateAssetMenu(fileName = "NewNPCDialogue", menuName = "NPC Dialogue")]
public class NPCDialogue : ScriptableObject
{
    public string[] expectedAnswers;
    public bool[] requiresInput;

    public string npcName;
    public Sprite npcPortrait;
    public string[] dialogueLines;
    public bool[] autoProgressLines;
    public float autoProgressDelay = 1.5f;
    public float typingSpeed = 0.05f;
    public string onDialogueCompleteFlag;
    public AudioClip voiceSound;
    public float voicePitch = 1f;
    public ConditionalDialogue[] alternateDialogues;
}
