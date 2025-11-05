using UnityEngine;

[CreateAssetMenu(fileName = "NewMinigameUnlockSettingSO", menuName = "ScriptableObjects/Unlocking/MinigameUnlockSetting")]
public class MinigameLandmarkSettingsSO : ScriptableObject
{
    [Range(0,10000)] public int landmarkScore1;
    [Range(0, 10000)] public int landmarkScore2;
    [Range(0, 10000)] public int landmarkScore3;
}
