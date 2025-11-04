using UnityEngine;
using UnityEngine.UI;

public class TestButtonUI : MonoBehaviour
{
    [Header("Test Settings")]
    [SerializeField] private Button testSceneButton;
    [SerializeField] private string testScene;
    [SerializeField] private TransitionType testSceneTransitionType;

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        testSceneButton.onClick.AddListener(LoadTestScene);
    }

    private void LoadTestScene() => ScenesManager.Instance.TransitionLoadTargetScene(testScene, testSceneTransitionType);
}
