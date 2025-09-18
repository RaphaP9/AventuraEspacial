using System;
using UnityEngine;

public abstract class MinigameScoreManager : MonoBehaviour
{
    [Header("Runtime Filled")]
    [SerializeField] protected int currentScore;
    [SerializeField] protected int currentCombo;
    [SerializeField] protected int currentConsecutiveHits;

    public int CurrentScore => currentScore;

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

    private void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        currentConsecutiveHits = 0;
        currentScore = 0;
        currentCombo = 0;

        OnScoreInitialized?.Invoke(this, new OnScoreInitializedEventArgs { currentScore = currentScore });
    }

    #region Processing
    protected void ProcessHit()
    {
        currentConsecutiveHits++;

        int newCombo = EvaluateCombo(currentConsecutiveHits);

        if (newCombo > currentCombo)
        {
            if (!HasCombo()) OnComboGained?.Invoke(this, new OnComboGainedEventArgs { comboGained = newCombo });    
            else OnComboUpdated?.Invoke(this, new OnComboGainedEventArgs { comboGained = newCombo});
        }

        currentCombo = newCombo;

        int scoreToAdd = EvaluateScoreToAdd(currentCombo);

        IncreaseScore(scoreToAdd);
    }

    protected void ProcessFail()
    {
        currentConsecutiveHits = 0;

        if (HasCombo())
        {
            OnComboLost?.Invoke(this, EventArgs.Empty); 
        }

        currentCombo = 0;
    }
    #endregion

    #region Utility Methods
    protected int EvaluateCombo(int consecutiveMatches)
    {
        if (consecutiveMatches <= 1) return 0; //No combo on 0 or 1 consecutive match
        if (consecutiveMatches >= GetMaxCombo()) return GetMaxCombo();

        return consecutiveMatches;
    }

    protected int EvaluateScoreToAdd(int combo)
    {
        if (combo <= 0) return GetBaseScorePerHit();

        int scoreToAdd = GetBaseScorePerHit() + GetBonusScorePerCombo() * (combo - 1); //Ex. If ComboX2 and baseScorePerPairMatch = 2, bonusScorePerCombo=2 , ScoreToAdd = 4
        return scoreToAdd;
    }

    protected void IncreaseScore(int quantity)
    {
        currentScore += quantity;
        OnScoreIncreased?.Invoke(this, new OnScoreIncreasedEventArgs { currentScore = currentScore, increaseQuantity = quantity });
    }

    protected bool HasCombo() => currentCombo > 0;
    #endregion

    protected abstract int GetBaseScorePerHit();
    protected abstract int GetBonusScorePerCombo();
    protected abstract int GetMaxCombo();
}
