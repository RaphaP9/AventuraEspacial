using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }

    [Header("Runtime Filled")]
    [SerializeField] private bool gamePaused;

    public static event EventHandler OnGamePaused;
    public static event EventHandler OnGameResumed;

    public bool GamePaused => gamePaused;

    private void Awake()
    {
        SetSingleton();
    }

    private void Start()
    {
        InitializeVariables();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There is more than one PauseManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }
    private void InitializeVariables()
    {
        SetGamePaused(false);
        AudioListener.pause = false;
    }

    public void PauseGame()
    {
        OnGamePaused?.Invoke(this, EventArgs.Empty);
        SetPauseTimeScale();
        SetGamePaused(true);
        AudioListener.pause = false;
    }

    public void ResumeGame()
    {
        OnGameResumed?.Invoke(this, EventArgs.Empty);
        SetResumeTimeScale();
        SetGamePaused(false);
        AudioListener.pause = false;
    }

    private void SetGamePaused(bool gamePaused) => this.gamePaused = gamePaused;
    private void SetPauseTimeScale() => Time.timeScale = 0f;
    private void SetResumeTimeScale() => Time.timeScale = 1f;
}
