using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;

public class CinematicSceneManager : MonoBehaviour
{
    public static CinematicSceneManager Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private CinematicSO cinematicSO;
    [SerializeField] private VideoPlayer videoPlayer;

    [Header("Settings")]
    [SerializeField] private TransitionType transitionType;
    [SerializeField] private string nextScene;

    [Header("Debug")]
    [SerializeField] private bool debug;

    [Header("Runtime Filled")]
    [SerializeField] private VideoClip localizedVideo;

    private const float SCENE_FADE_OUT_TIME = 0.2f;
    private const float MIN_SECURE_CINEMATIC_DURATION = 1f;

    private bool shouldSkipCinematic = false;
    private bool isReloading = false;

    private void OnEnable()
    {
        LocalizationSettings.SelectedLocaleChanged += LocalizationSettings_SelectedLocaleChanged;
    }

    private void OnDisable()
    {
        LocalizationSettings.SelectedLocaleChanged -= LocalizationSettings_SelectedLocaleChanged;
    }

    private void Awake()
    {
        SetSingleton();
    }

    private async void Start()
    {
        await PlayVideoFromStart();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There is more than one CinematicSceneManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private async Task PlayVideoFromStart()
    {
        bool success = await LoadLocalizedVideo();
        if (!success) return;

        StartCoroutine(CinematicCoroutine(localizedVideo));
    }

    private async Task<bool> LoadLocalizedVideo() //Returns True if Success, otherwise returns false. It loads automatically to the field "localizedVideo"
    {
        AsyncOperationHandle<VideoClip> handle = LocalizationSettings.AssetDatabase.GetLocalizedAssetAsync<VideoClip>(cinematicSO.localizationTable, cinematicSO.localizationBinding);

        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            localizedVideo = handle.Result;
            return true;
        }
        else
        {
            if(debug) Debug.LogError($"Failed to load localized video. Table: {cinematicSO.localizationTable}, Video: {cinematicSO.localizationBinding}");
            return false;
        }
    }
    private IEnumerator CinematicCoroutine(VideoClip videoClip)
    {
        videoPlayer.clip = videoClip;

        float clipTotalDuration = videoPlayer.frameCount / (float)videoPlayer.frameRate;
        float clipFixedDuration = clipTotalDuration - SCENE_FADE_OUT_TIME;

        clipFixedDuration = clipFixedDuration < MIN_SECURE_CINEMATIC_DURATION ? MIN_SECURE_CINEMATIC_DURATION : clipFixedDuration;

        float elapsedTime = 0;

        videoPlayer.Stop();
        videoPlayer.Play();

        while (elapsedTime <= clipFixedDuration)
        {
            if (shouldSkipCinematic) break;

            if (isReloading)
            {
                yield return null;
                continue;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        shouldSkipCinematic = false;

        ScenesManager.Instance.TransitionLoadTargetScene(nextScene, transitionType);
    }

    public void SkipCinematic() => shouldSkipCinematic = true;

    private async void ReloadVideo()
    {
        if(isReloading) return;

        isReloading = true;

        double currentTime = videoPlayer.time;
        videoPlayer.Stop();

        bool success = await LoadLocalizedVideo();
        if (!success) return;

        videoPlayer.clip = localizedVideo;

        StartCoroutine(RestoreTimeWhenReady(currentTime));
    }

    private IEnumerator RestoreTimeWhenReady(double time)
    {
        videoPlayer.Prepare();

        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

        videoPlayer.time = time;
        videoPlayer.Play();

        isReloading = false;
    }

    #region Subscriptions
    private void LocalizationSettings_SelectedLocaleChanged(UnityEngine.Localization.Locale obj)
    {
        ReloadVideo();
    }
    #endregion
}
