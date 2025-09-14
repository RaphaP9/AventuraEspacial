using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
public class LanguageSelectionSceneUI : MonoBehaviour
{
    [Header("Language Settings")]
    [SerializeField] private bool enableStartingSelectedLanguage;
    [SerializeField] private Language startingSelectedLanguage;

    [Header("Confirm Components")]
    [SerializeField] private Button confirmButton;

    [Header("Confirm Settings")]
    [SerializeField] private string confirmScene;
    [SerializeField] private TransitionType confirmTransitionType;

    [Header("Runtime Filled")]
    [SerializeField] private bool languageHasBeenSelected;
    [SerializeField] private Language currentSelectedLanguage;

    public static event EventHandler<OnLanguageSelectedEventArgs> OnLanguageSelected;

    public class OnLanguageSelectedEventArgs : EventArgs
    {
        public Language language;
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

        SelectLanguage(startingSelectedLanguage, true);
    }

    private void HandleLanguageSelection(Language language)
    {
        if (!languageHasBeenSelected)
        {
            SelectLanguage(language, false);
        }
        else if(language != currentSelectedLanguage) 
        {
            SelectLanguage(language, false);
        }
    }

    private void SelectLanguage(Language language, bool instantly)
    {
        languageHasBeenSelected = true;
        currentSelectedLanguage = language;
        LanguageManager.Instance.SetLanguage(language);

        OnLanguageSelected?.Invoke(this, new OnLanguageSelectedEventArgs { language = language, instantly = instantly });

        EnableConfirmButton();
    }


    private void DisableConfirmButton() => confirmButton.interactable = false;
    private void EnableConfirmButton() => confirmButton.interactable = true;

    private void LoadConfirmScene()
    {
        ScenesManager.Instance.TransitionLoadTargetScene(confirmScene,confirmTransitionType);
    }

    #region Subscriptions

    private void LanguageSceneButtonSingleUI_OnClicked(object sender, LanguageSceneButtonSingleUI.OnClickedEventArgs e)
    {
        HandleLanguageSelection(e.language);
    }

    #endregion

}
