using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class MemoryCardHandler : MonoBehaviour, IPointerClickHandler
{
    [Header("UI Components")]
    [SerializeField] private Image frontImage;
    [SerializeField] private Image backImage;

    [Header("Components")]
    [SerializeField] private MemoryCardAnimationController animatorController;

    [Header("Runtime Filled")]
    [SerializeField] private MemoryCardSO memoryCardSO;
    [SerializeField] private bool isRevealed; //NOTE: isRevealed is also manipulated by Animation Events
    [SerializeField] private bool isMatched;

    public static event EventHandler<OnCardRevealedEventArgs> OnCardRevealed;

    public MemoryCardSO MemoryCardSO => memoryCardSO;

    public class OnCardRevealedEventArgs : EventArgs
    {
        public MemoryCardHandler memoryCardHandler;
    }

    public void SetMemoryCard(MemoryCardSO memoryCardSO)
    {
        this.memoryCardSO = memoryCardSO;

        isRevealed = false;
        isMatched = false;

        SetMemoryCardImage(memoryCardSO.sprite);
    }

    private void SetMemoryCardImage(Sprite sprite) => frontImage.sprite = sprite;   
    public void SetBackSprite(Sprite sprite) => backImage.sprite = sprite;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isRevealed) return;
        if (isMatched) return;
        if (!MemoryMinigameManager.Instance.CanFlipCard()) return;

        RevealCard();
        MemoryMinigameManager.Instance.SetInputLockCooldown();
    }

    public void RevealCard()
    {
        animatorController.PlayRevealAnimation();
        isRevealed = true;

        OnCardRevealed?.Invoke(this, new OnCardRevealedEventArgs { memoryCardHandler = this });
    }

    public void CoverCard()
    {
        animatorController.PlayCoverAnimation();
    }

    public void MatchCard()
    {
        animatorController.PlayMatchAnimation();
        isMatched = true;   
    }

    public void FailMatch()
    {
        animatorController.PlayFailAnimation();
    }

    public void DisappearCard()
    {
        animatorController.PlayDisappearAnimation();
    }

    public void OnRevealBegin() => isRevealed = true;
    public void OnCoverCompleted() => isRevealed = false;
}
