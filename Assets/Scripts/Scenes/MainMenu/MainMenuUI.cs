using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Button quitButton;

    [Header("Play Settings")]
    [SerializeField] private Button playButton;
    [SerializeField] private string playScene;
    [SerializeField] private TransitionType playSceneTransitionType;

    [Header("Album Settings")]
    [SerializeField] private Button albumButton;
    [SerializeField] private string albumScene;
    [SerializeField] private TransitionType albumSceneTransitionType;

    [Header("Collection Settings")]
    [SerializeField] private Button collectionButton;
    [SerializeField] private string collectionScene;
    [SerializeField] private TransitionType collectionSceneTransitionType;

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        quitButton.onClick.AddListener(QuitGame);

        playButton.onClick.AddListener(LoadPlayScene);
        albumButton.onClick.AddListener(LoadAlbumScene);
        collectionButton.onClick.AddListener(LoadCollectionScene);
    }

    private void LoadPlayScene() => ScenesManager.Instance.TransitionLoadTargetScene(playScene, playSceneTransitionType);
    private void LoadAlbumScene() => ScenesManager.Instance.TransitionLoadTargetScene(albumScene, albumSceneTransitionType);
    private void LoadCollectionScene() => ScenesManager.Instance.TransitionLoadTargetScene(collectionScene, collectionSceneTransitionType);
    private void QuitGame() => ScenesManager.Instance.QuitGame();
}
