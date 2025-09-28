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
    [SerializeField] private Button openButton;
    [Space]
    [SerializeField] private List<Button> UIButtons;

    private const string SHOW_TRIGGER = "Show";
    private const string HIDE_TRIGGER = "Hide";

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
        openButton.onClick.AddListener(OpenUI);

        foreach (Button UIButton in UIButtons)
        {
            UIButton.onClick.AddListener(CloseUI);
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

    #region Subscriptions
    private void ExitUIDetector_OnPointerClicked(object sender, EventArgs e)
    {
        HideUI();
    }
    #endregion
}
