using System;
using UnityEngine;
using UnityEngine.UI;

public class MinigameSelectionSingleUI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Minigame minigame;

    [Header("UI Components")]
    [SerializeField] private Button minigameButton;

    [Header("Start Cinematic Scene")]
    [SerializeField] private string startCinematicScene;
    [SerializeField] private TransitionType startCinematicTransitionType;
    [SerializeField] private bool ignoreStartCinematic;

    [Header("Minigame Scene")]
    [SerializeField] private string minigameScene;
    [SerializeField] private TransitionType minigameTransitionType;

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        minigameButton.onClick.AddListener(LoadNextScene);
    }

    public void LoadNextScene()
    {
        bool isFirstTimeEntering = DataContainer.Instance.IsFirstTimeEnteringMinigame(minigame);

        if (isFirstTimeEntering && !ignoreStartCinematic) LoadStartCinematicScene();
        else LoadMinigameScene();
    }

    private void LoadStartCinematicScene() => ScenesManager.Instance.TransitionLoadTargetScene(startCinematicScene, startCinematicTransitionType);
    private void LoadMinigameScene() => ScenesManager.Instance.TransitionLoadTargetScene(minigameScene, minigameTransitionType);
}
