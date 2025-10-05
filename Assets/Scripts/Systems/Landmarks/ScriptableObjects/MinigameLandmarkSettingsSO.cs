using UnityEngine;

[CreateAssetMenu(fileName = "NewMinigameUnlockSettingSO", menuName = "ScriptableObjects/Unlocking/MinigameUnlockSetting")]
public class MinigameLandmarkSettingsSO : ScriptableObject
{
    [Range(0,10000)] public int landmarkScore1;
    public CinematicSO landmarkCinematic1;
    [Space]
    [Range(0, 10000)] public int landmarkScore2;
    public CinematicSO landmarkCinematic2;
    [Space]
    [Range(0, 10000)] public int landmarkScore3;
    public CinematicSO landmarkCinematic3;
}
