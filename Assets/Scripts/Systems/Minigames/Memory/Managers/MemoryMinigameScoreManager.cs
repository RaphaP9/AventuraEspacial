using UnityEngine;
using System;

public class MemoryMinigameScoreManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private MemoryMinigameSettings settings;

    [Header("Runtime Filled")]
    [SerializeField] private int currentScore;
    [SerializeField] private int currentCombo;
    [SerializeField] private int currentConsecutiveMatches;

    public static event EventHandler<OnScoreInitializedEventArgs> OnScoreInitialized;
    public static event EventHandler<OnScoreIncreasedEventArgs> OnScoreIncreased;

    public static event EventHandler<OnComboGainedEventArgs> OnComboGained; 
    public static event EventHandler<OnComboGainedEventArgs> OnComboUpdated;
    public static event EventHandler OnComboLost;

    public class OnScoreInitializedEventArgs : EventArgs
    {
        public int currentScore;
    }

    public class OnScoreIncreasedEventArgs : EventArgs
    {
        public int currentScore;
        public int increaseQuantity;
    }

    public class OnComboGainedEventArgs : EventArgs
    {
        public int comboGained; //Ex. comboGained = 2 -> Combo X2
    }

    private void OnEnable()
    {
        MemoryMinigameManager.OnPairMatch += MemoryMinigameManager_OnPairMatch;
        MemoryMinigameManager.OnPairFailed += MemoryMinigameManager_OnPairFailed;
    }

    private void OnDisable()
    {
        MemoryMinigameManager.OnPairMatch -= MemoryMinigameManager_OnPairMatch;
        MemoryMinigameManager.OnPairFailed -= MemoryMinigameManager_OnPairFailed;
    }

    private void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        currentScore = 0;
        currentConsecutiveMatches = 0;
        currentCombo = 0;

        OnScoreInitialized?.Invoke(this, new OnScoreInitializedEventArgs { currentScore = currentScore});
    }

    #region Pair Processing
    private void ProcessPairMatch()
    {
        currentConsecutiveMatches++;
     
        int newCombo = EvaluateCombo(currentConsecutiveMatches);

        if(newCombo > currentCombo)
        {
            if(!HasCombo()) OnComboGained?.Invoke(this, new OnComboGainedEventArgs { comboGained = newCombo });
            else OnComboUpdated?.Invoke(this, new OnComboGainedEventArgs { comboGained = newCombo });
        }

        currentCombo = newCombo;

        int scoreToAdd = EvaluateScoreToAdd(currentCombo);

        IncreaseScore(scoreToAdd);
    }
    private void ProcessPairFailed()
    {
        currentConsecutiveMatches = 0;

        if (HasCombo())
        {
            OnComboLost?.Invoke(this, EventArgs.Empty); 
        }

        currentCombo = 0;
    }
    #endregion

    #region Utility Methods
    private int EvaluateCombo(int consecutiveMatches)
    {
        if (consecutiveMatches <= 1) return 0; //No combo on 0 or 1 consecutive match
        if (consecutiveMatches >= settings.maxCombo) return settings.maxCombo;

        return consecutiveMatches;
    }

    private int EvaluateScoreToAdd(int combo)
    {
        if (combo <= 0) return settings.baseScorePerPairMatch;

        int scoreToAdd = settings.baseScorePerPairMatch + settings.bonusScorePerCombo*(combo-1); //Ex. If ComboX2 and baseScorePerPairMatch = 2, bonusScorePerCombo=2 , ScoreToAdd = 4
        return scoreToAdd;
    }


    private void IncreaseScore(int quantity)
    {
        currentScore += quantity;
        OnScoreIncreased?.Invoke(this, new OnScoreIncreasedEventArgs { currentScore = currentScore, increaseQuantity = quantity});
    }

    private bool HasCombo() => currentCombo > 0;
    #endregion

    #region Subscriptions
    private void MemoryMinigameManager_OnPairMatch(object sender, System.EventArgs e)
    {
        ProcessPairMatch();
    }

    private void MemoryMinigameManager_OnPairFailed(object sender, System.EventArgs e)
    {
        ProcessPairFailed();
    }
    #endregion


}
