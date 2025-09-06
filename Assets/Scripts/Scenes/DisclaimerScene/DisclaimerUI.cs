using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DisclaimerUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Button acceptButton;
    [SerializeField] private Button rejectButton;

    [Header("PlayerPrefs Settings")]
    [SerializeField] private string disclaimerAuthorizationKey;

    [Header("Next Scene Settings")]
    [SerializeField] private string nextScene;
    [SerializeField] private TransitionType transitionType;

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        acceptButton.onClick.AddListener(AcceptDisclaimer);
        rejectButton.onClick.AddListener(RejectDisclaimer);
    }

    private void AcceptDisclaimer()
    {
        PlayerPrefs.SetInt(disclaimerAuthorizationKey, 1);
        PlayerPrefs.Save();
    
        ScenesManager.Instance.TransitionLoadTargetScene(nextScene,transitionType);
        //ScenesManager.Instance.SimpleLoadTargetScene(nextScene);
    }

    private void RejectDisclaimer()
    {
        ScenesManager.Instance.QuitGame();
    }
}
