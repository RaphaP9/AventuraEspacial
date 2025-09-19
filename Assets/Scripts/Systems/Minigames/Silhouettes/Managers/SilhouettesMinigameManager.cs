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

    [Header("Debug")]
    [SerializeField] private bool debug;
    private enum MiniGameState { StartingMinigame, RevealingCards, WaitForFigureSelection, DraggingFigure, ProcessingSilhouette, EndingRound, SwitchingRound, Winning, Win, Losing, Lose }

    private bool gameEnded = false;
    private bool gameWon = false;
    private bool gameLost = false;
    private int currentRoundIndex = 0;

    #region Events
    public static event EventHandler<OnRoundEventArgs> OnRoundStart;
    public static event EventHandler<OnRoundEventArgs> OnRoundEnd;

    public static event EventHandler OnSilhouetteMatch;
    public static event EventHandler OnSilhouetteFailed;

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
        MinigameTimerManager.OnTimeEnd += MinigameTimerManager_OnTimeEnd;
    }

    private void OnDisable()
    { 
    
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
        yield return null;
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

    #region Public Methods
    public bool CanDragSilhouette() => miniGameState == MiniGameState.WaitForFigureSelection;
    public override bool CanPassTime() => miniGameState == MiniGameState.WaitForFigureSelection || miniGameState == MiniGameState.DraggingFigure;

    public void LoseMinigameByTime()
    {
        StopAllCoroutines();
        StartCoroutine(LoseMinigameByTimeCoroutine());
    }
    #endregion

    #region Subscriptions
    private void MinigameTimerManager_OnTimeEnd(object sender, EventArgs e)
    {
        LoseMinigameByTime();
    }
    #endregion
}
