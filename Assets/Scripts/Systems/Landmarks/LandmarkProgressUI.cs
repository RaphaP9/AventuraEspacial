using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LandmarkProgressUI : MonoBehaviour
{
    [Header("Main Settings")]
    [SerializeField] private Minigame minigame;
    [SerializeField] private MinigameLandmarkSettingsSO landmarkSettings;

    [Header("Components")]
    [SerializeField] private Slider landmarkProgressSlider;

    [Header("RuntimeFilled")]
    [SerializeField] private int currentMinigameScore;
    [SerializeField] private int finalMinigameScoreLandmark;

    private void Start()
    {
        CalculateScores();
        UpdateLandmarkSlider();
    }

    private void CalculateScores()
    {
        currentMinigameScore = DataContainer.Instance.GetMinigameTotalScoreByMinigame(minigame);
        finalMinigameScoreLandmark = landmarkSettings.landmarkScore3;
    }

    private void UpdateLandmarkSlider()
    {
        float targetLandmarkFill = Mathf.Clamp01((float)currentMinigameScore / finalMinigameScoreLandmark);      
        landmarkProgressSlider.value = targetLandmarkFill;
    }
}
