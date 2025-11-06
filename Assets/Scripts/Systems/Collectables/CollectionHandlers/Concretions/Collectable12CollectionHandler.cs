using UnityEngine;

public class Collectable12CollectionHandler : CollectableCollectionHandler
{
    //This collectable checks a combo on Silhouettes Minigame (make sure to put this script on SilhouettesMinigame scene)

    [Header("Settings")]
    [SerializeField, Range(2, 10)] private int targetCombo;

    private void OnEnable()
    {
        MinigameScoreManager.OnComboGained += MinigameScoreManager_OnComboGained;
        MinigameScoreManager.OnComboUpdated += MinigameScoreManager_OnComboUpdated;
    }

    private void OnDisable()
    {
        MinigameScoreManager.OnComboGained -= MinigameScoreManager_OnComboGained;
        MinigameScoreManager.OnComboUpdated -= MinigameScoreManager_OnComboUpdated;
    }

    private void CheckCollectableCollected(int combo)
    {
        if (combo != targetCombo) return;
        CollectCollectable(false);
    }

    private void MinigameScoreManager_OnComboUpdated(object sender, MinigameScoreManager.OnComboGainedEventArgs e)
    {
        CheckCollectableCollected(e.comboGained);
    }

    private void MinigameScoreManager_OnComboGained(object sender, MinigameScoreManager.OnComboGainedEventArgs e)
    {
        CheckCollectableCollected(e.comboGained);
    }
}
