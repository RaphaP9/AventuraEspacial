using UnityEngine;
using UnityEngine.UI;

public class PasswordEntryIndicatorHandler : MonoBehaviour
{
    [Header("Componets")]
    [SerializeField] private Animator animator;

    [Header("UI Components")]
    [SerializeField] private Image itemImage;

    [Header("Runtime Filled")]
    [SerializeField] private PasswordEntryUIHandler passwordEntryUIHandler;
    [SerializeField] private int assignedIndex;
    [SerializeField] private PasswordItemSO currentPasswordItem;

    private const string FILL_TRIGGER = "Fill";
    private const string UNFILL_TRIGGER = "Unfill";

    private const string FILLED_ANIMATION_NAME = "Filled";
    private const string UNFILLED_ANIMATION_NAME = "Unfilled";

    public void SetIndicator(int assignedIndex, PasswordEntryUIHandler passwordEntryUIHandler)
    {
        this.assignedIndex = assignedIndex;
        this.passwordEntryUIHandler = passwordEntryUIHandler;

        passwordEntryUIHandler.OnPasswordItemTyped += PasswordEntryUIHandler_OnPasswordItemTyped;
        passwordEntryUIHandler.OnPasswordItemDeleted += PasswordEntryUIHandler_OnPasswordItemDeleted;

        UnfillImmediately();
    }

    private void OnDisable()
    {
        if (passwordEntryUIHandler == null) return;

        passwordEntryUIHandler.OnPasswordItemTyped -= PasswordEntryUIHandler_OnPasswordItemTyped;
        passwordEntryUIHandler.OnPasswordItemDeleted -= PasswordEntryUIHandler_OnPasswordItemDeleted;
    }

    private void SetItemImage(PasswordItemSO passwordItemSO) => itemImage.sprite = passwordItemSO.sprite;

    public void Fill(PasswordItemSO passwordItemSO)
    {
        currentPasswordItem = passwordItemSO;

        animator.ResetTrigger(UNFILL_TRIGGER);
        animator.SetTrigger(FILL_TRIGGER);
    }

    public void Unfill()
    {
        currentPasswordItem = null;

        animator.ResetTrigger(FILL_TRIGGER);
        animator.SetTrigger(UNFILL_TRIGGER);
    }

    public void FillImmediately(PasswordItemSO passwordItemSO)
    {
        currentPasswordItem = passwordItemSO;

        animator.ResetTrigger(FILL_TRIGGER);
        animator.ResetTrigger(UNFILL_TRIGGER);

        animator.Play(FILLED_ANIMATION_NAME);
    }

    public void UnfillImmediately()
    {
        currentPasswordItem = null;

        animator.ResetTrigger(FILL_TRIGGER);
        animator.ResetTrigger(UNFILL_TRIGGER);

        animator.Play(UNFILLED_ANIMATION_NAME);
    }

    #region Subscriptions
    private void PasswordEntryUIHandler_OnPasswordItemTyped(object sender, PasswordEntryUIHandler.OnPasswordItemEventArgs e)
    {
        if (e.index != assignedIndex) return;

        SetItemImage(e.passwordItemSO);

        if (e.immediately) FillImmediately(e.passwordItemSO);
        else Fill(e.passwordItemSO);
    }

    private void PasswordEntryUIHandler_OnPasswordItemDeleted(object sender, PasswordEntryUIHandler.OnPasswordItemEventArgs e)
    {
        if (e.index != assignedIndex) return;

        if (e.immediately) UnfillImmediately();
        else Unfill();
    }
    #endregion
}
