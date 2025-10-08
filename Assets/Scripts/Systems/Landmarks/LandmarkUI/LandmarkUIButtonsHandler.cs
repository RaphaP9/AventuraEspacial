using System;
using UnityEngine;
using UnityEngine.UI;

public class LandmarkUIButtonsHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private LandmarkUI landmarkUI;

    [Header("UI Components")]
    [SerializeField] private Button landmarkButton;

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        landmarkButton.onClick.AddListener(ClaimLandmark);
    }

    private void ClaimLandmark()
    {
        landmarkUI.ClaimLandmark();
    }
}
