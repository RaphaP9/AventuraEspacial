using UnityEngine;

public class SilhouetteAnimationController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;

    private const string APPEAR_ANIMATION_NAME = "Appear";

    private const string MATCH_ANIMATION_NAME = "Match";
    private const string FAIL_ANIMATION_NAME = "Fail";

    public void PlayAppearAnimation() => animator.Play(APPEAR_ANIMATION_NAME);
    public void PlayMatchAnimation() => animator.Play(MATCH_ANIMATION_NAME);
    public void PlayFailAnimation() => animator.Play(FAIL_ANIMATION_NAME);
}
