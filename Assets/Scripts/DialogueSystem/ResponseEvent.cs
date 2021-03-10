using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ResponseEvent
{
    [HideInInspector] public string name; 

    [SerializeField] private UnityEvent onResponseSelected;
    
    public DialogueObject DialogueObject { get; private set; }

    public ResponseEvent(Response response)
    {
        name = response.ResponseText;
        DialogueObject = response.DialogueObject;
    }

    public void Update(ResponseEvent responseEvent)
    {
        name = responseEvent.name;
        DialogueObject = responseEvent.DialogueObject;
    } 
    
    public void OnSelected()
    {
        onResponseSelected?.Invoke();
    }
}