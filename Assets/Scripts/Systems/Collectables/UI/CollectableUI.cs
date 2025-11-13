using UnityEngine;
using UnityEngine.UI;
using System;

public class CollectableUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Button collectableButton;
    [SerializeField] private Image collectableImage;
    [SerializeField] private Image collectableBackground;

    [Header("Runtime Filled")]
    [SerializeField] private CollectableSO collectableSO;
    [SerializeField] private bool isCollected;

    public CollectableSO CollectableSO => collectableSO;
    public bool IsCollected => isCollected;
    
    public static event EventHandler<OnCollectableUIEventArgs> OnCollectableUISet;
    public static event EventHandler<OnCollectableUIEventArgs> OnCollectableUIClicked;

    public event EventHandler<OnCollectableSelectEventArgs> OnCollectableUISelected;
    public event EventHandler<OnCollectableSelectEventArgs> OnCollectableUIDeselected;

    public class OnCollectableUIEventArgs : EventArgs
    {
        public CollectableUI collectableUI;
    }

    public class OnCollectableSelectEventArgs: EventArgs
    {
        public bool instant;
    }

    public void SetCollectable(CollectableSO collectableSO)
    {
        this.collectableSO = collectableSO;
        isCollected = DataContainer.Instance.HasCollectedCollectable(collectableSO);

        UpdateImage();
        InitializeButtonListener();

        OnCollectableUISet?.Invoke(this, new OnCollectableUIEventArgs { collectableUI = this });
    }

    private void UpdateImage()
    {
        if (collectableSO == null) return;

        collectableImage.sprite = collectableSO.collectableSprite;

        if (isCollected)
        {
            collectableImage.material = null;
            collectableBackground.color = collectableSO.collectedBackgroundColor;
        }
        else
        {
            collectableImage.material = collectableSO.notCollectedMaterial;
            collectableBackground.color = collectableSO.notCollectedBackgroundColor;
        }
    }

    private void InitializeButtonListener()
    {
        collectableButton.onClick.RemoveAllListeners();
        collectableButton.onClick.AddListener(OnCollectableUIClick);
    }

    private void OnCollectableUIClick()
    {
        OnCollectableUIClicked?.Invoke(this, new OnCollectableUIEventArgs { collectableUI = this });
    }

    public void SelectCollectable(bool instant)
    {
        OnCollectableUISelected?.Invoke(this, new OnCollectableSelectEventArgs { instant = instant});
    }
    public void DeselectCollectable(bool instant)
    {
        OnCollectableUIDeselected?.Invoke(this, new OnCollectableSelectEventArgs { instant = instant });
    }
}
