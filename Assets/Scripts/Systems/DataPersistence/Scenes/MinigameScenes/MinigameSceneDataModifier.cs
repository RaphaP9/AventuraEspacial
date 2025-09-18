using UnityEngine;

public class MinigameSceneDataModifier : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Minigame minigame;

    [Header("Components")]
    [SerializeField] private MinigameScoreManager minigameScoreManager;

    private void OnEnable()
    {
        MinigameManager.OnGameWon += MinigameManager_OnGameWon;
        MinigameManager.OnGameLost += MinigameManager_OnGameLost;
    }
    private void OnDisable()
    {
        MinigameManager.OnGameWon -= MinigameManager_OnGameWon;
        MinigameManager.OnGameLost -= MinigameManager_OnGameLost;
    }

    private void Start()
    {
        HandleEnteringMinigameDataModification();
    }

    private void HandleEnteringMinigameDataModification()
    {
        DataContainer.Instance.IncreaseTimesEnteredMinigame(minigame);
        GeneralDataManager.Instance.SaveJSONDataAsyncWrapper();
    }

    #region Subscriptions
    private void MinigameManager_OnGameWon(object sender, System.EventArgs e)
    {
        DataContainer.Instance.IncreaseTimesWonMinigame(minigame);
        DataContainer.Instance.IncreaseTotalScoreMinigame(minigame, minigameScoreManager.CurrentScore);

        GeneralDataManager.Instance.SaveJSONDataAsyncWrapper();
    }

    private void MinigameManager_OnGameLost(object sender, System.EventArgs e)
    {
        DataContainer.Instance.IncreaseTimesLostMinigame(minigame);
        DataContainer.Instance.IncreaseTotalScoreMinigame(minigame, minigameScoreManager.CurrentScore);

        GeneralDataManager.Instance.SaveJSONDataAsyncWrapper();
    }
    #endregion
}