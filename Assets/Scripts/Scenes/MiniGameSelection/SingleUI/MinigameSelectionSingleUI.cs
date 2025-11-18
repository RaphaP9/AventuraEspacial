using System;
using UnityEngine;
using UnityEngine.UI;

public class MinigameSelectionSingleUI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Minigame minigame;

    [Header("UI Components")]
    [SerializeField] private Button minigameButton;

    [Header("First Enter Scene")]
    [SerializeField] private string firstEnterScene;
    [SerializeField] private TransitionType firstEnterSceneTransitionType;
    [SerializeField] private bool ignoreFirstEnterScene;

    [Header("Regular Enter Scene")]
    [SerializeField] private string regularEnterScene;
    [SerializeField] private TransitionType regularEnterSceneTransitionType;

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

        if (isFirstTimeEntering && !ignoreFirstEnterScene) LoadFirstEnterScene();
        else LoadRegularEnterScene();
    }

    private void LoadFirstEnterScene() => ScenesManager.Instance.TransitionLoadTargetScene(firstEnterScene, firstEnterSceneTransitionType);
    private void LoadRegularEnterScene() => ScenesManager.Instance.TransitionLoadTargetScene(regularEnterScene, regularEnterSceneTransitionType);
}
