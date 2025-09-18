using UnityEngine;
using TMPro;

public class MinigameScoreUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI scoreText;

    protected void SetScoreText(int score) => scoreText.text = score.ToString();
}
