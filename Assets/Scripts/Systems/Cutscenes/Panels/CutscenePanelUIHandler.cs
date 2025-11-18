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
    [SerializeField] private CutscenePanelSentenceAnimationController sentenceAnimationController;
    [SerializeField] private CutscenePanelAudioHandler audioHandler;

    [Header("General Settings")]
    [SerializeField, Range(0f, 5f)] private float timeToSkipPanel;

    [Header("Sentence Settings")]
    [SerializeField] private bool hasSentence;
    [SerializeField] private string sentenceLocalizationTable;
    [SerializeField] private string sentenceLocalizationBinding;
    [SerializeField, ColorUsage(true, true)] private Color sentenceColor;
    [Space]
    [SerializeField, Range(0f, 5f)] private float timeToAppearSentence;

    [Header("Audio Settings")]
    [SerializeField] private bool hasAudio;
    [SerializeField] private string audioLocalizationTable;
    [SerializeField] private string audioLocalizationBinding;
    [Space]
    [SerializeField, Range(0f, 5f)] public float timeToPlayAudio;

    [Header("Runtime Filled")]
    [SerializeField] private CutscenePanel cutscenePanel;
    [SerializeField] private bool canSkipPanel;

    #region Properties
    public bool CanSkipPanel => canSkipPanel;

    public bool HasSentence => hasSentence;
    public string SentenceLocalizationTable => sentenceLocalizationTable;
    public string SentenceLocalizationBinding => sentenceLocalizationBinding;
    public Color SentenceColor => sentenceColor;

    public bool HasAudio => hasAudio;
    public string AudioLocalizationTable => audioLocalizationTable;
    public string AudioLocalizationBinding => audioLocalizationBinding;
    #endregion

    public void SetPanel(CutscenePanel cutscenePanel)
    {
        this.cutscenePanel = cutscenePanel;
        canSkipPanel = false;

        StartCoroutine(HandleCanSkipPanelCoroutine());
        StartCoroutine(AppearSentenceCoroutine());
        StartCoroutine(HandleAudioPlayCoroutine());
    }

    public void DisposePanel()
    {
        audioHandler.TerminateAudioHandler();
    }

    private IEnumerator HandleCanSkipPanelCoroutine()
    {
        yield return new WaitForSeconds(timeToSkipPanel);
        canSkipPanel = true;
    }

    private IEnumerator AppearSentenceCoroutine()
    {
        if (!hasSentence) yield break;

        yield return new WaitForSeconds(timeToAppearSentence);
        sentenceAnimationController.ShowSentenceLogic();
    }

    private IEnumerator HandleAudioPlayCoroutine()
    {
        if (!hasAudio) yield break;

        yield return new WaitForSeconds(timeToPlayAudio);
        audioHandler.PlayAudioLogic();
    }
}
