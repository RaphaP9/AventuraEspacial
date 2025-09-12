using UnityEngine;

public class ScrollSnapSingleIndicatorUI : MonoBehaviour
{
    [Header("Componets")]
    [SerializeField] private Animator animator;

    [Header("Runtime Filled")]
    [SerializeField] private int linkedIndex;

    private const string UNSNAP_TRIGGER = "Unsnap";
    private const string SNAP_TRIGGER = "Snap";

    private const string UNSNAPPED_ANIMATION_NAME = "Unsnapped";
    private const string SNAPPED_ANIMATION_NAME = "Snapped";

    public void SetLinkedIndex(int index) => linkedIndex = index;

    public void Snap()
    {
        animator.ResetTrigger(UNSNAP_TRIGGER);
        animator.SetTrigger(SNAP_TRIGGER);
    }

    public void Unsnap()
    {
        animator.ResetTrigger(SNAP_TRIGGER);
        animator.SetTrigger(UNSNAP_TRIGGER);
    }

    public void SnapImmediately()
    {
        animator.ResetTrigger(UNSNAP_TRIGGER);
        animator.ResetTrigger(SNAP_TRIGGER);

        animator.Play(SNAPPED_ANIMATION_NAME);
    }

    public void UnsnapImmediately()
    {
        animator.ResetTrigger(UNSNAP_TRIGGER);
        animator.ResetTrigger(SNAP_TRIGGER);

        animator.Play(UNSNAPPED_ANIMATION_NAME);
    }
}
