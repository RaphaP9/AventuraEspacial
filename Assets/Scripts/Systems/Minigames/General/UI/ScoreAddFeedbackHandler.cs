using UnityEngine;

public class ScoreAddFeedbackHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform feedbackPrefab;
    [SerializeField] private Transform feedbackHolder;

    private const string PLUS_CHARACTER = "+";

    private void OnEnable()
    {
        MinigameScoreManager.OnScoreIncreased += MinigameScoreManager_OnScoreIncreased;
    }

    private void OnDisable()
    {
        MinigameScoreManager.OnScoreIncreased -= MinigameScoreManager_OnScoreIncreased;
    }

    private void HideEachOtherFeedback()
    {
        foreach(Transform child in feedbackHolder)
        {
            ScoreAddFeedbackUI scoreAddFeedbackUI = child.GetComponentInChildren<ScoreAddFeedbackUI>();

            if (scoreAddFeedbackUI == null) return;

            scoreAddFeedbackUI.HideUI();
        }
    }

    private void CreateFeedback(int scoreAdded)
    {
        Transform feedbackTransform = Instantiate(feedbackPrefab, feedbackHolder);

        ScoreAddFeedbackUI scoreAddFeedbackUI = feedbackTransform.GetComponentInChildren<ScoreAddFeedbackUI>();

        if (scoreAddFeedbackUI == null) return;

        string feedbackText = $"{PLUS_CHARACTER}{scoreAdded}";

        scoreAddFeedbackUI.SetFeedbackText(feedbackText);
    }

    private void MinigameScoreManager_OnScoreIncreased(object sender, MemoryMinigameScoreManager.OnScoreIncreasedEventArgs e)
    {
        HideEachOtherFeedback();
        CreateFeedback(e.increaseQuantity);
    }
}
