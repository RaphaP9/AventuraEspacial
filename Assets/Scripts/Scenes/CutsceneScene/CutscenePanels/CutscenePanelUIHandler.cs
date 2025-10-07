using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using System.Collections;

public class CutscenePanelUIHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Image panelImage;
    [SerializeField] private TextMeshProUGUI sentenceText;
    [Space]
    [SerializeField] private CutscenePanelAnimationController animatorController;
    [SerializeField] private CutscenePanelSentenceAnimationController sentenceAnimationController;

    [Header("Runtime Filled")]
    [SerializeField] private CutscenePanel cutscenePanel;
    [SerializeField] private bool canSkipPanel;

    public bool CanSkipPanel => canSkipPanel;

    private void OnEnable()
    {
        LocalizationSettings.SelectedLocaleChanged += LocalizationSettings_SelectedLocaleChanged;
    }

    private void OnDisable()
    {
        LocalizationSettings.SelectedLocaleChanged -= LocalizationSettings_SelectedLocaleChanged;
    }

    public void SetPanel(CutscenePanel cutscenePanel)
    {
        this.cutscenePanel = cutscenePanel;
        panelImage.sprite = cutscenePanel.panelSprite;
        canSkipPanel = false;

        LocalizeSentenceByCurrentPanel();

        animatorController.Appear(cutscenePanel.transition);

        StartCoroutine(AppearSentenceCoroutine(cutscenePanel));
        StartCoroutine(HandleCanSkipPanelCoroutine(cutscenePanel));
    }

    private void LocalizeSentenceByCurrentPanel()
    {
        if (cutscenePanel == null) return;

        if (!cutscenePanel.hasSentence) return;
        sentenceText.text = LocalizationSettings.StringDatabase.GetLocalizedString(cutscenePanel.sentenceLocalizationTable, cutscenePanel.sentenceLocalizationBinding);
    }

    private IEnumerator AppearSentenceCoroutine(CutscenePanel cutscenePanel)
    {
        if (!cutscenePanel.hasSentence) yield break;

        yield return new WaitForSeconds(cutscenePanel.timeToAppearSentence);
        sentenceAnimationController.ShowSentence();
    }

    private IEnumerator HandleCanSkipPanelCoroutine(CutscenePanel cutscenePanel)
    {
        yield return new WaitForSeconds(cutscenePanel.timeToSkipPanel);
        canSkipPanel = true;
    }

    #region Subsctiptions
    private void LocalizationSettings_SelectedLocaleChanged(Locale locale)
    {
        LocalizeSentenceByCurrentPanel();
    }
    #endregion
}
