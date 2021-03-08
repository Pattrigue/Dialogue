using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResponseHandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;

    private DialogueUI dialogueUI;
    private List<GameObject> tempResponseButtons;
    private ResponseEvent[] responseEvents;
    
    private void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();
        
        responseBox.gameObject.SetActive(false);
        responseButtonTemplate.gameObject.SetActive(false);
    }
    
    public void SetResponseEvents(ResponseEvent[] responseEvents)
    {
        this.responseEvents = responseEvents;
    }

    public void ShowResponses(Response[] responses)
    {
        float responseBoxHeight = 0;

        tempResponseButtons = new List<GameObject>(responses.Length);
        
        foreach (Response response in responses)
        {
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response));
            
            tempResponseButtons.Add(responseButton);
            
            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }

        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.gameObject.SetActive(true);
    }

    private void OnPickedResponse(Response response)
    {
        FireResponseEvent(response);
        ClearResponseBox();
        
        // Show the new dialogue
        dialogueUI.ShowDialogue(response.DialogueObject);
    }

    private void FireResponseEvent(Response response)
    {
        if (responseEvents != null)
        {
            Array.Find(responseEvents, e => e.Response == response.DialogueObject)?.OnSelected();
        }

        responseEvents = null;
    }
    
    private void ClearResponseBox()
    {
        foreach (GameObject tempResponseButton in tempResponseButtons)
        {
            Destroy(tempResponseButton);
        }

        tempResponseButtons.Clear();
        responseBox.gameObject.SetActive(false);
    }
}
