using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using System.Collections.Generic;

public class TimerSelectorUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button confirmButton;
    [Space]
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [Header("Lists")]
    [SerializeField] private List<TimerSettingSO> timerSettingList;

    [Header("Settings")]
    [SerializeField] private string nextScene;
    [SerializeField] private TransitionType nextSceneTransitionType;

    private int currentIndex;

    private const int CONSTANT_60 = 60;

    private void OnEnable()
    {
        LocalizationSettings.SelectedLocaleChanged += LocalizationSettings_SelectedLocaleChanged;
    }
    private void OnDisable()
    {
        LocalizationSettings.SelectedLocaleChanged -= LocalizationSettings_SelectedLocaleChanged;
    }

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void Start()
    {
        InitializeVariables();
        UpdateTimeTextByCurrentIndex();
        UpdateDescriptionTextByCurrentIndex();
    }

    private void InitializeButtonsListeners()
    {
        previousButton.onClick.AddListener(SelectPreviousTime);
        nextButton.onClick.AddListener(SelectNextTime);
        confirmButton.onClick.AddListener(ConfirmTime);
    }

    private void InitializeVariables()
    {
        currentIndex = 0;   
    }

    private void SelectPreviousTime()
    {
        SelectPreviousIndex();
        UpdateTimeTextByCurrentIndex();
        UpdateDescriptionTextByCurrentIndex();
    }

    private void SelectNextTime()
    {
        SelectNextIndex();
        UpdateTimeTextByCurrentIndex();
        UpdateDescriptionTextByCurrentIndex();
    }

    private void ConfirmTime()
    {
        int selectedTimeSeconds = timerSettingList[currentIndex].time;

        TimerManager.Instance.SetTime(selectedTimeSeconds);
        ScenesManager.Instance.TransitionLoadTargetScene(nextScene, nextSceneTransitionType);
    }

    private void SelectPreviousIndex()
    {
        currentIndex--;

        if(currentIndex < 0)
        {
            currentIndex = timerSettingList.Count - 1;
        }
    }
    private void SelectNextIndex()
    {
        currentIndex++;

        if(currentIndex >= timerSettingList.Count)
        {
            currentIndex = 0;
        }
    }

    private void UpdateTimeTextByCurrentIndex()
    {
        timeText.text = FormattingUtilities.FormatTimeByTimerSettingSO(timerSettingList[currentIndex]);
    }

    private void UpdateDescriptionTextByCurrentIndex()
    {
        descriptionText.text = LocalizationSettings.StringDatabase.GetLocalizedString(timerSettingList[currentIndex].descriptionLocalizationTable, timerSettingList[currentIndex].descriptionLocalizationBinding);
    }

    #region Subscriptions
    private void LocalizationSettings_SelectedLocaleChanged(Locale obj)
    {
        UpdateTimeTextByCurrentIndex();
        UpdateDescriptionTextByCurrentIndex();
    }
    #endregion
}
