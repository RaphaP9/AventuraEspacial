using UnityEngine;

public class CutsceneSceneBGMHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private AudioSource cutsceneBGMAudioSource;

    [Header("Settings")]
    [SerializeField, Range(0.25f, 1f)] private float cutscenesVolumeFadeInTime = 0.3f;
    [SerializeField, Range(0.25f, 1f)] private float cutscenesVolumeFadeOutTime = 0.3f;

    private void OnEnable()
    {
        CutsceneSceneUIHandler.OnCutscenePlay += CutsceneSceneUIHandler_OnCutscenePlay;
        CutsceneSceneUIHandler.OnCutsceneConclude += CutsceneSceneUIHandler_OnCutsceneConclude;
    }

    private void OnDisable()
    {
        CutsceneSceneUIHandler.OnCutscenePlay -= CutsceneSceneUIHandler_OnCutscenePlay;
        CutsceneSceneUIHandler.OnCutsceneConclude -= CutsceneSceneUIHandler_OnCutsceneConclude;
    }

    private void PlayCutsceneBGM(AudioClip cutsceneBGM)
    {
        cutsceneBGMAudioSource.Stop();

        if (cutsceneBGM == null) return;

        cutsceneBGMAudioSource.clip = cutsceneBGM;
        cutsceneBGMAudioSource.Play();
    }

    #region Subscriptions
    private void CutsceneSceneUIHandler_OnCutscenePlay(object sender, CutsceneSceneUIHandler.OnCutsceneEventArgs e)
    {
        CutscenesVolumeFadeManager.Instance.FadeInVolume(cutscenesVolumeFadeInTime);
        PlayCutsceneBGM(e.cutsceneSO.cutsceneBGM);
    }

    private void CutsceneSceneUIHandler_OnCutsceneConclude(object sender, CutsceneSceneUIHandler.OnCutsceneEventArgs e)
    {
        CutscenesVolumeFadeManager.Instance.FadeOutVolume(cutscenesVolumeFadeOutTime);
    }
    #endregion
}
