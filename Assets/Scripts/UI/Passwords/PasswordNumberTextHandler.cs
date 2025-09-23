using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class PasswordNumberTextHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI numberText;

    [Header("Runtime Filled")]
    [SerializeField] private PasswordNumberSO passwordNumberSO;

    public PasswordNumberSO PasswordNumberSO => passwordNumberSO;

    private void OnEnable()
    {
        LocalizationSettings.SelectedLocaleChanged += LocalizationSettings_SelectedLocaleChanged;
    }

    private void OnDisable()
    {
        LocalizationSettings.SelectedLocaleChanged -= LocalizationSettings_SelectedLocaleChanged;
    }

    public void SetNumberText(PasswordNumberSO passwordNumberSO)
    {
        this.passwordNumberSO = passwordNumberSO;

        string localizedString = LocalizationSettings.StringDatabase.GetLocalizedString(passwordNumberSO.localizationTable, passwordNumberSO.localizationBinding);
        SetText(localizedString);
    }

    private void SelfUpdateText()
    {
        if (passwordNumberSO == null) return;

        string localizedString = LocalizationSettings.StringDatabase.GetLocalizedString(passwordNumberSO.localizationTable, passwordNumberSO.localizationBinding);
        SetText(localizedString);
    }

    private void SetText(string text) => numberText.text = text;

    #region Subscriptions
    private void LocalizationSettings_SelectedLocaleChanged(UnityEngine.Localization.Locale obj)
    {
        SelfUpdateText();
    }
    #endregion
}
