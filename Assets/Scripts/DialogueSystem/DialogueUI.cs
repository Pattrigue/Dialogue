using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;

    public bool IsOpen { get; private set; }

    private DialogueIterator dialogueIterator;
    private ResponseHandler responseHandler;

    private bool isWritingDialogue;

    private void Start()
    {
        dialogueIterator = GetComponent<DialogueIterator>();
        responseHandler = GetComponent<ResponseHandler>();
        
        SetDialogueBoxOpen(false);
    }

    public ResponseHandler ShowDialogue(DialogueObject dialogueObject)
    {
        if (!isWritingDialogue)
        {
            StartCoroutine(DisplayDialogue(dialogueObject));
        }

        return responseHandler;
    }

    private IEnumerator DisplayDialogue(DialogueObject dialogueObject)
    {
        SetDialogueBoxOpen(true);
        isWritingDialogue = true;
        
        yield return dialogueIterator.IterateDialogue(dialogueObject, textLabel);

        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            SetDialogueBoxOpen(false);
        }
        
        isWritingDialogue = false;
    }

    private void SetDialogueBoxOpen(bool open)
    {
        dialogueBox.SetActive(open);
        IsOpen = open;
    }
}
