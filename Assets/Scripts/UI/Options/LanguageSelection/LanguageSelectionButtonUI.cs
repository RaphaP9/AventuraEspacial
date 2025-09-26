using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LanguageSelectionButtonUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private LanguageInfoSO languageInfoSO;
    [SerializeField] private Button button;

    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI languageNameText;
    [SerializeField] private Image languageFlagImage;

    [Header("Settings")]
    [SerializeField] private Language language;

    public static event EventHandler<OnClickedEventArgs> OnClicked;

    public class OnClickedEventArgs : EventArgs
    {
        public Language language;
    }

    private void Awake()
    {
        InitializeButtonsListeners();
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

    private void SelectLanguage()
    {
        OnClicked?.Invoke(this, new OnClickedEventArgs { language = language });
    }
}
