using System;
using UnityEngine;
using UnityEngine.UI;

public class FinalScoreUIButtonsHandler : MonoBehaviour
{
    [Header("Retry Button")]
    [SerializeField] private Button retryButton;
    [SerializeField] private TransitionType retryTransitionType;

    [Header("Home Button")]
    [SerializeField] private Button homeButton;
    [SerializeField] private string homeScene;
    [SerializeField] private TransitionType homeTransitionType;

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        retryButton.onClick.AddListener(ReloadScene);
        homeButton.onClick.AddListener(LoadHomeScene);
    }

    private void ReloadScene() => ScenesManager.Instance.TransitionReloadCurrentScene(retryTransitionType);
    private void LoadHomeScene() => ScenesManager.Instance.TransitionLoadTargetScene(homeScene, homeTransitionType);
}
