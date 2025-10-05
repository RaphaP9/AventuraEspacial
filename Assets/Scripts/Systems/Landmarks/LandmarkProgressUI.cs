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
    [Space]
    [SerializeField] private List<LandmarkUI> landmarkUIList;

    [Header("RuntimeFilled")]
    [SerializeField] private int currentMinigameScore;
    [SerializeField] private int finalMinigameScoreLandmark;


    private void Start()
    {
        CalculateScores();
        SetLandmarks();

        UpdateLandmarkSlider();
    }

    private void CalculateScores()
    {
        currentMinigameScore = DataContainer.Instance.GetMinigameTotalScoreByMinigame(minigame);
        finalMinigameScoreLandmark = landmarkSettings.landmarkScore3;
    }

    private void SetLandmarks()
    {
        foreach(LandmarkUI landmarkUI in landmarkUIList)
        {
            SetLandmark(landmarkUI);
        }
    }

    private void SetLandmark(LandmarkUI landmarkUI)
    {
        int targetScore;
        bool landmarkUnlocked = DataContainer.Instance.GetLandmarkUnlockedByMinigame(minigame, landmarkUI.MinigameLandmark);
        bool landmarkChecked = DataContainer.Instance.GetLandmarkCheckedByMinigame(minigame, landmarkUI.MinigameLandmark);

        switch (landmarkUI.MinigameLandmark)
        {
            case MinigameLandmark.FirstLandmark:
            default:
                targetScore = landmarkSettings.landmarkScore1;
                break;
            case MinigameLandmark.SecondLandmark:
                targetScore = landmarkSettings.landmarkScore2;
                break;
            case MinigameLandmark.ThirdLandmark:
                targetScore = landmarkSettings.landmarkScore3;
                break;
        }

        landmarkUI.SetLandmark(minigame, targetScore, landmarkUnlocked, landmarkChecked);
    }

    private void UpdateLandmarkSlider()
    {
        float targetLandmarkFill = Mathf.Clamp01((float)currentMinigameScore / finalMinigameScoreLandmark);      
        landmarkProgressSlider.value = targetLandmarkFill;
    }
}
