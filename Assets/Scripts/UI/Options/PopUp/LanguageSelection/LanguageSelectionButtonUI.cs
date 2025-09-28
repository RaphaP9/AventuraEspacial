using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LanguageSelectionButtonUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private LanguageSettingSO languageSettingSO;
    [SerializeField] private Button button;

    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI languageNameText;
    [SerializeField] private Image languageFlagImage;

    [Header("Settings")]
    [SerializeField] private Language language;

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
        languageNameText.text = languageSettingSO.languageName;
    }

    private void SetLanguageFlag()
    {
        languageFlagImage.sprite = languageSettingSO.languageFlag;
    }

    private void SelectLanguage()
    {
        LanguageManager.Instance.SetLanguage(languageSettingSO);
    }
}
