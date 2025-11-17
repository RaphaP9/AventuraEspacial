using UnityEngine;
using System;
using UnityEngine.UI;

public class TimeEndedUI : PauseUIBase
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private TimeEndedAddTimeUI timeEndedAddTimeUI;

    public static event EventHandler OnTimeEndedUIOpen;
    public static event EventHandler OnTimeEndedUIClose;

    private const string SHOW_TRIGGER = "Show";
    private const string HIDE_TRIGGER = "Hide";

    private void OnEnable()
    {
        TimerManager.OnTimeEnded += TimerManager_OnTimeEnded;
        timeEndedAddTimeUI.OnUIClosedByButtonClick += TimeEndedAddTimeUI_OnUIClosedByButtonClick;
    }

    private void OnDisable()
    {
        TimerManager.OnTimeEnded -= TimerManager_OnTimeEnded;
        timeEndedAddTimeUI.OnUIClosedByButtonClick -= TimeEndedAddTimeUI_OnUIClosedByButtonClick;
    }

    public void OpenUI()
    {
        ShowUI();
        OnPauseUIBaseOpenMethod();
        OnTimeEndedUIOpen?.Invoke(this, EventArgs.Empty);
    }

    private void CloseUI()
    {
        HideUI();
        OnPauseUIBaseCloseMethod();
        OnTimeEndedUIClose?.Invoke(this, EventArgs.Empty);
    }

    public void ShowUI()
    {
        animator.ResetTrigger(HIDE_TRIGGER);
        animator.SetTrigger(SHOW_TRIGGER);
    }

    public void HideUI()
    {
        animator.ResetTrigger(SHOW_TRIGGER);
        animator.SetTrigger(HIDE_TRIGGER);
    }

    #region Subscriptions
    private void TimerManager_OnTimeEnded(object sender, TimerManager.OnTimeEventArgs e)
    {
        OpenUI();
    }

    private void TimeEndedAddTimeUI_OnUIClosedByButtonClick(object sender, EventArgs e)
    {
        CloseUI();
    }

    #endregion
}