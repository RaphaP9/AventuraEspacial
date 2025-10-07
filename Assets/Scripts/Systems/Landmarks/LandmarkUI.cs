using UnityEngine;

public class LandmarkUI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Minigame minigame;
    [SerializeField] private MinigameLandmark minigameLandmark;
    [SerializeField] private MinigameLandmarkSettingsSO landmarkSettings;
    [Space]
    [SerializeField] private CutsceneSO unlockedCutscene;

    [Header("RuntimeFilled")]
    [SerializeField] private bool landmarkUnlocked;
    [SerializeField] private bool landmarkChecked;
    [Space]
    [SerializeField] private int currentMinigameScore;
    [SerializeField] private int targetScore;

    public MinigameLandmark MinigameLandmark => minigameLandmark;
    public int TargetScore => targetScore;  
    public bool LandmarkUnlocked => landmarkUnlocked;
    public bool LandmarkChecked => landmarkChecked;

    private void Start()
    {
        SetLandmark();
        CalculateMinigameScore();
        CheckLandmarkUnlocked();
    }

    private void SetLandmark()
    {
        landmarkUnlocked = DataContainer.Instance.GetLandmarkUnlockedByMinigame(minigame, minigameLandmark);
        landmarkChecked = DataContainer.Instance.GetLandmarkCheckedByMinigame(minigame, minigameLandmark);

        switch (minigameLandmark)
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
    }

    private void CalculateMinigameScore()
    {
        currentMinigameScore = DataContainer.Instance.GetMinigameTotalScoreByMinigame(minigame);
    }

    private void CheckLandmarkUnlocked()
    {
        if (landmarkUnlocked) return;
        if (currentMinigameScore < targetScore) return;
        UnlockLandmark();
    }

    private void UnlockLandmark()
    {
        landmarkUnlocked = true;
        DataContainer.Instance.UnlockLandmark(minigame, minigameLandmark);
    }

    private void CheckLandmark()
    {
        landmarkChecked = true;
        DataContainer.Instance.CheckLandmark(minigame, minigameLandmark);
    }
}
