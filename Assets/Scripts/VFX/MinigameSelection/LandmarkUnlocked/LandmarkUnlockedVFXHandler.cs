using UnityEngine;
using UnityEngine.VFX;

public class LandmarkUnlockedVFXHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private LandmarkUI landmarkUI;
    [SerializeField] private LandmarkUIAnimationController landmarkUIAnimationController;
    [SerializeField] private VisualEffect visualEffect;

    [Header("Runtime Filled")]
    [SerializeField] private bool VFXEnabled;

    private void OnEnable()
    {
        landmarkUI.OnLandmarkInitialized += LandmarkUI_OnLandmarkInitialized;
        landmarkUI.OnLandmarkClaimed += LandmarkUI_OnLandmarkClaimed;
        landmarkUIAnimationController.OnAnimationRest += LandmarkUIAnimationController_OnAnimationRest;
    }

    private void OnDisable()
    {
        landmarkUI.OnLandmarkInitialized -= LandmarkUI_OnLandmarkInitialized;
        landmarkUI.OnLandmarkClaimed -= LandmarkUI_OnLandmarkClaimed;
        landmarkUIAnimationController.OnAnimationRest -= LandmarkUIAnimationController_OnAnimationRest;
    }

    private void PlayVFX() => visualEffect.Play();
    private void StopVFX() => visualEffect.Stop();

    #region Subscriptions
    private void LandmarkUI_OnLandmarkInitialized(object sender, LandmarkUI.OnLandmarkStateEventArgs e)
    {
        if (e.landmarkState == LandmarkState.Unlocked) VFXEnabled = true;
        else VFXEnabled = false;
    }

    private void LandmarkUI_OnLandmarkClaimed(object sender, System.EventArgs e)
    {
        VFXEnabled = false;
        StopVFX();
    }

    private void LandmarkUIAnimationController_OnAnimationRest(object sender, System.EventArgs e)
    {
        if (!VFXEnabled) return;
        PlayVFX();
    }
    #endregion
}
