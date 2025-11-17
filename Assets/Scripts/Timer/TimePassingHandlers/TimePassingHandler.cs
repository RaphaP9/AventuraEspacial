using System;
using UnityEngine;

public abstract class TimePassingHandler : MonoBehaviour
{
    public static TimePassingHandler Instance { get; private set; }

    private bool gamePaused = false;
    private bool optionsUIOpen = false;
    private bool passwordUIOpen = false;

    private void OnEnable()
    {
        PauseManager.OnGamePaused += PauseManager_OnGamePaused;
        PauseManager.OnGameResumed += PauseManager_OnGameResumed;

        OptionsUI.OnOptionsUIOpen += OptionsUI_OnOptionsUIOpen;
        OptionsUI.OnOptionsUIClose += OptionsUI_OnOptionsUIClose;

        PasswordAccessUI.OnPasswordAccessUIOpen += PasswordAccessUI_OnPasswordAccessUIOpen;
        PasswordAccessUI.OnPasswordAccessUIClose += PasswordAccessUI_OnPasswordAccessUIClose;
    }

    private void OnDisable()
    {
        PauseManager.OnGamePaused -= PauseManager_OnGamePaused;
        PauseManager.OnGameResumed -= PauseManager_OnGameResumed;

        OptionsUI.OnOptionsUIOpen -= OptionsUI_OnOptionsUIOpen;
        OptionsUI.OnOptionsUIClose -= OptionsUI_OnOptionsUIClose;

        PasswordAccessUI.OnPasswordAccessUIOpen -= PasswordAccessUI_OnPasswordAccessUIOpen;
        PasswordAccessUI.OnPasswordAccessUIClose -= PasswordAccessUI_OnPasswordAccessUIClose;
    }

    private void Awake()
    {
        SetSingleton();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //Debug.LogWarning("There is more than one TimePassingHandler instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    public virtual bool CanPassTime()
    {
        if (ScenesManager.Instance != null)
        {
            if (!ScenesManager.Instance.IsSceneOnIdle()) return false; //Can't pass time while scene transitioning
        }

        if (gamePaused) return false;
        if (optionsUIOpen) return false;
        if (passwordUIOpen) return false;

        return true;
    }

    #region Subscriptions
    private void PauseManager_OnGamePaused(object sender, EventArgs e)
    {
        gamePaused = true;
    }

    private void PauseManager_OnGameResumed(object sender, EventArgs e)
    {
        gamePaused = false;
    }

    private void OptionsUI_OnOptionsUIOpen(object sender, EventArgs e)
    {
        optionsUIOpen = true;
    }

    private void OptionsUI_OnOptionsUIClose(object sender, EventArgs e)
    {
        optionsUIOpen = false;
    }
    private void PasswordAccessUI_OnPasswordAccessUIOpen(object sender, EventArgs e)
    {
        passwordUIOpen = true;
    }

    private void PasswordAccessUI_OnPasswordAccessUIClose(object sender, EventArgs e)
    {
        passwordUIOpen = false;
    }
    #endregion
}
