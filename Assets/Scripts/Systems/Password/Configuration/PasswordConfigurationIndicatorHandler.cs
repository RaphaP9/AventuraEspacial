using UnityEngine;
using UnityEngine.UI;

public class PasswordConfigurationIndicatorHandler : MonoBehaviour
{
    [Header("Componets")]
    [SerializeField] private Animator animator;

    [Header("UI Components")]
    [SerializeField] private Image itemImage;

    [Header("Runtime Filled")]
    [SerializeField] private PasswordConfigurationUIHandler passwordConfigurationUIHandler;
    [SerializeField] private int assignedIndex;
    [SerializeField] private PasswordItemSO currentPasswordItem;

    private const string FILL_TRIGGER = "Fill";
    private const string UNFILL_TRIGGER = "Unfill";

    private const string FILLED_ANIMATION_NAME = "Filled";
    private const string UNFILLED_ANIMATION_NAME = "Unfilled";

    public void SetIndicator(int assignedIndex, PasswordConfigurationUIHandler passwordConfigurationUIHandler)
    {
        this.assignedIndex = assignedIndex;
        this.passwordConfigurationUIHandler = passwordConfigurationUIHandler;

        passwordConfigurationUIHandler.OnPasswordItemTyped += PasswordConfigurationUIHandler_OnPasswordItemTyped;
        passwordConfigurationUIHandler.OnPasswordItemDeleted += PasswordConfigurationUIHandler_OnPasswordItemDeleted;

        UnfillImmediately();
    }

    private void OnDisable()
    {
        if (passwordConfigurationUIHandler == null) return;

        passwordConfigurationUIHandler.OnPasswordItemTyped -= PasswordConfigurationUIHandler_OnPasswordItemTyped;
        passwordConfigurationUIHandler.OnPasswordItemDeleted -= PasswordConfigurationUIHandler_OnPasswordItemDeleted;
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
    private void PasswordConfigurationUIHandler_OnPasswordItemTyped(object sender, PasswordConfigurationUIHandler.OnPasswordItemEventArgs e)
    {
        if (e.index != assignedIndex) return;

        SetItemImage(e.passwordItemSO);

        if (e.immediately) FillImmediately(e.passwordItemSO);
        else Fill(e.passwordItemSO);
    }

    private void PasswordConfigurationUIHandler_OnPasswordItemDeleted(object sender, PasswordConfigurationUIHandler.OnPasswordItemEventArgs e)
    {
        if(e.index != assignedIndex) return;

        if (e.immediately) UnfillImmediately();
        else Unfill();
    }
    #endregion
}
