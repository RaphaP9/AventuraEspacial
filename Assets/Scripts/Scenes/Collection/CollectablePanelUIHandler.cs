using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;
using System;

public class CollectablePanelUIHandler : MonoBehaviour
{
    public static CollectablePanelUIHandler Instance {  get; private set; }

    [Header("Components")]
    [SerializeField] private Animator animator;

    [Header("UI Components")]
    [SerializeField] private Image collectableImage;
    [SerializeField] private TextMeshProUGUI collectableNameText;
    [SerializeField] private TextMeshProUGUI collectableDescriptionText;

    [Header("Settings")]
    [SerializeField] private string stringLocalizationTable;
    [SerializeField] private string notCollectedNameLocalizationBinding;

    [Header("Runtime Filled")]
    [SerializeField] private CollectableSO currentCollectable;
    [SerializeField] private bool currentCollectableCollected;

    private const string SHOW_TRIGGER = "Show";
    private const string HIDE_TRIGGER = "Hide";

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
        SetSingleton();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //Debug.LogWarning("There is more than one CollectablePanelUI instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void ShowUI()
    {
        animator.ResetTrigger(HIDE_TRIGGER);
        animator.SetTrigger(SHOW_TRIGGER);
    }

    private void HideUI()
    {
        animator.ResetTrigger(SHOW_TRIGGER);
        animator.SetTrigger(HIDE_TRIGGER);
    }

    public void ShowCollectablePanel(CollectableSO collectableSO, bool isCollected)
    {
        currentCollectable = collectableSO;
        currentCollectableCollected = isCollected;

        SetNameTextByCurrentCollectable();
        SetDescriptionTextByCurrentCollectable();
        SetImageByCurrentCollectable();

        ShowUI();
    }

    public void CloseCollectablePanel()
    {
        currentCollectable = null;
        currentCollectableCollected = false;

        HideUI();
    }

    private void SetNameTextByCurrentCollectable()
    {
        if (currentCollectable == null) return;

        if (currentCollectableCollected) collectableNameText.text = LocalizationSettings.StringDatabase.GetLocalizedString(currentCollectable.localizationTable, currentCollectable.nameLocalizationBinding);
        else collectableNameText.text = LocalizationSettings.StringDatabase.GetLocalizedString(stringLocalizationTable, notCollectedNameLocalizationBinding);
    }

    private void SetDescriptionTextByCurrentCollectable()
    {
        if (currentCollectable == null) return;

        collectableDescriptionText.text = LocalizationSettings.StringDatabase.GetLocalizedString(currentCollectable.localizationTable, currentCollectable.descriptionLocalizationBinding);
    }

    private void SetImageByCurrentCollectable()
    {
        if(currentCollectable == null) return;

        collectableImage.sprite = currentCollectable.collectableSprite;

        if (currentCollectableCollected) collectableImage.color = currentCollectable.collectedColor;
        else collectableImage.color = currentCollectable.notCollectedColor;
    }

    #region Subscriptions
    private void LocalizationSettings_SelectedLocaleChanged(Locale locale)
    {
        SetNameTextByCurrentCollectable();
        SetDescriptionTextByCurrentCollectable();
    }
    #endregion
}
