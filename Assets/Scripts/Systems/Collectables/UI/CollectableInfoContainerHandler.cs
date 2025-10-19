using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;

public class CollectableInfoContainerHandler : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Image collectableImage;
    [SerializeField] private TextMeshProUGUI collectableNameText;
    [SerializeField] private TextMeshProUGUI collectableDescriptionText;

    [Header("Settings")]
    [SerializeField] private string stringLocalizationTable;
    [SerializeField] private string notCollectedNameLocalizationBinding;

    [Header("Runtime Filled")]
    [SerializeField] private CollectableUI currentCollectableUI;

    private void OnEnable()
    {
        CollectableUI.OnCollectableSelected += CollectableUI_OnCollectableSelected;
        LocalizationSettings.SelectedLocaleChanged += LocalizationSettings_SelectedLocaleChanged;
    }

    private void OnDisable()
    {
        CollectableUI.OnCollectableSelected -= CollectableUI_OnCollectableSelected;
        LocalizationSettings.SelectedLocaleChanged -= LocalizationSettings_SelectedLocaleChanged;
    }

    private void UpdateUI(CollectableUI collectableUI)
    {
        currentCollectableUI = collectableUI;

        SetImageByCurrentCollectable();
        SetNameTextByCurrentCollectable();
        SetDescriptionTextByCurrentCollectable();
    }

    private void SetNameTextByCurrentCollectable()
    {
        if (currentCollectableUI == null) return;

        if (currentCollectableUI.IsCollected) collectableNameText.text = LocalizationSettings.StringDatabase.GetLocalizedString(currentCollectableUI.CollectableSO.localizationTable, currentCollectableUI.CollectableSO.nameLocalizationBinding);
        else collectableNameText.text = LocalizationSettings.StringDatabase.GetLocalizedString(stringLocalizationTable, notCollectedNameLocalizationBinding);
    }

    private void SetDescriptionTextByCurrentCollectable()
    {
        if (currentCollectableUI == null) return;

        collectableDescriptionText.text = LocalizationSettings.StringDatabase.GetLocalizedString(currentCollectableUI.CollectableSO.localizationTable, currentCollectableUI.CollectableSO.descriptionLocalizationBinding);
    }

    private void SetImageByCurrentCollectable()
    {
        if (currentCollectableUI == null) return;

        collectableImage.sprite = currentCollectableUI.CollectableSO.collectableSprite;

        if (currentCollectableUI.IsCollected) collectableImage.color = currentCollectableUI.CollectableSO.collectedColor;
        else collectableImage.color = currentCollectableUI.CollectableSO.notCollectedColor;
    }

    #region Subscriptions
    private void CollectableUI_OnCollectableSelected(object sender, CollectableUI.OnCollectableUIEventArgs e)
    {
        UpdateUI(e.collectableUI);
    }

    private void LocalizationSettings_SelectedLocaleChanged(Locale locale)
    {
        SetNameTextByCurrentCollectable();
        SetDescriptionTextByCurrentCollectable();
    }

    #endregion
}
