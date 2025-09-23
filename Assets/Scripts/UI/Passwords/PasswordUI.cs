using UnityEngine;
using UnityEngine.UI;
using System;

public class PasswordUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;

    [Header("UI Components")]
    [SerializeField] private Button openButton;
    [SerializeField] private Button closeButton;

    private const string SHOW_TRIGGER = "Show";
    private const string HIDE_TRIGGER = "Hide";
    private const string ACCESS_TRIGGER = "Access";

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        openButton.onClick.AddListener(OpenUI);
        closeButton.onClick.AddListener(CloseUI);
    }

    #region Animations
    public void ShowUI()
    {
        animator.ResetTrigger(HIDE_TRIGGER);
        animator.ResetTrigger(ACCESS_TRIGGER);

        animator.SetTrigger(SHOW_TRIGGER);
    }

    public void HideUI()
    {
        animator.ResetTrigger(SHOW_TRIGGER);
        animator.ResetTrigger(ACCESS_TRIGGER);

        animator.SetTrigger(HIDE_TRIGGER);
    }

    public void AccessUI()
    {
        animator.ResetTrigger(SHOW_TRIGGER);
        animator.ResetTrigger(HIDE_TRIGGER);

        animator.SetTrigger(ACCESS_TRIGGER);
    }
    #endregion

    private void OpenUI()
    {
        ShowUI();
    }

    private void CloseUI()
    {
        HideUI();
    }
}
