using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LanguageOptionsUIHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private LanguageInfoSO languageInfoSO;

    [SerializeField] private TextMeshProUGUI languageNameText;
    [SerializeField] private Image languageFlagImage;

    private void OnEnable()
    {
        LanguageManager.OnLanguageSet += LanguageManager_OnLanguageSet;
    }

    private void OnDisable()
    {
        LanguageManager.OnLanguageSet -= LanguageManager_OnLanguageSet;
    }

    private void Start()
    {
        UpdateLanguageNameText(LanguageManager.Instance.CurrentLanguage);
        UpdateLanguageFlag(LanguageManager.Instance.CurrentLanguage);
    }

    private void UpdateLanguageNameText(Language language)
    {
        LanguageAttributes attributes = languageInfoSO.GetLanguageAttributesByLanguage(language);

        if (attributes == null) return;

        languageNameText.text = attributes.languageName;
    }

    private void UpdateLanguageFlag(Language language)
    {
        LanguageAttributes attributes = languageInfoSO.GetLanguageAttributesByLanguage(language);

        if (attributes == null) return;

        languageFlagImage.sprite = attributes.languageFlag;
    }

    #region Subscriptions
    private void LanguageManager_OnLanguageSet(object sender, LanguageManager.OnLanguageEventArgs e)
    {
        UpdateLanguageNameText(e.language);
        UpdateLanguageFlag(e.language);
    }
    #endregion
}
