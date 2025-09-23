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

    public void SetNumberText(PasswordNumberSO passwordNumberSO)
    {
        this.passwordNumberSO = passwordNumberSO;

        string localizedString = LocalizationSettings.StringDatabase.GetLocalizedString(passwordNumberSO.localizationTable, passwordNumberSO.localizationBinding);
        SetText(localizedString);
    }

    public void SetText(string text) => numberText.text = text;
}
