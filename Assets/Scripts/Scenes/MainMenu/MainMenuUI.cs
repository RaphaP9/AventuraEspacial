using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button settingsButton;

    [Header("Settings")]
    [SerializeField] private string playScene;
    [SerializeField] private TransitionType playSceneTransitionType;

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        playButton.onClick.AddListener(LoadPlayScene);
        quitButton.onClick.AddListener(QuitGame);
        settingsButton.onClick.AddListener(OpenSettings);
    }

    private void LoadPlayScene() => ScenesManager.Instance.TransitionLoadTargetScene(playScene, playSceneTransitionType);
    private void QuitGame() => ScenesManager.Instance.QuitGame();

    private void OpenSettings()
    {

    }
}
