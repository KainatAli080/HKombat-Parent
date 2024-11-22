using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class GameLabelAnimationScript : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Color colorA;
    public Color colorB;
    public float duration = 10;
    private bool isOnMainScreen = true;

    public GameObject mainMenuPanel;

    // Object to store reference top coroutine
    // Allows us to have more control over the coroutine we execute
    // Using it here to control my animations and ensure I do not start multiple coroutines at once
    private Coroutine colorCoroutine;

    private void Start()
    {
        if (mainMenuPanel.activeSelf)
            isOnMainScreen = true;   
    }

    private void Update()
    {
        if (mainMenuPanel.activeSelf == false)
        { 
            isOnMainScreen = false; 
            StopCoroutine(colorCoroutine);
        }

        if (mainMenuPanel.activeSelf == true)
        {
            if(colorCoroutine == null)
                colorCoroutine = StartCoroutine(LoopColorChange());
        }
    }

    private IEnumerator LoopColorChange()
    {
        // Fade from colorB to colorA
        yield return changeColor(colorB, colorA, duration);

        // Fade from colorA to colorB
        // yield return changeColor(colorA, colorB, duration);
    }

    private IEnumerator changeColor(Color start, Color end, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            text.color = Color.Lerp(start, end, time / duration);
            time += Time.deltaTime;
            // This line yields the coroutine's execution until the next frame. By yielding null, we’re telling
            // Unity to pause the coroutine here and resume it on the next frame. This allows other operations
            // in Unity to continue running without blocking the main thread.
            yield return null;
        }

        text.color = colorB; // Ensure the final color is set
        colorCoroutine = null; // Reset reference when done
    } 
}
