using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator optionsUIAnimator;
    [SerializeField] private ScrollRect scrollRect;

    [Header("UI Components")]
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button closeButton;

    public static event EventHandler OnOptionsUIOpen;
    public static event EventHandler OnOptionsUIClose;

    private const string SHOW_TRIGGER = "Show";
    private const string HIDE_TRIGGER = "Hide";

    private const float TOP_SCROLL_RECT_VERTICAL_NORMALIZED_POSITION = 1f;


    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        optionsButton.onClick.AddListener(OpenUI);
        closeButton.onClick.AddListener(CloseUI);
    }

    public void OpenUI()
    {
        ResetScrollRectToTop();

        ShopOptionsUI();
        OnOptionsUIOpen?.Invoke(this, EventArgs.Empty);
    }

    private void CloseUI()
    {
        HideOptionsUI();
        OnOptionsUIClose?.Invoke(this, EventArgs.Empty);
    }

    private void ResetScrollRectToTop()
    {
        scrollRect.verticalNormalizedPosition = TOP_SCROLL_RECT_VERTICAL_NORMALIZED_POSITION;
    }

    #region Animation Methods
    public void ShopOptionsUI()
    {
        optionsUIAnimator.ResetTrigger(HIDE_TRIGGER);
        optionsUIAnimator.SetTrigger(SHOW_TRIGGER);
    }

    public void HideOptionsUI()
    {
        optionsUIAnimator.ResetTrigger(SHOW_TRIGGER);
        optionsUIAnimator.SetTrigger(HIDE_TRIGGER);
    }
    #endregion
}
