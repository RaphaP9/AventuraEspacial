using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PopUpUI : MonoBehaviour
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

    public event EventHandler OnPopUpUIClosedByButtonClick;

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
        foreach (Button openButton in openButtons)
        {
            openButton.onClick.AddListener(OpenUI);
        }

        foreach (Button UIButton in UIButtons)
        {
            UIButton.onClick.AddListener(CloseUIByButtonClick);
        }
    }

    #region Animations
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
    #endregion

    protected void OpenUI()
    {
        ShowUI();
    }

    protected void CloseUI()
    {
        HideUI();
    }

    protected virtual void CloseUIByButtonClick()
    {
        CloseUI();
        OnPopUpUIClosedByButtonClick?.Invoke(this, EventArgs.Empty);
    }

    #region Subscriptions
    private void ExitUIDetector_OnPointerClicked(object sender, EventArgs e)
    {
        CloseUI();
    }
    #endregion
}
