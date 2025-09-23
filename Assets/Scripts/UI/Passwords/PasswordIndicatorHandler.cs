using UnityEngine;

public class PasswordIndicatorHandler : MonoBehaviour
{
    [Header("Componets")]
    [SerializeField] private Animator animator;

    private const string FILL_TRIGGER = "Fill";
    private const string UNFILL_TRIGGER = "Unfill";

    private const string FILLED_ANIMATION_NAME = "Filled";
    private const string UNFILLED_ANIMATION_NAME = "Unfilled";

    public void Fill()
    {
        animator.ResetTrigger(UNFILL_TRIGGER);
        animator.SetTrigger(FILL_TRIGGER);
    }

    public void Unfill()
    {
        animator.ResetTrigger(FILL_TRIGGER);
        animator.SetTrigger(UNFILL_TRIGGER);
    }

    public void FillImmediately()
    {
        animator.ResetTrigger(FILL_TRIGGER);
        animator.ResetTrigger(UNFILL_TRIGGER);

        animator.Play(FILLED_ANIMATION_NAME);
    }

    public void UnfillImmediately()
    {
        animator.ResetTrigger(FILL_TRIGGER);
        animator.ResetTrigger(UNFILL_TRIGGER);

        animator.Play(UNFILLED_ANIMATION_NAME);
    }
}
