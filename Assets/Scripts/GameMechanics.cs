using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMechanics : MonoBehaviour
{
    public int counter = 0;
    public float timer;
    private float defaultTimer = 5;    // default timer for user (30 seconds to perform any action)
    public GamePlayUIManagerScript gamePlayUIReference;

    private bool timeEnded = false;
    public bool isPaused;

    private int counterTarget = 20;
    private bool hasWon = false;
    private int highScore = 0;

    private float readySetTimer = 4;
    private bool isReadyToPlay = false;

    private bool touchRegistered;

    public AudioSource gamePlayAudioSource;
    public AudioClip[] audioClips;

    // Start is called before the first frame update
    void Start()
    {
        timer = defaultTimer;
        isPaused = false;
        highScore = PlayerPrefs.GetInt("HighScoreLevel1", 0);
        gamePlayUIReference.updateHighScore(highScore);
        touchRegistered = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Meaning timer for ReadySetGo is still not finished
        if (isReadyToPlay == false)
        {
            readySetTimer = readySetTimer - Time.deltaTime;
            if (readySetTimer <= 0)
            {
                isReadyToPlay = true;
                readySetTimer = 0;
            }

            float intoSeconds = Mathf.FloorToInt(readySetTimer);
            gamePlayUIReference.updateReadySetGoTimer(intoSeconds);
        }
        else { 
            // meaning timer has ended
            gamePlayUIReference.startGameNow();          
        }

        if (timeEnded == false && isReadyToPlay == true)
        {
            // deltaTime is the amount of time taken to render the current frame
            // Each frame can take variable time to render, depending upon the physics, objects, and more present on the frame to render
            timer = timer - Time.deltaTime;
            if (timer <= 0)
            {
                timeEnded = true;
                timer = 0;
            }
            // The following function converts given value to floor value and then to int. 
            float seconds = Mathf.FloorToInt(timer);
            gamePlayUIReference.updateTimer(seconds);

            // Handling mouse Input
            if ((Input.GetMouseButtonDown(0)) && isPaused == false)
            {
                counter++;
                playSound();
                gamePlayUIReference.updateCounter(counter);
            }

            // handling touch input
            // touchCount checks if there is at least one touch on the screen
            else if ((Input.touchCount > 0) && isPaused == false)
            {
                // Input.GetTouch(0) gets data about the first touch, Input.GetTouch(1) retrieves the second, goes on and on
                // This is for when there are multiple touches on the screen simultaneously, for example, zoominh in and stuff
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began && !touchRegistered) { 
                    // TouchPhase.Began detecting the first time we tap so we can ignore if user keeps his finger on the screen
                    counter++;
                    playSound();
                    gamePlayUIReference.updateCounter(counter);
                    touchRegistered = true; // to make sure touch is only registered once
                }
                // Reset when the finger is lifted
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    touchRegistered = false;
                }
            }
        }
        else if (isReadyToPlay == true)
        {
            // meaning timer ended, checking win condition
            if (counter >= counterTarget)
            {
                Debug.Log("Has Won!");
                gamePlayUIReference.ShowWinLosePanel("You Win!");
            }
            else 
            {
                Debug.Log("Has Lost....");
                gamePlayUIReference.ShowWinLosePanel("You Lost...");
            }

            // Checking if final Score higher than HighScore
            if (counter > highScore)
            { 
                highScore = counter;
                PlayerPrefs.SetInt("HighScoreLevel1", highScore);
                gamePlayUIReference.updateHighScore(highScore);
            }
        }
    }

    public void PauseGame() {
        isPaused = true;
        Time.timeScale = 0;
        gamePlayUIReference.pauseGamePanelActivate();

        // Still detecting touch upon pausing
        counter--;
        gamePlayUIReference.updateCounter(counter);
    }

    public void ResumeGame() {
        isPaused = false;
        Time.timeScale = 1;
        gamePlayUIReference.resumeGame();
    }

    public void playSound() 
    {
        int random;
        random = Random.Range(0, 4);    // in Random.Range(x, y), x value is included in indexes and y is excluded
        gamePlayAudioSource.PlayOneShot(audioClips[random]);
    }
}
