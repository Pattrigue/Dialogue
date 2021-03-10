using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ResponseEvent
{
    [SerializeField] private string name;
    [SerializeField] private DialogueObject response;
    [SerializeField] private UnityEvent onResponseSelected;

    public DialogueObject Response => response;
    
    public void OnSelected()
    {
        onResponseSelected?.Invoke();
    }
}