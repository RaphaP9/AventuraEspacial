using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LanguageOptionsUIHandler : MonoBehaviour
{
    [Header("Components")]
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
        UpdateLanguageNameText(LanguageManager.Instance.CurrentLanguageSetting);
        UpdateLanguageFlag(LanguageManager.Instance.CurrentLanguageSetting);
    }

    private void UpdateLanguageNameText(LanguageSettingSO languageSetting)
    {
        languageNameText.text = languageSetting.languageName;
    }

    private void UpdateLanguageFlag(LanguageSettingSO languageSetting)
    {
        languageFlagImage.sprite = languageSetting.languageFlag;
    }

    #region Subscriptions
    private void LanguageManager_OnLanguageSet(object sender, LanguageManager.OnLanguageEventArgs e)
    {
        UpdateLanguageNameText(e.languageSetting);
        UpdateLanguageFlag(e.languageSetting);
    }
    #endregion
}
