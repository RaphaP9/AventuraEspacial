using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PasswordConfigurationConfirmButtonHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PasswordConfigurationUIHandler passwordConfigurationUIHandler;

    [Header("UI Components")]
    [SerializeField] private Button confirmButton;

    [Header("Settings")]
    [SerializeField] private string nextScene;
    [SerializeField] private TransitionType nextSceneTransitionType;

    private void OnEnable()
    {
        passwordConfigurationUIHandler.OnPasswordItemTyped += PasswordConfigurationUIHandler_OnPasswordItemTyped;
        passwordConfigurationUIHandler.OnPasswordItemDeleted += PasswordConfigurationUIHandler_OnPasswordItemDeleted;
    }

    private void OnDisable()
    {
        passwordConfigurationUIHandler.OnPasswordItemTyped -= PasswordConfigurationUIHandler_OnPasswordItemTyped;
        passwordConfigurationUIHandler.OnPasswordItemDeleted -= PasswordConfigurationUIHandler_OnPasswordItemDeleted;
    }

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void Start()
    {
        CheckButtonState();
    }

    private void InitializeButtonsListeners()
    {
        confirmButton.onClick.AddListener(SavePasswordAndGoToNextScene);
    }

    private void SavePasswordAndGoToNextScene()
    {
        DataContainer.Instance.SetPasswordItemIDs(passwordConfigurationUIHandler.TypedPassword);
        GeneralDataManager.Instance.SaveJSONData();

        ScenesManager.Instance.TransitionLoadTargetScene(nextScene, nextSceneTransitionType);
    }

    private void CheckButtonState()
    {
        if (passwordConfigurationUIHandler.CompletePasswordTyped())
        {
            EnableConfirmButton();
        }
        else
        {
            DisableConfirmButton();
        }
    }

    private void EnableConfirmButton() => confirmButton.interactable = true;
    private void DisableConfirmButton() => confirmButton.interactable = false;

    #region Subscriptions
    private void PasswordConfigurationUIHandler_OnPasswordItemDeleted(object sender, PasswordConfigurationUIHandler.OnPasswordItemEventArgs e)
    {
        CheckButtonState();
    }

    private void PasswordConfigurationUIHandler_OnPasswordItemTyped(object sender, PasswordConfigurationUIHandler.OnPasswordItemEventArgs e)
    {
        CheckButtonState();
    }
    #endregion
}
