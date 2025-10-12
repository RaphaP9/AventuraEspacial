using UnityEngine;

public class CutscenePanelAnimationController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;

    private const string INSTANT_APPEAR_ANIMATION_NAME = "InstantAppear";
    private const string FADE_APPEAR_ANIMATION_NAME = "FadeAppear";

    public void Appear(CutscenePanelTransition transition)
    {
        string animationToPlay = GetAnimationNameByTransition(transition);
        animator.Play(animationToPlay);
    }

    private string GetAnimationNameByTransition(CutscenePanelTransition transition)
    {
        switch (transition)
        {
            case CutscenePanelTransition.Instant:
            default:
                return INSTANT_APPEAR_ANIMATION_NAME;
            case CutscenePanelTransition.Fade:
                return FADE_APPEAR_ANIMATION_NAME;
        }
    }
}
