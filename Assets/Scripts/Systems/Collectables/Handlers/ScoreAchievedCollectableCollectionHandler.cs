using UnityEngine;

public class ScoreAchievedCollectableCollectionHandler : CollectableCollectionHandler
{
    [Header("Settings")]
    [SerializeField] private int scoreToAchieve;

    private void OnEnable()
    {
        MinigameScoreManager.OnScoreIncreased += MinigameScoreManager_OnScoreIncreased;
    }

    private void OnDisable()
    {
        MinigameScoreManager.OnScoreIncreased -= MinigameScoreManager_OnScoreIncreased;
    }

    private void CheckCollectableCollection(int score)
    {
        if (score < scoreToAchieve) return;

        CollectCollectable(false);
    }

    private void MinigameScoreManager_OnScoreIncreased(object sender, MinigameScoreManager.OnScoreIncreasedEventArgs e)
    {
        CheckCollectableCollection(e.currentScore);
    }
}
