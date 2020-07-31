using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Background : MonoBehaviour
{
    private StoryManager storyManager;
    private RawImage rawImage;
    private Dictionary<string, string> backgrounds = new Dictionary<string, string>
    {
        {"beginning", "https://cdn.glitch.com/475e1d65-6dfb-4a37-92b8-ffd178d08d8c%2Fbeginning.jpg?1520817106330"},
        {"farm", "https://cdn.glitch.com/475e1d65-6dfb-4a37-92b8-ffd178d08d8c%2Ffarm.jpg?1520817107140"},
        {"barn", "https://cdn.glitch.com/475e1d65-6dfb-4a37-92b8-ffd178d08d8c%2Fbarn.jpg?1520817110937"},
        {"city", "https://cdn.glitch.com/475e1d65-6dfb-4a37-92b8-ffd178d08d8c%2Fcity.jpg?1520817106054"},
        {"bar", "https://cdn.glitch.com/475e1d65-6dfb-4a37-92b8-ffd178d08d8c%2Fbar.jpg?1520817105083"},
        {"store", "https://cdn.glitch.com/475e1d65-6dfb-4a37-92b8-ffd178d08d8c%2Fstore.jpg?1520817110669"}
    };

    public void SetBackground(string background)
    {
        if (backgrounds.ContainsKey(background))
        {
            StartCoroutine(DownloadImage(backgrounds[background]));
        }
    }

    private void Awake()
    {
        rawImage = GetComponent<RawImage>();
        storyManager = FindObjectOfType<StoryManager>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        storyManager.AddTagHandler("background", SetBackground);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator DownloadImage(string mediaUrl)
    {   
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(mediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            rawImage.texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
        }
    } 
}
