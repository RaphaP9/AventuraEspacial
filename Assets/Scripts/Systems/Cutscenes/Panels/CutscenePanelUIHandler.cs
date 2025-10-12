using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CutscenePanelUIHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Image panelImage;
    [SerializeField] private TextMeshProUGUI sentenceText;
    [Space]
    [SerializeField] private CutscenePanelAnimationController animatorController;
    [SerializeField] private CutscenePanelSentenceAnimationController sentenceAnimationController;
    [SerializeField] private CutscenePanelAudioHandler audioHandler;

    [Header("Runtime Filled")]
    [SerializeField] private CutscenePanel cutscenePanel;
    [SerializeField] private bool canSkipPanel;

    public bool CanSkipPanel => canSkipPanel;

    public void SetPanel(CutscenePanel cutscenePanel)
    {
        this.cutscenePanel = cutscenePanel;
        panelImage.sprite = cutscenePanel.panelSprite;
        canSkipPanel = false;

        animatorController.Appear(cutscenePanel.transition);

        StartCoroutine(HandleCanSkipPanelCoroutine(cutscenePanel));
        StartCoroutine(AppearSentenceCoroutine(cutscenePanel));
        StartCoroutine(HandleAudioPlayCoroutine(cutscenePanel));
    }

    public void DisposePanel()
    {
        audioHandler.TerminateAudioHandler();
    }

    private IEnumerator HandleCanSkipPanelCoroutine(CutscenePanel cutscenePanel)
    {
        yield return new WaitForSeconds(cutscenePanel.timeToSkipPanel);
        canSkipPanel = true;
    }

    private IEnumerator AppearSentenceCoroutine(CutscenePanel cutscenePanel)
    {
        if (!cutscenePanel.hasSentence) yield break;

        yield return new WaitForSeconds(cutscenePanel.timeToAppearSentence);
        sentenceAnimationController.ShowSentenceLogic(cutscenePanel);
    }

    private IEnumerator HandleAudioPlayCoroutine(CutscenePanel cutscenePanel)
    {
        if (!cutscenePanel.hasAudio) yield break;

        yield return new WaitForSeconds(cutscenePanel.timeToPlayAudio);
        audioHandler.PlayAudioLogic(cutscenePanel);
    }
}
