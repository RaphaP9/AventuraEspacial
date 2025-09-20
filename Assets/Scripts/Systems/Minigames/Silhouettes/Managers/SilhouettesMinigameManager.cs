using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SilhouettesMinigameManager : MinigameManager
{
    public static SilhouettesMinigameManager Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private SilhouettesMinigameSettings settings;
    [Space]
    [Space]
    [SerializeField] private Transform figurePrefab;
    [SerializeField] private Transform silhouettePrefab;

    [Header("RuntimeFilled")]
    [SerializeField] private MiniGameState miniGameState;
    [SerializeField] private SilhouettesRound currentRound;
    [Space]
    [SerializeField] private List<FigureHandler> currentRoundFigures;
    [SerializeField] private List<SilhouetteHandler> currentRoundSilhouettes;
    [Space]
    [SerializeField] private List<FigureHandler> currentMatchedFigures;
    [SerializeField] private List<SilhouetteHandler> currentMatchedSilhouettes;
    [Space]
    [SerializeField] private FigureHandler lastDraggedFigure;
    [SerializeField] private SilhouetteHandler lastPointerOnSilhouette;

    [Header("Debug")]
    [SerializeField] private bool debug;
    private enum MiniGameState { StartingMinigame, WaitForFigureDragging, DraggingFigure, ProcessingSilhouette, EndingRound, SwitchingRound, Winning, Win, Losing, Lose }
    private enum SilhouetteProcessResult {NotDraggedOntoSilhouette, Match, Fail }

    private bool gameEnded = false;
    private bool gameWon = false;
    private bool gameLost = false;
    private int currentRoundIndex = 0;

    private bool draggingFigure = false;

    #region Events
    public static event EventHandler<OnRoundEventArgs> OnRoundStart;
    public static event EventHandler<OnRoundEventArgs> OnRoundEnd;

    public static event EventHandler OnSilhouetteMatch;
    public static event EventHandler OnSilhouetteFailed;
    public static event EventHandler OnFigureReturnToOriginalPosition;

    public static event EventHandler OnGameInitialized;
    #endregion

    #region Custom Classes
    public class OnRoundEventArgs : EventArgs
    {
        public SilhouettesRound silhouettesRound;
        public int roundIndex;
        public int totalRounds;
    }
    #endregion

    private void OnEnable()
    {
        FigureHandler.OnFigureDragStart += FigureHandler_OnFigureDragStart;
        FigureHandler.OnFigureDragEnd += FigureHandler_OnFigureDragEnd;

        SilhouetteHandler.OnSilhouettePointerEnter += SilhouetteHandler_OnSilhouettePointerEnter;
        SilhouetteHandler.OnSilhouettePointerExit += SilhouetteHandler_OnSilhouettePointerExit;

        MinigameTimerManager.OnTimeEnd += MinigameTimerManager_OnTimeEnd;
    }

    private void OnDisable()
    {
        FigureHandler.OnFigureDragStart -= FigureHandler_OnFigureDragStart;
        FigureHandler.OnFigureDragEnd -= FigureHandler_OnFigureDragEnd;

        SilhouetteHandler.OnSilhouettePointerEnter -= SilhouetteHandler_OnSilhouettePointerEnter;
        SilhouetteHandler.OnSilhouettePointerExit -= SilhouetteHandler_OnSilhouettePointerExit;

        MinigameTimerManager.OnTimeEnd -= MinigameTimerManager_OnTimeEnd;
    }

    private void Awake()
    {
        SetSingleton();
    }

    private void Start()
    {
        InitializeVariables();
        StartCoroutine(SilhouettesMinigameCoroutine());
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
    private IEnumerator SilhouettesMinigameCoroutine()
    {
        SetMinigameState(MiniGameState.StartingMinigame);

        yield return new WaitForSeconds(settings.startingGameTime);

        while (!gameEnded)
        {
            yield return StartCoroutine(SilhouettesRoundCoroutine(settings.rounds[currentRoundIndex], currentRoundIndex));

            #region Minigame Completed Evaluation
            if (currentRoundIndex >= settings.rounds.Count - 1)
            {
                gameEnded = true;
            }
            else
            {
                currentRoundIndex++;
            }
            #endregion
        }

        yield return StartCoroutine(WinMinigameCoroutine());
    }

    private IEnumerator SilhouettesRoundCoroutine(SilhouettesRound silhouetteRound, int roundIndex)
    {
        List<SilhouetteSO> chosenSilhouettes = GeneralUtilities.ChooseNRandomDifferentItemsFromPoolFisherYates(silhouetteRound.silhouettesPool, silhouetteRound.silhouettesCount);

        //CreateBackpack
        //CreateFigures
        //CreateSilhouettes

        OnRoundStart?.Invoke(this, new OnRoundEventArgs { silhouettesRound = silhouetteRound, roundIndex = roundIndex, totalRounds = settings.rounds.Count });

        bool roundEnded = false;

        while (!roundEnded)
        {
            #region Wait For Figure Dragging
            SetMinigameState(MiniGameState.WaitForFigureDragging);

            yield return new WaitUntil(() => draggingFigure);
            FigureHandler draggedFigure = lastDraggedFigure;
            #endregion

            #region Figure Dragging
            SetMinigameState(MiniGameState.DraggingFigure);
            yield return new WaitUntil(() => !draggingFigure);
            #endregion

            #region Silhouette Processing - 3 cases
            SetMinigameState(MiniGameState.ProcessingSilhouette);

            if(GetSilhouetteProcessingResult(draggedFigure, lastPointerOnSilhouette) == SilhouetteProcessResult.Match)
            {
                currentMatchedFigures.Add(draggedFigure);
                currentMatchedSilhouettes.Add(lastPointerOnSilhouette);
            }

            ProcessSilhouette(draggedFigure, lastPointerOnSilhouette);

            lastDraggedFigure = null;
            lastPointerOnSilhouette = null;
            #endregion

            #region Round End Evaluation
            if (AllSilhouettesMatch())
            {
                SetMinigameState(MiniGameState.EndingRound);

                yield return new WaitForSeconds(settings.allSilhouettesMatchTime);

                OnRoundEnd?.Invoke(this, new OnRoundEventArgs { silhouettesRound = silhouetteRound, roundIndex = roundIndex, totalRounds = settings.rounds.Count });

                if (IsLastRound(roundIndex))
                {
                    yield return new WaitForSeconds(settings.endLastRoundTimer);
                }
                else
                {
                    SetMinigameState(MiniGameState.SwitchingRound);
                    yield return new WaitForSeconds(settings.switchRoundTimer);
                }

                currentMatchedFigures.Clear();
                currentMatchedSilhouettes.Clear();

                currentRoundFigures.Clear();
                currentRoundSilhouettes.Clear();

                roundEnded = true;

                //ClearBackpack
                //ClearFigures
                //ClearSilhouettes
            }
            else
            {
                yield return new WaitForSeconds(settings.timeBetweenSilhouettes);
            }
            #endregion
        }
    }

    private IEnumerator WinMinigameCoroutine()
    {
        SetMinigameState(MiniGameState.Winning);
        OnGameWinningMethod();

        yield return new WaitForSeconds(settings.endingGameTime);

        SetMinigameState(MiniGameState.Win);
        OnGameWonMethod();
    }

    private IEnumerator LoseMinigameByTimeCoroutine()
    {
        SetMinigameState(MiniGameState.Losing);
        OnGameLosingMethod();

        yield return new WaitForSeconds(settings.endingGameTime);

        SetMinigameState(MiniGameState.Lose);
        OnGameLostMethod();
    }
    #endregion

    #region Setters
    private void SetMinigameState(MiniGameState state) => miniGameState = state;

    #endregion

    #region Silhouette Processing
    private void ProcessSilhouette(FigureHandler figure, SilhouetteHandler silhouette)
    {
        SilhouetteProcessResult processResult = GetSilhouetteProcessingResult(figure, silhouette);

        switch (processResult)
        {
            case SilhouetteProcessResult.NotDraggedOntoSilhouette:
                figure.ReturnToOriginalPosition();
                OnFigureReturnToOriginalPosition?.Invoke(this, EventArgs.Empty);
                break;
            case SilhouetteProcessResult.Match:
                OnSilhouetteMatch?.Invoke(this, EventArgs.Empty);
                break;
            case SilhouetteProcessResult.Fail:
                OnSilhouetteFailed?.Invoke(this, EventArgs.Empty);
                break;
        }
    }

    private SilhouetteProcessResult GetSilhouetteProcessingResult(FigureHandler figure, SilhouetteHandler silhouette)
    {
        if (silhouette == null) return SilhouetteProcessResult.NotDraggedOntoSilhouette;

        if (figure.SilhouetteSO == silhouette.SilhouetteSO) return SilhouetteProcessResult.Match;
        else return SilhouetteProcessResult.Fail;
    }

    private bool AllSilhouettesMatch() => currentMatchedFigures.Count >= currentRoundFigures.Count; //Can also check with silhouettes or both figures and silhouettes
    private bool IsLastRound(int roundIndex) => roundIndex + 1 >= settings.rounds.Count;

    #endregion

    #region Public Methods
    public bool CanDragSilhouette() => miniGameState == MiniGameState.WaitForFigureDragging;
    public override bool CanPassTime() => miniGameState == MiniGameState.WaitForFigureDragging || miniGameState == MiniGameState.DraggingFigure;

    public void LoseMinigameByTime()
    {
        StopAllCoroutines();
        StartCoroutine(LoseMinigameByTimeCoroutine());
    }
    #endregion

    #region Subscriptions
    private void FigureHandler_OnFigureDragStart(object sender, FigureHandler.OnFigureEventArgs e)
    {
        lastDraggedFigure = e.figureHandler;
        draggingFigure = true;
    }

    private void FigureHandler_OnFigureDragEnd(object sender, FigureHandler.OnFigureEventArgs e)
    {
        lastDraggedFigure = e.figureHandler;
        draggingFigure = false;
    }
    private void SilhouetteHandler_OnSilhouettePointerEnter(object sender, SilhouetteHandler.OnSilhouetteEventArgs e)
    {
        lastPointerOnSilhouette = e.silhouetteHandler;
    }

    private void SilhouetteHandler_OnSilhouettePointerExit(object sender, SilhouetteHandler.OnSilhouetteEventArgs e)
    {
        if (lastPointerOnSilhouette == e.silhouetteHandler) lastPointerOnSilhouette = null;
    }

    private void MinigameTimerManager_OnTimeEnd(object sender, EventArgs e)
    {
        LoseMinigameByTime();
    }
    #endregion
}
