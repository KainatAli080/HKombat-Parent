using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePlayUIManagerScript : MonoBehaviour
{
    public GameObject gamePlayPanel;
    public GameObject gameOverPanel;
    public GameObject gamePausedPanel;
    public GameObject readySetGoPanel;

    public TextMeshProUGUI tapViewer;
    public TextMeshProUGUI timerViewer;
    public TextMeshProUGUI WinLoseLabel;
    public TextMeshProUGUI HighScoreValue;
    public TextMeshProUGUI ReadySetGoTimerValue;

    // Called as soon as Scene is loaded
    void Start()
    {
        readySetGoPanel.SetActive(true);
        gamePlayPanel.SetActive(false);
        gamePausedPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void goBackToMainMenu() {
        SceneManager.LoadScene(0);
    }

    public void pauseGamePanelActivate() { 
        gamePausedPanel.SetActive(true);
    }

    public void resumeGame() { 
        gamePausedPanel.SetActive(false);
    }

    public void updateCounter(int counter) {
        tapViewer.text = counter.ToString();
    }

    public void updateTimer(float timer)
    {
        timerViewer.text = timer.ToString();
    }

    public void ShowWinLosePanel(string win_lose)
    {
        gameOverPanel.SetActive(true);
        WinLoseLabel.text = win_lose;
    }

    public void Retry() 
    {
        SceneManager.LoadScene(1);
    }

    public void updateHighScore(int highscore) 
    {
        HighScoreValue.text = highscore.ToString();
    }

    public void updateReadySetGoTimer(float timer)
    { 
        ReadySetGoTimerValue.text = timer.ToString();
    }

    public void startGameNow()
    {
        readySetGoPanel.SetActive(false);
        gamePlayPanel.SetActive(true);
    }
}
