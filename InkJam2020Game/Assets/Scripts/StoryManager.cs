using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    public bool shouldContinueMaximally;
    public Story inkStory { get; private set; }
    
    [SerializeField]
    private TextDisplay storyTextDisplay;
    [SerializeField]
    private ChoiceButtons storyChoiceButtons;
    [SerializeField]
    private TextAsset inkFile;

    public void SelectChoice(int index)
    {
        Debug.Log("index is " + index);
        inkStory.ChooseChoiceIndex(index);
        ContinueStory();
    }

    private void Awake()
    {
        inkStory = new Story(inkFile.text);
    }

    private void Start()
    {
        storyTextDisplay.textDisplayFinished.AddListener(OnTextDisplayFinished);
        
        ContinueStory();
    }

    private void ContinueStory()
    {
        // Clear UI
        storyTextDisplay.ClearText();
        storyChoiceButtons.ClearChoices();
        
        // Ensure that there is text to get
        if (inkStory.canContinue)
        {
            // Get the next line
            string nextLine;

            if (shouldContinueMaximally)
            {
                nextLine = inkStory.ContinueMaximally();
            }
            else
            {
                nextLine = inkStory.Continue();
            }

            // Display text
            storyTextDisplay.DisplayText(nextLine);
        }
        else if (inkStory.currentChoices.Count > 0)
        {
            storyChoiceButtons.DisplayChoices(inkStory.currentChoices, this);
        }
        else
        {
            Debug.LogError("The story is over! It can't continue!");
        }
    }

    private void OnTextDisplayFinished()
    {
        // Now that the text has finished displaying, we can present the user with their responses
        if (inkStory.currentChoices.Count > 0)
        {
            storyChoiceButtons.DisplayChoices(inkStory.currentChoices, this);
        }
    }
}
