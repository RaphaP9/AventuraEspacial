using UnityEngine;
using TMPro;

public class TimeOptionsUIHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI remainingTimeText;

    private void OnEnable()
    {
        TimerManager.OnTimeSet += TimerManager_OnTimeSet;
        TimerManager.OnTimeChanged += TimerManager_OnTimeChanged;
        TimerManager.OnTimeEnded += TimerManager_OnTimeEnded;
    }

    private void OnDisable()
    {
        TimerManager.OnTimeSet -= TimerManager_OnTimeSet;
        TimerManager.OnTimeChanged -= TimerManager_OnTimeChanged;
        TimerManager.OnTimeEnded -= TimerManager_OnTimeEnded;
    }

    private void Start()
    {
        UpdateRemainingTimeText(TimerManager.Instance.RemainingTimeInt);
    }

    private void UpdateRemainingTimeText(int remainingTime)
    {
        remainingTimeText.text = FormattingUtilities.FormatTime(remainingTime);
    }

    #region Subsciptions
    private void TimerManager_OnTimeChanged(object sender, TimerManager.OnTimeChangedEventArgs e)
    {
        UpdateRemainingTimeText(e.timeInt);
    }

    private void TimerManager_OnTimeSet(object sender, TimerManager.OnTimeEventArgs e)
    {
        UpdateRemainingTimeText(e.timeInt);
    }

    private void TimerManager_OnTimeEnded(object sender, TimerManager.OnTimeEventArgs e)
    {
        UpdateRemainingTimeText(0);
    }
    #endregion
}
