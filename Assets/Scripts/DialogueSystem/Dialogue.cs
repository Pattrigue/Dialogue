using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private ResponseEvent[] responseEvents;

    public DialogueObject DialogueObject => dialogueObject;
    public ResponseEvent[] ResponseEvents => responseEvents;

    public void Show(DialogueUI dialogueUI)
    {
        dialogueUI.ShowDialogue(DialogueObject).SetResponseEvents(ResponseEvents);
    }
}