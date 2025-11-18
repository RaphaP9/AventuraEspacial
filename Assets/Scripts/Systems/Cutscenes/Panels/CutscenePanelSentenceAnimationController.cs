using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class CutscenePanelSentenceAnimationController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI sentenceText;
    [Space]
    [SerializeField] private CutscenePanelUIHandler cutscenePanelUIHandler;

    private const string SHOW_TRIGGER = "Show";

    private void OnEnable()
    {
        LocalizationSettings.SelectedLocaleChanged += LocalizationSettings_SelectedLocaleChanged;
    }

    private void OnDisable()
    {
        LocalizationSettings.SelectedLocaleChanged -= LocalizationSettings_SelectedLocaleChanged;
    }

    public void ShowSentenceLogic()
    {
        SetSentenceColor();
        LocalizeSentence();
        ShowSentence();
    }

    private void ShowSentence()
    {
        animator.SetTrigger(SHOW_TRIGGER);
    }

    private void SetSentenceColor() => sentenceText.color = cutscenePanelUIHandler.SentenceColor;
    private void LocalizeSentence() => sentenceText.text = LocalizationSettings.StringDatabase.GetLocalizedString(cutscenePanelUIHandler.SentenceLocalizationTable, cutscenePanelUIHandler.SentenceLocalizationBinding);

    #region Subsctiptions
    private void LocalizationSettings_SelectedLocaleChanged(Locale locale)
    {
        LocalizeSentence();
    }
    #endregion
}
