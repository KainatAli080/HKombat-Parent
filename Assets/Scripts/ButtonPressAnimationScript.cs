using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuAnimationScript : MonoBehaviour, IPointerDownHandler
{
    public Animator playBtnAnimator; // Reference to Animator for triggering animations.
    public MainMenuScript mainMenuScriptRef;

    // Called when the button is pressed
    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(PlayAnimationWithDelay());
    }

    // Coroutine to play animation with a delay
    private IEnumerator PlayAnimationWithDelay()
    {
        playBtnAnimator.SetTrigger("playPressed");
        yield return new WaitForSeconds(0.7f); 
        playBtnAnimator.ResetTrigger("playPressed");

        mainMenuScriptRef.PlayButtonClicked();
    }
}
