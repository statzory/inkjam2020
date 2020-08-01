using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Background : MonoBehaviour
{
    [SerializeField]
    private StringSpriteDictionary backgrounds;
    private StoryManager storyManager;
    private Image image;

    public void SetBackground(string background)
    {
        if (backgrounds.ContainsKey(background))
        {
            image.sprite = backgrounds[background];
        }
    }

    private void Awake()
    {
        image = GetComponent<Image>();
        storyManager = FindObjectOfType<StoryManager>();
    }

    private void Start()
    {
        storyManager.AddTagHandler("background", SetBackground);
    }
}
