using UnityEngine;

public class LandmarkUIAnimationController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private LandmarkUI landmarkUI;
    [SerializeField] private Animator animator;

    private const string NOT_UNLOCKED_ANIMATION_NAME = "NotUnlocked";
    private const string UNLOCKED_ANIMATION_NAME = "Unlocked";
    private const string CLAIMED_ANIMATION_NAME = "Claimed";

    private const string CLAIM_TRIGGER = "Claim";

    private void OnEnable()
    {
        landmarkUI.OnLandmarkInitialized += LandmarkUI_OnLandmarkInitialized;
        landmarkUI.OnLandmarkClaimed += LandmarkUI_OnLandmarkClaimed;
    }

    private void OnDisable()
    {
        landmarkUI.OnLandmarkInitialized -= LandmarkUI_OnLandmarkInitialized;
        landmarkUI.OnLandmarkClaimed -= LandmarkUI_OnLandmarkClaimed;
    }

    private void InstantnNotUnlock()
    {
        animator.ResetTrigger(CLAIM_TRIGGER);
        animator.Play(NOT_UNLOCKED_ANIMATION_NAME);
    }

    private void InstantUnlock()
    {
        animator.ResetTrigger(CLAIM_TRIGGER);
        animator.Play(UNLOCKED_ANIMATION_NAME);
    }

    private void InstantClaim()
    {
        animator.ResetTrigger(CLAIM_TRIGGER);
        animator.Play(CLAIMED_ANIMATION_NAME);
    }

    private void Claim()
    {
        animator.SetTrigger(CLAIM_TRIGGER);
    }

    #region Subsscriptions
    private void LandmarkUI_OnLandmarkInitialized(object sender, LandmarkUI.OnLandmarkStateEventArgs e)
    {
        switch (e.landmarkState)
        {
            case LandmarkState.NotUnlocked:
                InstantnNotUnlock();
                break;
            case LandmarkState.Unlocked:
                InstantUnlock();
                break;
            case LandmarkState.Claimed:
                InstantClaim();
                break;
        }
    }

    private void LandmarkUI_OnLandmarkClaimed(object sender, System.EventArgs e)
    {
        Claim();
    }
    #endregion
}
