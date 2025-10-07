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
    [SerializeField] private bool currentlyPlaying;
    [SerializeField] private bool isPaused;
    [Space]
    [SerializeField] private float storedTimeStamp;

    private AsyncOperationHandle<AudioClip>? currentHandle;

    private const float SAFE_TIME_AFTER_CLIP_END = 0.01f;

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
        StopAllCoroutines();
        ReleaseAudioClip();
    }

    private void Awake()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
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

        StartCoroutine(PlayLocalizedAudioClip());
    }
    #endregion

    #region Coroutines
    private IEnumerator PlayLocalizedAudioClip()
    {
        if (cutscenePanel == null) yield break;

        AudioClip audioClip;

        StopAudioClip();
        ReleaseAudioClip();

        currentHandle = LocalizationSettings.AssetDatabase.GetLocalizedAssetAsync<AudioClip>(cutscenePanel.audioLocalizationTable, cutscenePanel.audioLocalizationBinding);

        yield return currentHandle.Value;

        if (currentHandle.Value.Status == AsyncOperationStatus.Succeeded)
        {
            audioClip = currentHandle.Value.Result;

            if (isPaused) yield return new WaitUntil(() => !isPaused);
            PlayAudioClip(audioClip);   
        }
    }

    private IEnumerator PlayLocalizedAudioClipWithResume()
    {
        if (cutscenePanel == null) yield break;
        if (audioSource.clip == null) yield break;
        if (!currentlyPlaying) yield break;

        AudioClip audioClip;

        StopAudioClip();
        ReleaseAudioClip();

        currentHandle = LocalizationSettings.AssetDatabase.GetLocalizedAssetAsync<AudioClip>(cutscenePanel.audioLocalizationTable, cutscenePanel.audioLocalizationBinding);

        yield return currentHandle.Value;

        if (currentHandle.Value.Status == AsyncOperationStatus.Succeeded)
        {
            audioClip = currentHandle.Value.Result;

            if (isPaused) yield return new WaitUntil(() => !isPaused);
            PlayAudioClipFromTime(audioClip, storedTimeStamp);
        }
    }
    #endregion

    #region Utility Methods
    private void PlayAudioClip(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.loop = false;
        audioSource.Play();
        currentlyPlaying = true;
    }

    private void PlayAudioClipFromTime(AudioClip audioClip, float time)
    {
        audioSource.clip = audioClip;
        audioSource.time = Mathf.Min(time, audioClip.length - SAFE_TIME_AFTER_CLIP_END);
        audioSource.loop = false;
        audioSource.Play();
        currentlyPlaying = true;
    }

    public void StopAudioClip()
    {
        audioSource.Stop();
        currentlyPlaying = false;
    }

    public void UpdateTimeStamp()
    {
        if (!currentlyPlaying) return;
        if (isPaused) return;

        storedTimeStamp += Time.deltaTime;
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
        StopAllCoroutines();
        StartCoroutine(PlayLocalizedAudioClipWithResume());
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
