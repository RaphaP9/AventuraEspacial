using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSceneButtonSingleUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private LanguageInfoSO languageInfoSO;
    [SerializeField] private Button button;
    [SerializeField] private Animator animator;

    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI languageNameText;
    [SerializeField] private Image languageFlagImage;

    [Header("Settings")]
    [SerializeField] private Language language;

    [Header("Runtime Filled")]
    [SerializeField] private bool isSelected;

    private const string SELECT_TRIGGER = "Select";
    private const string DESELECT_TRIGGER = "Deselect";

    private const string SELECTED_ANIMATION_NAME = "Selected";
    private const string DESELECTED_ANIMATION_NAME = "Deselected";

    public static event EventHandler<OnClickedEventArgs> OnClicked;

    public class OnClickedEventArgs : EventArgs
    {
        public Language language;
    }

    private void OnEnable()
    {
        LanguageSelectionSceneUI.OnLanguageSelected += LanguageSelectionSceneUI_OnLanguageSelected;
    }

    private void OnDisable()
    {
        LanguageSelectionSceneUI.OnLanguageSelected -= LanguageSelectionSceneUI_OnLanguageSelected;
    }

    private void Awake()
    {
        InitializeButtonsListeners();
        InitializeVariables();
    }

    private void Start()
    {
        SetLanguageNameText();
        SetLanguageFlag();
    }

    private void InitializeButtonsListeners()
    {
        button.onClick.AddListener(SelectLanguage);
    }

    private void InitializeVariables()
    {
        isSelected = false;
    }

    private void SelectLanguage()
    {
        OnClicked?.Invoke(this, new OnClickedEventArgs { language = language });
    }

    private void SetLanguageNameText()
    {
        LanguageAttributes attributes = languageInfoSO.GetLanguageAttributesByLanguage(language);

        if (attributes == null) return;

        languageNameText.text = attributes.languageName;
    }

    private void SetLanguageFlag()
    {
        LanguageAttributes attributes = languageInfoSO.GetLanguageAttributesByLanguage(language);

        if (attributes == null) return;

        languageFlagImage.sprite = attributes.languageFlag;
    }

    #region Animations
    public void SelectButton()
    {
        if (isSelected) return;
        
        animator.ResetTrigger(DESELECT_TRIGGER);
        animator.SetTrigger(SELECT_TRIGGER);

        isSelected = true;
    }

    public void DeselectButton()
    {
        if (!isSelected) return;

        animator.ResetTrigger(SELECT_TRIGGER);
        animator.SetTrigger(DESELECT_TRIGGER);

        isSelected = false;
    }

    public void SelectButtonInstantly()
    {
        if (isSelected) return;

        animator.ResetTrigger(DESELECT_TRIGGER);
        animator.ResetTrigger(SELECT_TRIGGER);

        animator.Play(SELECTED_ANIMATION_NAME);

        isSelected = true;
    }

    public void DeselectButtonInstantly()
    {
        if (!isSelected) return;

        animator.ResetTrigger(DESELECT_TRIGGER);
        animator.ResetTrigger(SELECT_TRIGGER);

        animator.Play(DESELECTED_ANIMATION_NAME);

        isSelected = false;
    }
    #endregion

    #region Subscriptions
    private void LanguageSelectionSceneUI_OnLanguageSelected(object sender, LanguageSelectionSceneUI.OnLanguageSelectedEventArgs e)
    {
        if(e.language == language)
        {
            if (e.instantly) SelectButtonInstantly();
            else SelectButton();
        }
        else
        {
            if (e.instantly) DeselectButtonInstantly();
            else DeselectButton();
        }
    }
    #endregion
}
