﻿using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreDisplay, highscoreDisplay, gameOverDisplay;
    [Space]
    [SerializeField] Button undoButton;

    int highScore;

    public event Action UndoAction;

    void Awake()
    {
        GameController gc = FindObjectOfType<GameController>();
        gc.ScoreChangedAction += SetScore;
        gc.GameStateChangedAction += SetUndoButtonState;
        gc.GameOverAction += OnGameOver;

        highScore = PlayerPrefs.GetInt("highScore");
        highscoreDisplay.text = highScore.ToString();
    }

    void SetScore(int value)
    {
        //update score display
        scoreDisplay.text = value.ToString();

        //update highscore and highscore display if necessary
        if (highScore < value)
        {
            highScore = value;
            highscoreDisplay.text = highScore.ToString();
            PlayerPrefs.SetInt("highScore", highScore);
        }
    }

    void SetUndoButtonState(bool state)
    {
        undoButton.interactable = state;
    }

    void OnGameOver()
    {
        gameOverDisplay.enabled = true;
    }

    public void OnSelectUndo()
    {
        UndoAction.Invoke();
    }

    public void OnSelectReset()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void OnSelectQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}