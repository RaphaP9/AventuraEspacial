using UnityEngine;

public class PasswordIndicatorHandler : MonoBehaviour
{
    [Header("Componets")]
    [SerializeField] private Animator animator;

    [Header("Runtime Filled")]
    [SerializeField] private bool isFilled;

    public bool IsFilled => isFilled;

    private const string FILL_TRIGGER = "Fill";
    private const string UNFILL_TRIGGER = "Unfill";

    private const string FILLED_ANIMATION_NAME = "Filled";
    private const string UNFILLED_ANIMATION_NAME = "Unfilled";

    public void Fill()
    {
        isFilled = true;

        animator.ResetTrigger(UNFILL_TRIGGER);
        animator.SetTrigger(FILL_TRIGGER);
    }

    public void Unfill()
    {
        isFilled = false;

        animator.ResetTrigger(FILL_TRIGGER);
        animator.SetTrigger(UNFILL_TRIGGER);
    }

    public void FillImmediately()
    {
        isFilled = true;

        animator.ResetTrigger(FILL_TRIGGER);
        animator.ResetTrigger(UNFILL_TRIGGER);

        animator.Play(FILLED_ANIMATION_NAME);
    }

    public void UnfillImmediately()
    {
        isFilled = false;

        animator.ResetTrigger(FILL_TRIGGER);
        animator.ResetTrigger(UNFILL_TRIGGER);

        animator.Play(UNFILLED_ANIMATION_NAME);
    }
}
