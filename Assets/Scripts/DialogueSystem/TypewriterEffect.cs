using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour, IDialogueDisplayEffect
{
    [SerializeField] [Range(1, 100)] private float typewriterSpeed = 50f;

    public Coroutine Run(string text, TMP_Text textLabel, Func<bool> nextButtonPressed)
    {
        return StartCoroutine(TypeText(text, textLabel, nextButtonPressed));
    }
    
    private IEnumerator TypeText(string text, TMP_Text textLabel, Func<bool> nextButtonPressed)
    {
        float t = 0;
        int charIndex = 0;

        while (charIndex < text.Length)
        {
            t += Time.deltaTime * typewriterSpeed;
            charIndex = Mathf.FloorToInt(t);

            textLabel.text = text.Substring(0, charIndex);

            yield return null;

            if (nextButtonPressed())
            {
                break;
            }
        }

        textLabel.text = text;
    }
}