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
    
    public void DisplayChoices(List<Choice> choices, StoryManager storyManager)
    {
        foreach (var choice in choices)
        {
            var choiceButton = Instantiate(buttonPrefab, transform, false);
            choiceButton.transform.Translate(Vector3.zero);

            var choiceText = choiceButton.GetComponentInChildren<Text>();
            choiceText.text = choice.text;

            choiceButton.onClick.AddListener(delegate { storyManager.SelectChoice(choice.index); });
        }
    }

    public void ClearChoices()
    {
        foreach (var choice in GetComponentsInChildren<Button>())
        {
            Destroy(choice.gameObject);
        }
    }
}
