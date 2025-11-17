using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TimeEndedAddTimeUI : MonoBehaviour, IPasswordAccessedUI
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private UIPointerDetector exitUIDetector;

    [Header("UI Components")]
    [SerializeField] private List<Button> openButtons;
    [Space]
    [SerializeField] private List<Button> UIButtons;

    private const string SHOW_TRIGGER = "Show";
    private const string HIDE_TRIGGER = "Hide";

    public event EventHandler OnUIClosedByButtonClick;

    private void OnEnable()
    {
        exitUIDetector.OnPointerClicked += ExitUIDetector_OnPointerClicked;
    }

    private void OnDisable()
    {
        exitUIDetector.OnPointerClicked -= ExitUIDetector_OnPointerClicked;
    }

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        foreach(Button button in openButtons)
        {
            button.onClick.AddListener(OpenUI);
        }

        foreach (Button UIButton in UIButtons)
        {
            UIButton.onClick.AddListener(CloseUIByButtonClick);
        }
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

    private void OpenUI()
    {
        ShowUI();
    }

    private void CloseUI()
    {
        HideUI();
    }

    protected virtual void CloseUIByButtonClick()
    {
        CloseUI();
        OnUIClosedByButtonClick?.Invoke(this, EventArgs.Empty);
    }

    public void AccessUI()
    {
        OpenUI();
    }

    #region Subscriptions
    private void ExitUIDetector_OnPointerClicked(object sender, EventArgs e)
    {
        HideUI();
    }
    #endregion
}
