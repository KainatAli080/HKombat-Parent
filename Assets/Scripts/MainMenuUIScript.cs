using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    // These are panels within a canvas, not buttons within a panel
    public GameObject mainMenu;
    public GameObject settings;
    public GameObject credits;
    public GameObject howToPlay;

    public AudioClip playButtonAudio;   // AudioClip is the data type which holds sounds
    public AudioSource audioSource;

    public Animator settingsAnimator;
    public Animator mainMenuAnimator;
    public Animator creditsAnimator;
    public Animator howToPlayAnimator;

    public Slider volumeSlider; // can adjust audio through this
    // Audio Listener contains all the audio related

    public Sprite[] sprites;    // for changing image of volume from on to off
    public Image image;

    public float delayTime = 2;

    public void changeImage()
    {
        image.sprite = sprites[0];  // make this more adaptable, change images based upon conditions
    }

    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
        settings.SetActive(false);
        credits.SetActive(false);
        howToPlay.SetActive(false);


        volumeSlider.value = PlayerPrefs.GetFloat("AudioVolume", 1);
        AudioListener.volume = volumeSlider.value;
    }

    public void changeVolume() 
    { 
        AudioListener.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("AudioVolume", volumeSlider.value);
    }

    public void PlayButtonClicked() {
        SceneManager.LoadScene(1);  // index of the scene to be loaded  (from Build Settings)
    }

    public void GoToSettingsFromMain()
    {
        settings.SetActive(true);
        //mainMenuAnimator.SetTrigger("triggerSlideDown");
        settingsAnimator.SetTrigger("DropDownTrigger");

        StartCoroutine("delayMainMenuDisable");
    }

    public void GoToCreditsFromMain()
    {
        credits.SetActive(true);
        //mainMenuAnimator.SetTrigger("triggerSlideDown");
        creditsAnimator.SetTrigger("triggerCreditsSlideIn");

        StartCoroutine("delayMainMenuDisable");
    }

    public void GoToHowToPlayFromMain()
    {
        howToPlay.SetActive(true);
        //mainMenuAnimator.SetTrigger("triggerSlideDown");
        howToPlayAnimator.SetTrigger("triggerHowToSlideDown");

        StartCoroutine("delayMainMenuDisable");
    }

    public IEnumerator delayMainMenuDisable()
    {
        yield return new WaitForSeconds(1.5f);
        mainMenu.SetActive(false);
    }

    public void GoBackToMainFromSettings()
    {
        mainMenu.SetActive(true);    
        credits.SetActive(false);
        howToPlay.SetActive(false);

        //mainMenuAnimator.SetTrigger("triggerSlideBackUp");
        settingsAnimator.SetTrigger("SlideUpTrigger");   
        
        // Invoke("disableSettingsPanel", 1f);
        StartCoroutine("disableSettingsPanel");
    }

    public void GoBackToMainFromCredits()
    {
        mainMenu.SetActive(true);
        settings.SetActive(false);
        howToPlay.SetActive(false);

        //mainMenuAnimator.SetTrigger("triggerSlideBackUp");
        creditsAnimator.SetTrigger("triggerCreditsSlideUp");

        // Invoke("disableSettingsPanel", 1f);
        StartCoroutine("disableCreditsPanel");
    }

    public void GoBackToMainFromHowToPlay()
    {
        mainMenu.SetActive(true);
        settings.SetActive(false);
        credits.SetActive(false);

        mainMenuAnimator.SetTrigger("triggerSlideBackUp");
        howToPlayAnimator.SetTrigger("triggerHowToSlideUp");

        // Invoke("disableSettingsPanel", 1f);
        StartCoroutine("disableHowToPlayPanel");
    }

    // This is for invoke
    //public void disableSettingsPanel() {
    //    settings.SetActive(false);
    //}

    // this is for coroutine
    public IEnumerator disableSettingsPanel()
    {
        yield return new WaitForSeconds(delayTime);
        settings.SetActive(false);
    }

    public IEnumerator disableCreditsPanel()
    {
        yield return new WaitForSeconds(delayTime);
        credits.SetActive(false);
    }

    public IEnumerator disableHowToPlayPanel()
    {
        yield return new WaitForSeconds(delayTime);
        howToPlay.SetActive(false);
    }

    public void stopMusic() 
    { 
        audioSource.Stop();
        volumeSlider.value = 0;
        PlayerPrefs.SetInt("Muted", 1);
        Debug.Log("StopMusic called");
    }

    public void playMusic()
    {
        float volume = PlayerPrefs.GetFloat("AudioVolume", 1);
        audioSource.Play();
        if (volume == 0)
            volume += 0.2f;
        volumeSlider.value = volume;
        AudioListener.volume = volume;
    }

    public void playBtnSound()
    { 
        // Meaning play one sound when a button is clicked
        audioSource.PlayOneShot(playButtonAudio);
    }

    public void ResetHighScore() 
    {
        PlayerPrefs.SetInt("HighScoreLevel1", 0);
    }
}
