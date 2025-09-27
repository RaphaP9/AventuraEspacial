using System;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSelectionUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private UIPointerDetector exitUIDetector;

    [Header("UI Components")]
    [SerializeField] private Button languageSelectionButton;

    private const string SHOW_TRIGGER = "Show";
    private const string HIDE_TRIGGER = "Hide";

    private void OnEnable()
    {
        LanguageSelectionButtonUI.OnClicked += LanguageSelectionButtonUI_OnClicked;
        exitUIDetector.OnPointerClicked += ExitUIDetector_OnPointerClicked;
    }

    private void OnDisable()
    {
        LanguageSelectionButtonUI.OnClicked -= LanguageSelectionButtonUI_OnClicked;
        exitUIDetector.OnPointerClicked -= ExitUIDetector_OnPointerClicked;
    }

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        languageSelectionButton.onClick.AddListener(OpenUI);
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

    private void SelectLanguage(Language language)
    {
        LanguageManager.Instance.SetLanguage(language);
        HideUI();
    }

    #region Subscriptions
    private void LanguageSelectionButtonUI_OnClicked(object sender, LanguageSelectionButtonUI.OnClickedEventArgs e)
    {
        SelectLanguage(e.language);
    }

    private void ExitUIDetector_OnPointerClicked(object sender, EventArgs e)
    {
        HideUI();
    }
    #endregion
}
