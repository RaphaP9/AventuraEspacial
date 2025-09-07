using System;
using UnityEngine;
using UnityEngine.UI;

public class RotationUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Button forceRotationButton;

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        forceRotationButton.onClick.AddListener(ForceRotation);
    }

    private void ForceRotation() => RotationDetectorManager.Instance.ForceRotationAndNextScene();
}
