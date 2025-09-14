using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class TimerSelectorUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TimerSelectorSettingsSO timerSelectorSettingsSO;

    [Header("UI Components")]
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button confirmButton;
    [Space]
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [Header("Settings")]
    [SerializeField] private string nextScene;
    [SerializeField] private TransitionType nextSceneTransitionType;

    private int currentIndex;

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
        UpdateUI();
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
        UpdateUI();
    }

    private void SelectNextTime()
    {
        SelectNextIndex();
        UpdateUI();
    }

    private void ConfirmTime()
    {
        int selectedTimeSeconds = timerSelectorSettingsSO.timerSettings[currentIndex].time;

        TimerManager.Instance.SetTime(selectedTimeSeconds);

        ScenesManager.Instance.TransitionLoadTargetScene(nextScene, nextSceneTransitionType);
    }

    private void SelectPreviousIndex()
    {
        currentIndex--;

        if(currentIndex < 0)
        {
            currentIndex = timerSelectorSettingsSO.timerSettings.Count - 1;
        }
    }
    private void SelectNextIndex()
    {
        currentIndex++;

        if(currentIndex >= timerSelectorSettingsSO.timerSettings.Count)
        {
            currentIndex = 0;
        }
    }

    private void UpdateUI()
    {
        timeText.text = FormattingUtilities.FormatTime(timerSelectorSettingsSO.timerSettings[currentIndex].time);
        descriptionText.text = LocalizationSettings.StringDatabase.GetLocalizedString(timerSelectorSettingsSO.timeSelectorLocalizationTable, timerSelectorSettingsSO.timerSettings[currentIndex].descriptionLocalizationBinding);
    }

    #region Subscriptions
    private void LocalizationSettings_SelectedLocaleChanged(Locale obj)
    {
        UpdateUI();
    }
    #endregion
}
