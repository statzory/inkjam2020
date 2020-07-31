using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour
{
    // Public properties
    public bool displayingText { get; private set; }
    public UnityEvent textDisplayFinished;
    
    // Private properties
    private string displayString;
    private Text myText;
    private Coroutine displayTextCoroutine;
    
    // Public functions
    public void DisplayText(string textToDisplay)
    {
        displayingText = true;
        displayString = textToDisplay;
        displayTextCoroutine = StartCoroutine(DisplayTextCoroutine());
    }

    public void ClearText()
    {
        myText.text = "";
    }

    public void CompleteText()
    {
        StopCoroutine(displayTextCoroutine);
        myText.text = displayString;
        DisplayFinished();
    }
    
    // Private functions
    private void Awake()
    {
        myText = GetComponent<Text>();
        textDisplayFinished = new UnityEvent();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && displayingText)
        {
            CompleteText();
        }
    }

    private IEnumerator DisplayTextCoroutine()
    {
        foreach (var c in displayString)
        {
            myText.text += c;
            yield return new WaitForSeconds(0.05f);
        }

        DisplayFinished();
    }

    private void DisplayFinished()
    {
        displayingText = false;
        textDisplayFinished.Invoke();
    }
}
