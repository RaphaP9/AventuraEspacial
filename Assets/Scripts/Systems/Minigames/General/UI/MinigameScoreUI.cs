using UnityEngine;
using TMPro;

public class MinigameScoreUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        MinigameScoreManager.OnScoreInitialized += MinigameScoreManager_OnScoreInitialized;
        MinigameScoreManager.OnScoreIncreased += MinigameScoreManager_OnScoreIncreased;
    }

    private void OnDisable()
    {
        MinigameScoreManager.OnScoreInitialized -= MinigameScoreManager_OnScoreInitialized;
        MinigameScoreManager.OnScoreIncreased -= MinigameScoreManager_OnScoreIncreased;
    }
    protected void SetScoreText(int score) => scoreText.text = score.ToString();

    #region Subcriptions
    private void MinigameScoreManager_OnScoreInitialized(object sender, MinigameScoreManager.OnScoreInitializedEventArgs e)
    {
        SetScoreText(e.currentScore);
    }

    private void MinigameScoreManager_OnScoreIncreased(object sender, MinigameScoreManager.OnScoreIncreasedEventArgs e)
    {
        SetScoreText(e.currentScore);
    }
    #endregion

}
