using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MinigameSceneDataModifier : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Minigame minigame;

    [Header("Components")]
    [SerializeField] private MinigameScoreManager minigameScoreManager;

    public static event EventHandler<OnTotalScoreAddedEventArgs> OnTotalScoreAdded;

    public class OnTotalScoreAddedEventArgs : EventArgs
    {
        public int newTotalScore;
    }

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

    private void HandleTotalScoreAdd()
    {
        DataContainer.Instance.IncreaseTimesWonMinigame(minigame);
        DataContainer.Instance.IncreaseTotalScoreMinigame(minigame, minigameScoreManager.CurrentScore);

        GeneralDataManager.Instance.SaveJSONDataAsyncWrapper();

        OnTotalScoreAdded?.Invoke(this, new OnTotalScoreAddedEventArgs { newTotalScore = DataContainer.Instance.GetMinigameTotalScoreByMinigame(minigame) });
    }

    #region Subscriptions
    private void MinigameManager_OnGameWon(object sender, System.EventArgs e)
    {
        HandleTotalScoreAdd();
    }

    private void MinigameManager_OnGameLost(object sender, System.EventArgs e)
    {
        HandleTotalScoreAdd();
    }
    #endregion
}