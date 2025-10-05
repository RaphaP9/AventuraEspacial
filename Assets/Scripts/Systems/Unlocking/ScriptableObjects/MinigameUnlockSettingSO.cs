using UnityEngine;

[CreateAssetMenu(fileName = "NewMinigameUnlockSettingSO", menuName = "ScriptableObjects/Unlocking/MinigameUnlockSetting")]
public class MinigameUnlockSettingSO : ScriptableObject
{
    [Range(0,10000)] public int unlockScore1;
    public CinematicSO unlockCinematic1;
    [Space]
    [Range(0, 10000)] public float unlockScore2;
    public CinematicSO unlockCinematic2;
    [Space]
    [Range(0, 10000)] public float unlockScore3;
    public CinematicSO unlockCinematic3;
}
