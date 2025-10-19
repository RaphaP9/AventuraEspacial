using UnityEngine;
using UnityEngine.UI;
using System;

public class CollectableUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Button collectableButton;
    [SerializeField] private Image collectableImage;

    [Header("Runtime Filled")]
    [SerializeField] private CollectableSO collectableSO;
    [SerializeField] private bool isCollected;

    public CollectableSO CollectableSO => collectableSO;
    public bool IsCollected => isCollected;
    
    public static event EventHandler<OnCollectableUIEventArgs> OnCollectableUISet;
    public static event EventHandler<OnCollectableUIEventArgs> OnCollectableSelected;

    public class OnCollectableUIEventArgs : EventArgs
    {
        public CollectableUI collectableUI;
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

        if (isCollected) collectableImage.color = collectableSO.collectedColor;
        else collectableImage.color = collectableSO.notCollectedColor;
    }

    private void InitializeButtonListener()
    {
        collectableButton.onClick.RemoveAllListeners();
        collectableButton.onClick.AddListener(SelectCollectable);
    }

    private void SelectCollectable()
    {
        OnCollectableSelected?.Invoke(this, new OnCollectableUIEventArgs { collectableUI = this });
    }
}
