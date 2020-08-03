using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceButtons : MonoBehaviour
{
    [SerializeField]
    private Button buttonPrefab;
    private Button[] buttons;
    
    public void DisplayChoices(List<Choice> choices, StoryManager storyManager)
    {
        for (var i = 0; i < buttons.Length; i++)
        {
            var choiceButton = buttons[i];
            var choice = choices[i];
            
            choiceButton.gameObject.SetActive(true);
            
            var choiceText = choiceButton.GetComponentInChildren<Text>();
            choiceText.text = choice.text;

            choiceButton.onClick.AddListener(delegate { storyManager.SelectChoice(choice.index); });
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
}
