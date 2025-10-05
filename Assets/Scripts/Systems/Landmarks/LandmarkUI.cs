using UnityEngine;

public class LandmarkUI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private MinigameLandmark minigameLandmark;

    [Header("RuntimeFilled")]
    [SerializeField] private Minigame minigame;
    [Space]
    [SerializeField] private int targetScore;
    [Space]
    [SerializeField] private bool landmarkUnlocked;
    [SerializeField] private bool landmarkChecked;

    public MinigameLandmark MinigameLandmark => minigameLandmark;

    public void SetLandmark(Minigame minigame, int targetScore, bool landmarkUnlocked, bool landmarkChecked)
    {
        this.minigame = minigame;
        this.targetScore = targetScore;
        this.landmarkUnlocked = landmarkUnlocked;
        this.landmarkChecked = landmarkChecked;
    }
}
