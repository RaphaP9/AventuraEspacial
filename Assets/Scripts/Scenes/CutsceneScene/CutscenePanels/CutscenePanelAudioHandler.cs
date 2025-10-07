using System.Collections;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;
using System;
using UnityEngine.Audio;

public class CutscenePanelAudioHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private AudioSource audioSource;

    [Header("Runtime Filled")]
    [SerializeField] private CutscenePanel cutscenePanel;
    [SerializeField] private bool currentlyActive;
    [SerializeField] private bool currentlyPlaying;
    [SerializeField] private bool isPaused;
    [Space]
    [SerializeField] private float storedTimeStamp;

    [Header("Debug")]
    [SerializeField] private bool debug;

    private AsyncOperationHandle<AudioClip>? currentHandle;

    private const float SAFE_TIME_AFTER_CLIP_END = 0.01f;

    private Coroutine localeChangeCoroutine;

    private void OnEnable()
    {
        LocalizationSettings.SelectedLocaleChanged += LocalizationSettings_SelectedLocaleChanged;
        PauseManager.OnGamePaused += PauseManager_OnGamePaused;
        PauseManager.OnGameResumed += PauseManager_OnGameResumed;
    }

    private void OnDisable()
    {
        LocalizationSettings.SelectedLocaleChanged -= LocalizationSettings_SelectedLocaleChanged;
        PauseManager.OnGamePaused -= PauseManager_OnGamePaused;
        PauseManager.OnGameResumed -= PauseManager_OnGameResumed;

        TerminateAudioHandler();
    }

    private void Awake()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        currentlyActive = false;
        currentlyPlaying = false;
        isPaused = false;
    }

    private void Update()
    {
        UpdateTimeStamp();
    }

    #region Public Methods
    public void PlayAudioLogic(CutscenePanel cutscenePanel)
    {
        this.cutscenePanel = cutscenePanel;
        currentlyActive = true;

        StartCoroutine(PlayLocalizedAudioClip());
    }

    public void TerminateAudioHandler()
    {
        currentlyActive = false;

        StopAllCoroutines();
        StopAudioClip();
        ReleaseAudioClip();
    }

    #endregion

    #region Coroutines
    private IEnumerator PlayLocalizedAudioClip()
    {
        if (cutscenePanel == null) yield break;

        StopAudioClip();
        ReleaseAudioClip();

        currentHandle = LocalizationSettings.AssetDatabase.GetLocalizedAssetAsync<AudioClip>(cutscenePanel.audioLocalizationTable, cutscenePanel.audioLocalizationBinding);

        yield return currentHandle.Value;

        if (currentHandle.Value.Status == AsyncOperationStatus.Succeeded)
        {
            audioSource.clip = currentHandle.Value.Result;
            if (isPaused) yield return new WaitUntil(() => !isPaused);
            PlayAudioClip();   
        }
        else
        {
            if (debug) Debug.Log("Async Operation Failed");
        }
    }

    private IEnumerator PlayLocalizedAudioClipWithResume()
    {
        if (cutscenePanel == null) yield break;

        StopAudioClip();
        ReleaseAudioClip();

        currentHandle = LocalizationSettings.AssetDatabase.GetLocalizedAssetAsync<AudioClip>(cutscenePanel.audioLocalizationTable, cutscenePanel.audioLocalizationBinding);

        yield return currentHandle.Value;

        if (currentHandle.Value.Status == AsyncOperationStatus.Succeeded)
        {
            audioSource.clip = currentHandle.Value.Result;
            if (isPaused) yield return new WaitUntil(() => !isPaused);
            PlayAudioClipFromTime(storedTimeStamp);
        }
        else
        {
            if (debug) Debug.Log("Async Operation Failed");
        }
    }
    #endregion

    #region Utility Methods
    public void UpdateTimeStamp()
    {
        if (!currentlyPlaying) return;
        if (isPaused) return;

        storedTimeStamp += Time.deltaTime;
    }

    private void PlayAudioClip()
    {
        audioSource.loop = false;
        audioSource.Play();
        currentlyPlaying = true;
    }

    private void PlayAudioClipFromTime(float time)
    {
        if(audioSource.clip == null)
        {
            audioSource.time = 0f;
        }
        else
        {
            audioSource.time = Mathf.Min(time, audioSource.clip.length - SAFE_TIME_AFTER_CLIP_END);
        }

        audioSource.loop = false;
        audioSource.Play();
        currentlyPlaying = true;
    }

    private void StopAudioClip()
    {
        audioSource.Stop();
        currentlyPlaying = false;
    }

    public void ReleaseAudioClip()
    {
        if (!currentHandle.HasValue) return;

        if (currentHandle.Value.IsValid())
        {
            Addressables.Release(currentHandle.Value);
        }

        currentHandle = null;
    }
    #endregion

    #region Subsctiptions
    private void LocalizationSettings_SelectedLocaleChanged(Locale locale)
    {
        if (!currentlyActive) return;

        if (localeChangeCoroutine != null) StopCoroutine(localeChangeCoroutine);
        localeChangeCoroutine = StartCoroutine(PlayLocalizedAudioClipWithResume());
    }

    private void PauseManager_OnGamePaused(object sender, System.EventArgs e)
    {
        audioSource.Pause();
        isPaused = true;
    }

    private void PauseManager_OnGameResumed(object sender, System.EventArgs e)
    {
        audioSource.UnPause();
        isPaused = false;
    }
    #endregion
}
