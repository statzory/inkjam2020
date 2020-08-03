using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demon : MonoBehaviour
{
    public float fadeTime;

    private StoryManager storyManager;
    private Image image;
    private Coroutine fade = null;

    public void ShowDemon()
    {
        if (fade != null)
        {
            StopCoroutine(fade);
        }

        fade = StartCoroutine(ShowDemonCoroutine());
    }
    
    public void HideDemon()
    {
        if (fade != null)
        {
            StopCoroutine(fade);
        }

        fade = StartCoroutine(HideDemonCoroutine());
    }

    public void DemonTagHandler(string tag)
    {
        switch (tag)
        {
            case "show":
                ShowDemon();
                break;
            
            case "hide":
                HideDemon();
                break;
        }
    }

    private void Awake()
    {
        image = GetComponent<Image>();
        storyManager = FindObjectOfType<StoryManager>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        storyManager.AddTagHandler("demon", DemonTagHandler);
    }

    private IEnumerator ShowDemonCoroutine()
    {

        var fadeRate = (1 / fadeTime) * Time.deltaTime;
        for (var f = 0.0f; f < 1.0f; f += fadeRate)
        {
            var color = image.color;
            color.a = f;
            image.color = color;
            yield return null;
        }
    }
    
    private IEnumerator HideDemonCoroutine()
    {

        var fadeRate = (1 / fadeTime) * Time.deltaTime;
        for (var f = 1.0f; f > 0.0f; f -= fadeRate)
        {
            var color = image.color;
            color.a = f;
            image.color = color;
            yield return null;
        }
    }
}
