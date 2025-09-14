using UnityEngine;
using UnityEngine.UI;

public class DisclaimerUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Button acceptButton;
    [SerializeField] private Button rejectButton;

    [Header("Components")]
    [SerializeField] private ScrollRectBottomDetector detector;

    [Header("Next Scene Settings")]
    [SerializeField] private string nextScene;
    [SerializeField] private TransitionType transitionType;

    private void OnEnable()
    {
        detector.OnBottomReached += Detector_OnBottomReached;
    }

    private void OnDisable()
    {
        detector.OnBottomReached -= Detector_OnBottomReached;
    }

    private void Awake()
    {
        DisableAcceptButton();
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        acceptButton.onClick.AddListener(AcceptDisclaimer);
        rejectButton.onClick.AddListener(RejectDisclaimer);
    }

    private void AcceptDisclaimer()
    {
        ScenesManager.Instance.TransitionLoadTargetScene(nextScene, transitionType);
        //ScenesManager.Instance.SimpleLoadTargetScene(nextScene);
    }

    private void RejectDisclaimer()
    {
        ScenesManager.Instance.QuitGame();
    }

    private void DisableAcceptButton() => acceptButton.interactable = false;
    private void EnableAcceptButton() => acceptButton.interactable = true;

    #region Subscriptions
    private void Detector_OnBottomReached(object sender, System.EventArgs e)
    {
        EnableAcceptButton();
    }
    #endregion
}
