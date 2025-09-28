using System;
using UnityEngine;

public abstract class TimePassingHandler : MonoBehaviour
{
    public static TimePassingHandler Instance { get; private set; }

    private bool optionsOpen = false;
    private bool gamePaused = false;

    private void OnEnable()
    {
        PauseManager.OnGamePaused += PauseManager_OnGamePaused;
        PauseManager.OnGameResumed += PauseManager_OnGameResumed;

        OptionsUI.OnOptionsUIOpen += OptionsUI_OnOptionsUIOpen;
        OptionsUI.OnOptionsUIClose += OptionsUI_OnOptionsUIClose;
    }

    private void OnDisable()
    {
        PauseManager.OnGamePaused -= PauseManager_OnGamePaused;
        PauseManager.OnGameResumed -= PauseManager_OnGameResumed;

        OptionsUI.OnOptionsUIOpen -= OptionsUI_OnOptionsUIOpen;
        OptionsUI.OnOptionsUIClose -= OptionsUI_OnOptionsUIClose;
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
        if (optionsOpen) return false;

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
        optionsOpen = true;
    }

    private void OptionsUI_OnOptionsUIClose(object sender, EventArgs e)
    {
        optionsOpen = false;
    }
    #endregion
}
