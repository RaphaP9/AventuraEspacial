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
    [SerializeField] private List<MemoryCardHandler> currentMatchedCards;
    [Space]
    [SerializeField] private List<MemoryCardHandler> currentRevealedCards;
    [Space]
    [SerializeField] private MemoryCardHandler lastRevealedCard;

    [Header("Debug")]
    [SerializeField] private bool debug;

    private enum MiniGameState { StartingMinigame, RevealingCards, WaitForFirstCard, WaitForSecondCard, ProcessingPair, EndingRound, SwitchingRound, Win, Lose}

    private bool gameEnded = false;
    private bool gameWon = false;
    private bool gameLost = false;
    private int currentRoundIndex = 0;

    private bool cardRevealed = false;

    private const float PAIR_PROCESSING_TIME = 0.5f;

    #region Events

    public static event EventHandler<OnRevealTimeEventArgs> OnRevealTimeStart;
    public static event EventHandler<OnRevealTimeEventArgs> OnRevealTimeEnd;

    public static event EventHandler<OnRoundEventArgs> OnRoundStart;
    public static event EventHandler<OnRoundEventArgs> OnRoundEnd;

    public static event EventHandler OnPairMatch;
    public static event EventHandler OnPairFailed;

    public static event EventHandler OnGameInitialized;
    public static event EventHandler OnGameLost;
    public static event EventHandler OnGameWon;
    #endregion

    #region Custom Classes
    public class OnRoundEventArgs : EventArgs
    {
        public MemoryRound memoryRound;
        public int roundIndex;
        public int totalRounds;
    }

    public class OnRevealTimeEventArgs : EventArgs
    {
        public float revealTime;
    }
    #endregion

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

        OnGameInitialized?.Invoke(this, EventArgs.Empty);   
    }

    #region Coroutines
    private IEnumerator MemoryMinigameCoroutine()
    {
        ClearCardsContainer();

        SetMinigameState(MiniGameState.StartingMinigame);

        yield return new WaitForSeconds(settings.startingGameTime);

        while (!gameEnded)
        {
            yield return StartCoroutine(MemoryRoundCoroutine(settings.rounds[currentRoundIndex], currentRoundIndex));

            #region Minigame Completed Evaluation
            if(currentRoundIndex>= settings.rounds.Count -1)
            {
                gameEnded = true;
            }
            else
            {
                currentRoundIndex++;
            }
            #endregion
        }

        SetMinigameState(MiniGameState.Win);
        OnGameWon?.Invoke(this, EventArgs.Empty);
    }

    private IEnumerator MemoryRoundCoroutine(MemoryRound memoryRound, int roundIndex)
    {
        SetUpGridLayout(memoryRound);

        List<MemoryCardSO> chosenPairs = GeneralUtilities.ChooseNRandomDifferentItemsFromPoolFisherYates(settings.cardPool, memoryRound.pairCount);
        CreateCards(chosenPairs, memoryRound);

        OnRoundStart?.Invoke(this, new OnRoundEventArgs { memoryRound = memoryRound, roundIndex = roundIndex, totalRounds = settings.rounds.Count }); 

        SetMinigameState(MiniGameState.RevealingCards);

        float revealTime = memoryRound.revealTime;

        OnRevealTimeEnd?.Invoke(this, new OnRevealTimeEventArgs { revealTime = revealTime });

        yield return new WaitForSeconds(revealTime);

        OnRevealTimeEnd?.Invoke(this, new OnRevealTimeEventArgs { revealTime = revealTime });

        CoverCards(currentRoundCards);

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

            #region Pair Processing
            SetMinigameState(MiniGameState.ProcessingPair);

            if(PairMatches(firstCard, secondCard)) //Instant Pair Processing
            {
                currentMatchedCards.Add(firstCard);
                currentMatchedCards.Add(secondCard);
            }

            StartCoroutine(ProcessPairCoroutine(firstCard, secondCard)); //Card Pair Processing (Separate Coroutine)

            currentRevealedCards.Clear();
            #endregion

            #region Round End Evaluation
            if(AllPairMatch())
            {
                SetMinigameState(MiniGameState.EndingRound);

                yield return new WaitForSeconds(settings.allPairsMatchTime);

                OnRoundEnd?.Invoke(this, new OnRoundEventArgs { memoryRound = memoryRound, roundIndex = roundIndex, totalRounds = settings.rounds.Count });

                DisappearCards(currentRoundCards);

                SetMinigameState(MiniGameState.SwitchingRound);
                yield return new WaitForSeconds(settings.switchRoundTimer);

                currentMatchedCards.Clear();
                currentRoundCards.Clear();
                lastRevealedCard = null;

                roundEnded = true;

                ClearCardsContainer();
            }
            else
            {
                yield return new WaitForSeconds(settings.timeBetweenPairs);
            }
            #endregion
        }
    }

    #endregion

    #region Setters
    private void SetUpGridLayout(MemoryRound memoryRound)
    {
        int columns = memoryRound.gridColumnCount;
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = columns;
    }

    private void CreateCards(List<MemoryCardSO> chosenPairs, MemoryRound memoryRound)
    {
        List<MemoryCardSO> cardList = new List<MemoryCardSO>(chosenPairs); //Add the chosen pairs
        cardList.AddRange(chosenPairs); //Add the corresponding second copy

        cardList = GeneralUtilities.FisherYatesShuffle(cardList);

        foreach (MemoryCardSO memoryCardSO in cardList)
        {
            CreateCard(memoryCardSO, memoryRound);
        }
    }

    private void CreateCard(MemoryCardSO memoryCardSO, MemoryRound memoryRound)
    {
        Transform createdCard = Instantiate(cardPrefab, cardsContainer);
        MemoryCardHandler memoryCardHandler = createdCard.GetComponent<MemoryCardHandler>();

        if (memoryCardHandler == null)
        {
            if (debug) Debug.Log("Instantiated card does not contain a MemoryCardHandler component.");
            return;
        }

        memoryCardHandler.SetMemoryCard(memoryCardSO);
        memoryCardHandler.SetBackSprite(memoryRound.cardBackSprite);
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

    #region Pair Processing

    private IEnumerator ProcessPairCoroutine(MemoryCardHandler firstCard, MemoryCardHandler secondCard)
    {
        List<MemoryCardHandler> evaluatedCards = new List<MemoryCardHandler> { firstCard, secondCard};

        yield return new WaitForSeconds(PAIR_PROCESSING_TIME);

        if (PairMatches(firstCard, secondCard))
        {
            MatchCards(evaluatedCards);
            OnPairMatch?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            FailCards(evaluatedCards);
            OnPairFailed?.Invoke(this, EventArgs.Empty);
        }
    }

    private bool PairMatches(MemoryCardHandler firstCard, MemoryCardHandler secondCard) => firstCard.MemoryCardSO == secondCard.MemoryCardSO;
    private bool AllPairMatch() => currentMatchedCards.Count >= currentRoundCards.Count;
    #endregion

    #region Cards

    private void CoverCards(List<MemoryCardHandler> memoryCardHandlers)
    {
        foreach (MemoryCardHandler memoryCardHandler in memoryCardHandlers)
        {
            memoryCardHandler.CoverCard();
        }
    }

    private void MatchCards(List<MemoryCardHandler> memoryCardHandlers)
    {
        foreach (MemoryCardHandler memoryCardHandler in memoryCardHandlers)
        {
            memoryCardHandler.MatchCard();
        }
    }

    private void FailCards(List<MemoryCardHandler> memoryCardHandlers)
    {
        foreach (MemoryCardHandler memoryCardHandler in memoryCardHandlers)
        {
            memoryCardHandler.FailMatch();
        }
    }

    private void DisappearCards(List<MemoryCardHandler> memoryCardHandlers)
    {
        foreach (MemoryCardHandler memoryCardHandler in memoryCardHandlers)
        {
            memoryCardHandler.DisappearCard();
        }
    }

    #endregion

    public bool CanFlipCard() => miniGameState == MiniGameState.WaitForFirstCard || miniGameState == MiniGameState.WaitForSecondCard;

    public void LoseMinigame()
    {
        StopAllCoroutines();

        SetMinigameState(MiniGameState.Lose);
        OnGameLost?.Invoke(this, EventArgs.Empty);
    }

    #region
    private void MemoryCardHandler_OnCardRevealed(object sender, MemoryCardHandler.OnCardRevealedEventArgs e)
    {
        currentRevealedCards.Add(e.memoryCardHandler);
        lastRevealedCard = e.memoryCardHandler;
        cardRevealed = true;
    }

    #endregion
}
    