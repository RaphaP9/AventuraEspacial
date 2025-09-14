using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimplifiedDisclaimerUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Button acceptButton;
    [SerializeField] private Button rejectButton;

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
        ScenesManager.Instance.TransitionLoadTargetScene(nextScene,transitionType);
        //ScenesManager.Instance.SimpleLoadTargetScene(nextScene);
    }

    private void RejectDisclaimer()
    {
        ScenesManager.Instance.QuitGame();
    }
}
