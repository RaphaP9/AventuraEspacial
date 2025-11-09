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
    [SerializeField] private bool isBeingFlippedReveal; //NOTE: isRevealed is also manipulated by Animation Events
    [SerializeField] private bool isMatched;
    [SerializeField] private bool isFailing; //Manipulated by Animation Events

    public static event EventHandler<OnCardRevealedEventArgs> OnCardRevealed;

    public event EventHandler OnCardMatch;
    public event EventHandler OnThisCardRevealed;

    public bool IsRevealed => isRevealed; //As soon as it is being show isRevealed is TRUE
    public bool IsMatched => isMatched;
    public bool IsFailing => isFailing;
    public bool IsBeingFlippedReveal => isBeingFlippedReveal;

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
        isFailing = false;

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
        OnThisCardRevealed?.Invoke(this, EventArgs.Empty);
    }

    public void CoverCard()
    {
        animatorController.PlayCoverAnimation();
    }

    public void MatchCard()
    {
        animatorController.PlayMatchAnimation();
        isMatched = true;   

        OnCardMatch?.Invoke(this, EventArgs.Empty);
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

    public void OnFlipRevealBegin() => isBeingFlippedReveal = true;
    public void OnFlipRevealCompleted() => isBeingFlippedReveal = false;

    public void OnFailBegin() => isFailing = true;
    public void OnFailCompleted() => isFailing = false;
}
