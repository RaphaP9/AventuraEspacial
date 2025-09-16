using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreAddFeedbackUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI feedbackText;

    [Header("Settings")]
    [SerializeField,Range(2f,10f)] private float timeShowing;

    private const string HIDE_TRIGGER = "Hide";

    public void SetFeedbackText(string text) => feedbackText.text = text;

    private void Start()
    {
        StartCoroutine(FeedbackCoroutine());
    }

    private IEnumerator FeedbackCoroutine()
    {
        yield return new WaitForSeconds(timeShowing);
        animator.SetTrigger(HIDE_TRIGGER);
    }

    public void HideUI()
    {
        StopAllCoroutines();
        animator.SetTrigger(HIDE_TRIGGER);
    }
}
