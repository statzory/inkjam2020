using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.Events;
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
    private List<string> currentTags;
    private Dictionary<string, UnityEvent<string>> tagHandlers;

    public void SelectChoice(int index)
    {
        Debug.Log("index is " + index);
        inkStory.ChooseChoiceIndex(index);
        ContinueStory();
    }

    public UnityAction<string> AddTagHandler(string tagType, UnityAction<string> handler)
    {
        if (!tagHandlers.ContainsKey(tagType))
        {
            tagHandlers.Add(tagType, new UnityEvent<string>());
        }
        
        tagHandlers[tagType].AddListener(handler);

        return handler;
    }
    
    public void RemoveTagHandler(string tagType, UnityAction<string> handler)
    {
        if (tagHandlers.ContainsKey(tagType))
        {
            tagHandlers[tagType].RemoveListener(handler);
        }
    }

    private void Awake()
    {
        inkStory = new Story(inkFile.text);
        currentTags = new List<string>();
        tagHandlers = new Dictionary<string, UnityEvent<string>>();
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
        
        currentTags.Clear();
        
        // Ensure that there is text to get
        if (inkStory.canContinue)
        {
            // Get the next line
            string nextLine = "";

            if (shouldContinueMaximally)
            {
                while (inkStory.canContinue)
                {
                    nextLine += inkStory.Continue();
                    currentTags.AddRange(inkStory.currentTags);
                }
            }
            else
            {
                nextLine = inkStory.Continue();
                currentTags.AddRange(inkStory.currentTags);
            }

            // Display text
            storyTextDisplay.DisplayText(nextLine);
            
            // Handle Tags
            InvokeTags();
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

    private void InvokeTags()
    {
        foreach (var tag in currentTags)
        {
            // Get the type and the content, making sure to trim whitespace
            var tagType = tag.Split(':')[0].Trim();
            var tagContent = tag.Split(':')[1].Trim();

            if (tagHandlers.ContainsKey(tagType))
            {
                tagHandlers[tagType].Invoke(tagContent);
            }
        }
    }
}
