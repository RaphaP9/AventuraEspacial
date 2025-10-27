using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class PauseUI : PauseUIBase
{
    [Header("Components")]
    [SerializeField] private Animator pauseUIAnimator;

    [Header("UI Components")]
    [SerializeField] private List<Button> pauseButtons;
    [SerializeField] private Button resumeButton;

    public static event EventHandler OnPauseUIOpen;
    public static event EventHandler OnPauseUIClose;

    private const string SHOW_TRIGGER = "Show";
    private const string HIDE_TRIGGER = "Hide";

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        foreach(Button button in pauseButtons)
        {
            button.onClick.AddListener(OpenUI);
        }

        resumeButton.onClick.AddListener(CloseUI);
    }

    public void OpenUI()
    {
        ShowPauseUI();
        OnPauseUIBaseOpenMethod();
        OnPauseUIOpen?.Invoke(this, EventArgs.Empty);
    }

    private void CloseUI()
    {
        HidePauseUI();
        OnPauseUIBaseCloseMethod();
        OnPauseUIClose?.Invoke(this, EventArgs.Empty);
    }

    public void ShowPauseUI()
    {
        pauseUIAnimator.ResetTrigger(HIDE_TRIGGER);
        pauseUIAnimator.SetTrigger(SHOW_TRIGGER);
    }

    public void HidePauseUI()
    {
        pauseUIAnimator.ResetTrigger(SHOW_TRIGGER);
        pauseUIAnimator.SetTrigger(HIDE_TRIGGER);
    }
}
