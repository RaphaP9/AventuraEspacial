using UnityEngine;

public class CollectableUIAnimationController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CollectableUI collectableUI;
    [SerializeField] private Animator animator;

    private const string SELECT_TRIGGER = "Select";
    private const string DESELECT_TRIGGER = "Deselect";

    private const string SELECTED_ANIMATION_NAME = "Selected";
    private const string DESELECTED_ANIMATION_NAME = "Deselected";

    private void OnEnable()
    {
        collectableUI.OnCollectableUISelected += CollectableUI_OnCollectableUISelected;
        collectableUI.OnCollectableUIDeselected += CollectableUI_OnCollectableUIDeselected;
    }

    private void OnDisable()
    {
        collectableUI.OnCollectableUISelected -= CollectableUI_OnCollectableUISelected;
        collectableUI.OnCollectableUIDeselected -= CollectableUI_OnCollectableUIDeselected;
    }

    public void Select()
    {
        animator.ResetTrigger(DESELECT_TRIGGER);
        animator.SetTrigger(SELECT_TRIGGER);
    }

    public void Deselect()
    {
        animator.ResetTrigger(SELECT_TRIGGER);
        animator.SetTrigger(DESELECT_TRIGGER);
    }

    public void SelectInstant()
    {
        animator.ResetTrigger(SELECT_TRIGGER);
        animator.ResetTrigger(DESELECT_TRIGGER);

        animator.Play(SELECTED_ANIMATION_NAME);
    }

    public void DeselectInstant()
    {
        animator.ResetTrigger(SELECT_TRIGGER);
        animator.ResetTrigger(DESELECT_TRIGGER);

        animator.Play(DESELECTED_ANIMATION_NAME);
    }

    #region Subscriptions
    private void CollectableUI_OnCollectableUISelected(object sender, CollectableUI.OnCollectableSelectEventArgs e)
    {
        if (e.instant) SelectInstant();
        else Select();
    }

    private void CollectableUI_OnCollectableUIDeselected(object sender, CollectableUI.OnCollectableSelectEventArgs e)
    {
        if (e.instant) DeselectInstant();
        else Deselect();
    }

    #endregion
}
