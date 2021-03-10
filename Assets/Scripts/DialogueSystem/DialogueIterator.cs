using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueIterator : MonoBehaviour
{
    [SerializeField] private KeyCode nextInput;
    
    private IDialogueDisplayEffect displayEffect;

    private void Start()
    {
        displayEffect = GetComponent<IDialogueDisplayEffect>();
    }

    public IEnumerator IterateDialogue(DialogueObject dialogueObject, TMP_Text textLabel)
    {
        string[] dialogue = dialogueObject.Dialogue;
        
        for (int i = 0; i < dialogue.Length; i++)
        {
            string text = dialogue[i];
            
            yield return displayEffect.Run(text, textLabel, NextButtonPressed);

            textLabel.text = text;
            
            if (i == dialogue.Length - 1 && dialogueObject.HasResponses) break;

            yield return null;
            yield return new WaitUntil(NextButtonPressed);

            textLabel.text = string.Empty;
        }
    }

    private bool NextButtonPressed()
    {
        return Input.GetKeyDown(nextInput);
    }
}