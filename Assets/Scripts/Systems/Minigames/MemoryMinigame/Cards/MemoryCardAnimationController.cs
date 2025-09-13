using System.Collections;
using UnityEngine;

public class MemoryCardAnimationController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;

    private const string APPEAR_ANIMATION_NAME = "Appear";
    private const string DISAPPEAR_ANIMATION_NAME = "Disappear";

    private const string FLIP_REVEAL_ANIMATION_NAME = "FlipReveal";
    private const string FLIP_COVER_ANIMATION_NAME = "FlipCover";

    private const string MATCH_ANIMATION_NAME = "Match";
    private const string FAIL_ANIMATION_NAME = "Fail";

    private const float FLIP_REVEAL_ANIMATION_DURATION = 0.5f;

    public void PlayAppearAnimation() => animator.Play(APPEAR_ANIMATION_NAME);
    public void PlayDisappearAnimation() => animator.Play(DISAPPEAR_ANIMATION_NAME);
    public void PlayRevealAnimation() => animator.Play(FLIP_REVEAL_ANIMATION_NAME);
    public void PlayCoverAnimation() => animator.Play(FLIP_COVER_ANIMATION_NAME);
    public void PlayMatchAnimation() => animator.Play(MATCH_ANIMATION_NAME);
    public void PlayFailAnimation() => animator.Play(FAIL_ANIMATION_NAME);
}
