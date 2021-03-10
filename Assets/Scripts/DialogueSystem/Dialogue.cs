using System;
using UnityEngine;

[Serializable]
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

    public void OnValidate()
    {
        ResponseEvent[] events = new ResponseEvent[dialogueObject.Responses.Length];

        for (int i = 0; i < dialogueObject.Responses.Length; i++)
        {
            Response response = dialogueObject.Responses[i];
            events[i] = new ResponseEvent(response);
        }

        Array.Resize(ref responseEvents, events.Length);

        for (int i = 0; i < responseEvents.Length; i++)
        {
            if (responseEvents[i] == null)
            {
                responseEvents[i] = events[i];
            }
            else
            {
                responseEvents[i].Update(events[i]);
            }
        }
    }
}