using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MemoryMinigameManager : MonoBehaviour
{
    public static MemoryMinigameManager Instance {  get; private set; }

    [Header("Components")]
    [SerializeField] private MemoryMinigameSettings settings;
    [Space]
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    [SerializeField] private Transform cardsContainer;
    [Space]
    [SerializeField] private Transform cardPrefab;

    [Header("RuntimeFilled")]
    [SerializeField] private MiniGameState miniGameState;
    [SerializeField] private MemoryRound currentRound;
    [Space]
    [SerializeField] private List<MemoryCardHandler> currentRoundCards;
    [Space]
    [SerializeField] private List<MemoryCardHandler> currentRevealedCards;
    [Space]
    [SerializeField] private MemoryCardHandler lastRevealedCard;

    [Header("Debug")]
    [SerializeField] private bool debug;

    private enum MiniGameState { StartingMinigame, RevealingCards, WaitForFirstCard, WaitForSecondCard, ProcessingPair, CompletedRound, Win, Lose}

    private bool gameEnded = false;
    private bool gameWon = false;
    private bool gameLost = false;
    private int currentRoundIndex = 0;

    private bool cardRevealed = false;

    public static event EventHandler OnRevealTimeEnd;

    private const float PAIR_PROCESSING_TIME = 0.5f;

    private void OnEnable()
    {
        MemoryCardHandler.OnCardRevealed += MemoryCardHandler_OnCardRevealed;
    }

    private void OnDisable()
    {
        MemoryCardHandler.OnCardRevealed -= MemoryCardHandler_OnCardRevealed;
    }

    private void Awake()
    {
        SetSingleton();
    }

    private void Start()
    {
        InitializeVariables();
        StartCoroutine(MemoryMinigameCoroutine());
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //Debug.LogWarning("There is more than one MemoryMinigameManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void InitializeVariables()
    {
        gameEnded = false;
        gameWon = false;
        gameLost = false;
        currentRoundIndex = 0;
    }

    #region Coroutines
    private IEnumerator MemoryMinigameCoroutine()
    {
        ClearCardsContainer();

        SetMinigameState(MiniGameState.StartingMinigame);

        yield return new WaitForSeconds(settings.startingGameTime);

        while (!gameEnded)
        {
            yield return StartCoroutine(MemoryRoundCoroutine(currentRoundIndex));
        }
    }

    private IEnumerator MemoryRoundCoroutine(int roundIndex)
    {
        SetUpGridLayout(roundIndex);

        List<MemoryCardSO> chosenPairs = GeneralUtilities.ChooseNRandomDifferentItemsFromPoolFisherYates(settings.cardPool, settings.rounds[roundIndex].pairCount);
        CreateCards(chosenPairs);

        SetMinigameState(MiniGameState.RevealingCards);

        yield return new WaitForSeconds(settings.rounds[roundIndex].revealTime);

        OnRevealTimeEnd?.Invoke(this, EventArgs.Empty);

        CoverAllCards(currentRoundCards);

        bool roundEnded = false;

        while (!roundEnded)
        {
            #region FirstCard
            SetMinigameState(MiniGameState.WaitForFirstCard);

            yield return new WaitUntil(() => cardRevealed);
            cardRevealed = false;

            MemoryCardHandler firstCard = lastRevealedCard;
            #endregion

            #region SecondCard
            SetMinigameState(MiniGameState.WaitForSecondCard);

            yield return new WaitUntil(() => cardRevealed);
            cardRevealed = false;

            MemoryCardHandler secondCard = lastRevealedCard;
            #endregion

            SetMinigameState(MiniGameState.ProcessingPair);

            StartCoroutine(ProcessPairCoroutine(firstCard, secondCard));

            yield return new WaitForSeconds(settings.timeBetweenPairs);

            currentRevealedCards.Clear();
        }
    }

    #endregion

    #region Setters
    private void SetUpGridLayout(int roundIndex)
    {
        int columns = settings.rounds[roundIndex].gridColumnCount;
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = columns;
    }

    private void CreateCards(List<MemoryCardSO> chosenPairs)
    {
        List<MemoryCardSO> cardList = new List<MemoryCardSO>(chosenPairs); //Add the chosen pairs
        cardList.AddRange(chosenPairs); //Add the corresponding second copy

        cardList = GeneralUtilities.FisherYatesShuffle(cardList);

        foreach (MemoryCardSO memoryCardSO in cardList)
        {
            CreateCard(memoryCardSO);
        }
    }

    private void CreateCard(MemoryCardSO memoryCardSO)
    {
        Transform createdCard = Instantiate(cardPrefab, cardsContainer);
        MemoryCardHandler memoryCardHandler = createdCard.GetComponent<MemoryCardHandler>();

        if (memoryCardHandler == null)
        {
            if (debug) Debug.Log("Instantiated card does not contain a MemoryCardHandler component.");
            return;
        }

        memoryCardHandler.SetMemoryCard(memoryCardSO);
        currentRoundCards.Add(memoryCardHandler);
    }

    private void ClearCardsContainer()
    {
        for (int i = cardsContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(cardsContainer.GetChild(i).gameObject);
        }
    }

    private void SetMinigameState(MiniGameState state) => miniGameState = state;

    #endregion

    #region Cards

    private void CoverAllCards(List<MemoryCardHandler> memoryCardHandlers)
    {
        foreach(MemoryCardHandler memoryCardHandler in memoryCardHandlers)
        {
            memoryCardHandler.CoverCard();
        }
    }

    private void MatchRevealedCards(List<MemoryCardHandler> memoryCardHandlers)
    {
        foreach (MemoryCardHandler memoryCardHandler in memoryCardHandlers)
        {
            memoryCardHandler.MatchCard();
        }
    }

    private void FailRevealedCards(List<MemoryCardHandler> memoryCardHandlers)
    {
        foreach (MemoryCardHandler memoryCardHandler in memoryCardHandlers)
        {
            memoryCardHandler.FailMatch();
        }
    }

    #endregion

    #region Pair Processing

    private IEnumerator ProcessPairCoroutine(MemoryCardHandler firstCard, MemoryCardHandler secondCard)
    {
        List<MemoryCardHandler> evaluatedCards = new List<MemoryCardHandler> { firstCard, secondCard};

        yield return new WaitForSeconds(PAIR_PROCESSING_TIME);

        if (firstCard.MemoryCardSO == secondCard.MemoryCardSO)
        {
            MatchRevealedCards(evaluatedCards);
        }
        else
        {
            FailRevealedCards(evaluatedCards);
        }
    }
    #endregion
    public bool CanFlipCard() => miniGameState == MiniGameState.WaitForFirstCard || miniGameState == MiniGameState.WaitForSecondCard;

    #region
    private void MemoryCardHandler_OnCardRevealed(object sender, MemoryCardHandler.OnCardRevealedEventArgs e)
    {
        currentRevealedCards.Add(e.memoryCardHandler);
        lastRevealedCard = e.memoryCardHandler;
        cardRevealed = true;
    }

    #endregion
}
    