using System;
using TMPro;
using UnityEngine;

public interface IDialogueDisplayEffect
{
    Coroutine Run(string text, TMP_Text textLabel, Func<bool> nextButtonPressed);
}