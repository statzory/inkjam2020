using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChoiceButtons : MonoBehaviour
{
    [SerializeField]
    private Button buttonPrefab;
    private Button[] buttons;
    private int buttonsFaded = 0;
    
    public void DisplayChoices(List<Choice> choices, StoryManager storyManager)
    {
        buttonsFaded = 0;
        
        for (var i = 0; i < buttons.Length && i < choices.Count; i++)
        {
            var choiceButton = buttons[i];
            var choice = choices[i];
            
            choiceButton.gameObject.SetActive(true);
            choiceButton.interactable = true;
            choiceButton.GetComponent<CanvasGroup>().alpha = 1.0f;
            
            var choiceText = choiceButton.GetComponentInChildren<Text>();
            choiceText.text = choice.text;

            UnityAction selectChoice = delegate { storyManager.SelectChoice(choice.index); };

            var selctedIndex = i;
            choiceButton.onClick.AddListener(delegate
            {
                StartCoroutine(SelectChoiceAnimation(selectChoice, selctedIndex));
            });
        }
    }

    public void ClearChoices()
    {
        foreach (var choice in buttons)
        {
            choice.onClick.RemoveAllListeners();
            choice.gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();
    }

    private IEnumerator SelectChoiceAnimation(UnityAction onFinishDelegate, int selectedButton)
    {
        foreach (var button in buttons)
        {
            button.interactable = false;
        }

        for (var buttonIndex = 0; buttonIndex < buttons.Length; buttonIndex++)
        {
            if (buttonIndex == selectedButton)
            {
                continue;
            }

            StartCoroutine(FadeButton(buttons[buttonIndex]));
        }

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(FadeButton(buttons[selectedButton]));
        
        yield return new WaitUntil(() => buttonsFaded == buttons.Length);
        
        onFinishDelegate.Invoke();
    }

    private IEnumerator FadeButton(Button button)
    {
        var a = 1.0f;
        var groupComponent = button.GetComponent<CanvasGroup>();

        while (a > 0.0f)
        {
            groupComponent.alpha = Mathf.Max(a, 0.0f);
            a -= 0.005f;
            yield return null;
        }

        buttonsFaded++;
    }
}

