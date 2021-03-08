using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] [Range(1, 100)] private float typewriterSpeed = 50f;

    private const KeyCode NextInput = KeyCode.Space;
    
    public Coroutine Run(DialogueObject dialogueObject, TMP_Text textLabel)
    {
        return StartCoroutine(DisplayDialogue(dialogueObject, textLabel));
    }

    private IEnumerator DisplayDialogue(DialogueObject dialogueObject, TMP_Text textLabel)
    {
        string[] dialogue = dialogueObject.Dialogue;
        
        for (int i = 0; i < dialogue.Length; i++)
        {
            yield return TypeText(dialogue[i], textLabel);

            if (i == dialogue.Length - 1 && dialogueObject.HasResponses) break;
            
            yield return new WaitUntil(() => Input.GetKeyDown(NextInput));
            textLabel.text = string.Empty;
        }
    }

    private IEnumerator TypeText(string text, TMP_Text textLabel)
    {
        float t = 0;
        int charIndex = 0;

        while (charIndex < text.Length)
        {
            t += Time.deltaTime * typewriterSpeed;
            charIndex = Mathf.FloorToInt(t);

            textLabel.text = text.Substring(0, charIndex);

            yield return null;

            if (Input.GetKeyDown(NextInput))
            {
                t = text.Length;
            }
        }

        textLabel.text = text;
    }
}