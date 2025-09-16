using System;
using UnityEngine;
using UnityEngine.UI;

public class MinigameSelectionSingleUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Button minigameButton;

    [Header("Settings")]
    [SerializeField] private string minigameScene;
    [SerializeField] private TransitionType minigameTransitionType;

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        minigameButton.onClick.AddListener(LoadMinigameScene);
    }

    private void LoadMinigameScene() => ScenesManager.Instance.TransitionLoadTargetScene(minigameScene, minigameTransitionType);
}
