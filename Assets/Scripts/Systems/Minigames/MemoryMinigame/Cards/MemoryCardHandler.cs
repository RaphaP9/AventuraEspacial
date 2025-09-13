using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MemoryCardHandler : MonoBehaviour, IPointerClickHandler
{
    [Header("UI Components")]
    [SerializeField] private Image frontImage;
    [SerializeField] private Image backImage;

    [Header("Components")]
    [SerializeField] private MemoryCardAnimationController animatorController;

    [Header("Runtime Filled")]
    [SerializeField] private MemoryCardSO memoryCardSO;
    [SerializeField] private bool isRevealed;
    [SerializeField] private bool isMatched;

    public void SetMemoryCard(MemoryCardSO memoryCardSO)
    {
        this.memoryCardSO = memoryCardSO;
        SetMemoryCardImage(memoryCardSO.sprite);
    }

    private void SetMemoryCardImage(Sprite sprite) => frontImage.sprite = sprite;   
    public void SetBackImage(Sprite sprite) => backImage.sprite = sprite;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isRevealed) return;
        if (isMatched) return;
        if (!MemoryMinigameManager.Instance.CanFlipCard()) return;

        Debug.Log("Clicked");
    }
}
