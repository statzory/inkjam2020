using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour
{
    // Public properties
    public bool displayingText { get; private set; }
    public UnityEvent textDisplayFinished;
    
    // Private properties
    private TextMeshProUGUI myText;
    private Coroutine displayTextCoroutine;
    
    // Public functions
    public void DisplayText(string textToDisplay)
    {
        displayingText = true;
        myText.text = textToDisplay;
        myText.maxVisibleCharacters = 0;
        displayTextCoroutine = StartCoroutine(DisplayTextCoroutine());
    }

    public void ClearText()
    {
        myText.text = "";
    }

    public void CompleteText()
    {
        StopCoroutine(displayTextCoroutine);
        myText.maxVisibleCharacters = myText.text.Length;
        DisplayFinished();
    }
    
    // Private functions
    private void Awake()
    {
        myText = GetComponent<TextMeshProUGUI>();
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
        foreach (var c in myText.text)
        {
            myText.maxVisibleCharacters++;
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
