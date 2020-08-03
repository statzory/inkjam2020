using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
  public float FadeTime = 1.0f;
  public Canvas firstCanvas;

  string dest;
  Canvas visibleCanvas;
  Coroutine fadeRoutine;

  private void Awake()
  {
    visibleCanvas = firstCanvas;
  }

  // Use this for initialization
	void Start ()
  {
	
	}
	
	// Update is called once per frame
	void Update ()
  {
	}

  public void SwapScene(string scene)
  {
    dest = scene;
    if (fadeRoutine != null)
    {
      StopCoroutine(fadeRoutine);
    }
    fadeRoutine = StartCoroutine("SwapOut");
  }

  public void SwapCanvas(string canvasName)
  {
    foreach (var canvas in FindObjectsOfType<Canvas>())
    {
      if (canvas.gameObject.name.Equals(canvasName))
      {
        visibleCanvas.enabled = false;
        visibleCanvas = canvas;
        visibleCanvas.enabled = true;
      }
    }
  }

  public void QuitGame()
  {
    dest = "quit";
    if (fadeRoutine != null)
    {
      StopCoroutine(fadeRoutine);
    }
    fadeRoutine = StartCoroutine("SwapOut");
  }

  IEnumerator SwapOut()
  {
    var canvasGroup = visibleCanvas.GetComponent<CanvasGroup>();
    
    float fadeRate = (1 / FadeTime) * Time.deltaTime;
    for (float f = 1.0f; f > 0.0f; f -= fadeRate)
    {
      canvasGroup.alpha = f;
      yield return null;
    }
    
    if (dest == "quit")
    {
      Application.Quit();
    }
    else
    {
      SceneManager.LoadScene(dest);
    }
  }
}
