using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseUIContentsHandler : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Button homeButton;

    [Header("Settings")]
    [SerializeField] private string homeScene;
    [SerializeField] private TransitionType homeTransitionType;

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        homeButton.onClick.AddListener(LoadHomeScene);
    }

    private void LoadHomeScene() => ScenesManager.Instance.TransitionLoadTargetScene(homeScene, homeTransitionType);
}
