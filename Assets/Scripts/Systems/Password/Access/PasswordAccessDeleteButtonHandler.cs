using UnityEngine;
using UnityEngine.UI;

public class PasswordAccessDeleteButtonHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private ItemPasswordAccessUIHandler itemPasswordAccessUIHandler;
    [SerializeField] private Button deleteButton;

    private void OnEnable()
    {
        itemPasswordAccessUIHandler.OnCompletePasswordTyped += ItemPasswordAccessUIHandler_OnCompletePasswordTyped;
        itemPasswordAccessUIHandler.OnPasswordCleared += ItemPasswordAccessUIHandler_OnPasswordCleared;
        itemPasswordAccessUIHandler.OnPasswordItemDeleted += ItemPasswordAccessUIHandler_OnPasswordItemDeleted;
    }

    private void OnDisable()
    {
        itemPasswordAccessUIHandler.OnCompletePasswordTyped -= ItemPasswordAccessUIHandler_OnCompletePasswordTyped;
        itemPasswordAccessUIHandler.OnPasswordCleared -= ItemPasswordAccessUIHandler_OnPasswordCleared;
        itemPasswordAccessUIHandler.OnPasswordItemDeleted -= ItemPasswordAccessUIHandler_OnPasswordItemDeleted;
    }

    private void EnableButton() => deleteButton.interactable = true;
    private void DisableButton() => deleteButton.interactable = false;

    #region Subscriptions
    private void ItemPasswordAccessUIHandler_OnCompletePasswordTyped(object sender, System.EventArgs e)
    {
        DisableButton();
    }

    private void ItemPasswordAccessUIHandler_OnPasswordCleared(object sender, System.EventArgs e)
    {
        EnableButton();
    }

    private void ItemPasswordAccessUIHandler_OnPasswordItemDeleted(object sender, ItemPasswordAccessUIHandler.OnPasswordItemEventArgs e)
    {
        EnableButton();
    }
    #endregion
}
