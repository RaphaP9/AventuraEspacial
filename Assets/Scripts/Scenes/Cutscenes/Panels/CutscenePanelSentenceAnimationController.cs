using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class CutscenePanelSentenceAnimationController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI sentenceText;

    [Header("Runtime Filled")]
    [SerializeField] private CutscenePanel cutscenePanel;

    private const string SHOW_TRIGGER = "Show";

    private void OnEnable()
    {
        LocalizationSettings.SelectedLocaleChanged += LocalizationSettings_SelectedLocaleChanged;
    }

    private void OnDisable()
    {
        LocalizationSettings.SelectedLocaleChanged -= LocalizationSettings_SelectedLocaleChanged;
    }

    public void ShowSentenceLogic(CutscenePanel cutscenePanel)
    {
        this.cutscenePanel = cutscenePanel;

        LocalizeSentenceByCurrentPanel();
        ShowSentence();
    }

    private void ShowSentence()
    {
        animator.SetTrigger(SHOW_TRIGGER);
    }

    private void LocalizeSentenceByCurrentPanel()
    {
        if (cutscenePanel == null) return;

        if (!cutscenePanel.hasSentence) return;
        sentenceText.text = LocalizationSettings.StringDatabase.GetLocalizedString(cutscenePanel.sentenceLocalizationTable, cutscenePanel.sentenceLocalizationBinding);
    }

    #region Subsctiptions
    private void LocalizationSettings_SelectedLocaleChanged(Locale locale)
    {
        LocalizeSentenceByCurrentPanel();
    }
    #endregion
}
