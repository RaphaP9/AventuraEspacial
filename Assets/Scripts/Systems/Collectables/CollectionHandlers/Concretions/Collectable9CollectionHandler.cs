using UnityEngine;

public class Collectable9CollectionHandler : CollectableCollectionHandler
{
    //Collectable that checks if your score is higher or equal to X in the Memory Minigame
    [Header("Settings")]
    [SerializeField, Range (100, 200)] private int targetScore;

    private void OnEnable()
    {
        MinigameScoreManager.OnScoreIncreased += MinigameScoreManager_OnScoreIncreased;
    }

    private void OnDisable()
    {
        MinigameScoreManager.OnScoreIncreased -= MinigameScoreManager_OnScoreIncreased;
    }

    private void CheckCollectableCollected(int score)
    {
        if (score < targetScore) return;
        CollectCollectable(false);
    }

    private void MinigameScoreManager_OnScoreIncreased(object sender, MinigameScoreManager.OnScoreIncreasedEventArgs e)
    {
        CheckCollectableCollected(e.currentScore);
    }
}
