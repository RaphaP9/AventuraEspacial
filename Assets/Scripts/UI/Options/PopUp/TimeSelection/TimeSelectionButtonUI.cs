using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class TimeSelectionButtonUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TimerSettingSO timerSettingSO;
    [SerializeField] private Button button;

    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI timeText;

    private void OnEnable()
    {
        LocalizationSettings.SelectedLocaleChanged += LocalizationSettings_SelectedLocaleChanged;
    }
    private void OnDisable()
    {
        LocalizationSettings.SelectedLocaleChanged -= LocalizationSettings_SelectedLocaleChanged;
    }

    public class OnClickedEventArgs : EventArgs
    {
        public TimerSettingSO timerSettingSO;
    }

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void Start()
    {
        UpdateTimeText();
    }

    private void InitializeButtonsListeners()
    {
        button.onClick.AddListener(SetTime);
    }

    private void UpdateTimeText()
    {
        timeText.text = FormattingUtilities.FormatTimeByTimerSettingSO(timerSettingSO);
    }

    private void SetTime()
    {
        TimerManager.Instance.SetTime(timerSettingSO.time);
    }

    #region Subscriptions
    private void LocalizationSettings_SelectedLocaleChanged(Locale obj)
    {
        UpdateTimeText();
    }
    #endregion
}
