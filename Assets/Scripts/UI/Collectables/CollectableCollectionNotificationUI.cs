using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;
using UnityEngine.Video;
using System.Collections;

public class CollectableCollectionNotificationUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Image collectableImage;
    [SerializeField] private Image collectableBackground;
    [SerializeField] private TextMeshProUGUI collectableNameText;
    [SerializeField] private Animator animator;

    [Header("Settings")]
    [SerializeField, Range(3f,10f)] private float timeShowing;
    [SerializeField] private bool useRealtime;

    [Header("Runtime Filled")]
    [SerializeField] private CollectableSO collectableSO;

    private const string HIDE_TRIGGER = "Hide";

    private void OnEnable()
    {
        LocalizationSettings.SelectedLocaleChanged += LocalizationSettings_SelectedLocaleChanged;
    }

    private void OnDisable()
    {
        LocalizationSettings.SelectedLocaleChanged -= LocalizationSettings_SelectedLocaleChanged;
    }

    private void Start()
    {
        StartCoroutine(ShowCoroutine());
    }

    private IEnumerator ShowCoroutine()
    {
        if (useRealtime) yield return new WaitForSecondsRealtime(timeShowing);
        else yield return new WaitForSeconds(timeShowing);

        animator.SetTrigger(HIDE_TRIGGER);
    }

    public void SetUI(CollectableSO collectableSO)
    {
        this.collectableSO = collectableSO;

        SetCollectableImage();
        SetCollectableBackground();

        LocalizeCollectableNameText();
    }

    private void SetCollectableImage()
    {
        if (collectableSO == null) return;

        collectableImage.sprite = collectableSO.collectableSprite;
    }

    private void SetCollectableBackground()
    {
        if (collectableSO == null) return;

        collectableBackground.color = collectableSO.collectedBackgroundColor;
    }


    private void LocalizeCollectableNameText()
    {
        if (collectableSO == null) return;

        collectableNameText.text = LocalizationSettings.StringDatabase.GetLocalizedString(collectableSO.localizationTable, collectableSO.nameLocalizationBinding);
    }

    #region Subscriptions
    private void LocalizationSettings_SelectedLocaleChanged(UnityEngine.Localization.Locale obj)
    {
        LocalizeCollectableNameText();
    }
    #endregion
}
