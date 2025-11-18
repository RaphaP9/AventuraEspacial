using UnityEngine;
using UnityEngine.UI;

public class PasswordEntryDeleteButtonHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PasswordEntryUIHandler passwordEntryUIHandler;
    [SerializeField] private Button deleteButton;

    private void OnEnable()
    {
        passwordEntryUIHandler.OnCompletePasswordTyped += ItemPasswordAccessUIHandler_OnCompletePasswordTyped;
        passwordEntryUIHandler.OnPasswordCleared += ItemPasswordAccessUIHandler_OnPasswordCleared;
        passwordEntryUIHandler.OnPasswordItemDeleted += ItemPasswordAccessUIHandler_OnPasswordItemDeleted;
    }

    private void OnDisable()
    {
        passwordEntryUIHandler.OnCompletePasswordTyped -= ItemPasswordAccessUIHandler_OnCompletePasswordTyped;
        passwordEntryUIHandler.OnPasswordCleared -= ItemPasswordAccessUIHandler_OnPasswordCleared;
        passwordEntryUIHandler.OnPasswordItemDeleted -= ItemPasswordAccessUIHandler_OnPasswordItemDeleted;
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

    private void ItemPasswordAccessUIHandler_OnPasswordItemDeleted(object sender, PasswordEntryUIHandler.OnPasswordItemEventArgs e)
    {
        EnableButton();
    }
    #endregion
}
