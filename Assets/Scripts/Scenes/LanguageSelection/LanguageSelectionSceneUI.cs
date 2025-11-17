using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
public class LanguageSelectionSceneUI : MonoBehaviour
{
    [Header("Language Settings")]
    [SerializeField] private bool enableStartingSelectedLanguage;
    [SerializeField] private LanguageSettingSO startingSelectedLanguageSetting;

    [Header("Confirm Components")]
    [SerializeField] private Button confirmButton;

    [Header("Confirm Settings")]
    [SerializeField] private string confirmScene;
    [SerializeField] private TransitionType confirmTransitionType;

    [Header("Runtime Filled")]
    [SerializeField] private bool languageHasBeenSelected;
    [SerializeField] private LanguageSettingSO currentSelectedLanguageSetting;

    public static event EventHandler<OnLanguageSelectedEventArgs> OnLanguageSelected;

    public class OnLanguageSelectedEventArgs : EventArgs
    {
        public LanguageSettingSO languageSetting;
        public bool instantly;
    }

    private void OnEnable()
    {
        LanguageSceneButtonSingleUI.OnClicked += LanguageSceneButtonSingleUI_OnClicked;
    }

    private void OnDisable()
    {
        LanguageSceneButtonSingleUI.OnClicked -= LanguageSceneButtonSingleUI_OnClicked;
    }

    private void Awake()
    {
        DisableConfirmButton();
        InitializeButtonsListeners();
        InitializeVariables();
    }

    private void Start()
    {
        HandleStartingLanguageSelection();
    }

    private void InitializeButtonsListeners()
    {
        confirmButton.onClick.AddListener(LoadConfirmScene);
    }

    private void InitializeVariables()
    {
        languageHasBeenSelected = false;
    }

    private void HandleStartingLanguageSelection()
    {
        if (!enableStartingSelectedLanguage) return;

        SelectLanguage(startingSelectedLanguageSetting, true);
    }

    private void HandleLanguageSelection(LanguageSettingSO languageSetting)
    {
        if (!languageHasBeenSelected)
        {
            SelectLanguage(languageSetting, false);
        }
        else if(languageSetting != currentSelectedLanguageSetting) 
        {
            SelectLanguage(languageSetting, false);
        }
    }

    private void SelectLanguage(LanguageSettingSO languageSetting, bool instantly)
    {
        languageHasBeenSelected = true;
        currentSelectedLanguageSetting = languageSetting;
        LanguageManager.Instance.SetLanguage(languageSetting);

        OnLanguageSelected?.Invoke(this, new OnLanguageSelectedEventArgs { languageSetting = languageSetting, instantly = instantly });

        EnableConfirmButton();
    }


    private void EnableConfirmButton() => confirmButton.interactable = true;
    private void DisableConfirmButton() => confirmButton.interactable = false;

    private void LoadConfirmScene()
    {
        ScenesManager.Instance.TransitionLoadTargetScene(confirmScene,confirmTransitionType);
    }

    #region Subscriptions

    private void LanguageSceneButtonSingleUI_OnClicked(object sender, LanguageSceneButtonSingleUI.OnClickedEventArgs e)
    {
        HandleLanguageSelection(e.languageSetting);
    }

    #endregion

}
