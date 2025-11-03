using UnityEngine;
using System;

public abstract class MinigameManager : MonoBehaviour
{
    public static event EventHandler OnGameWinning;
    public static event EventHandler OnGameWon;

    public static event EventHandler OnGameLosing;
    public static event EventHandler OnGameLost;

    public static event EventHandler OnGameInitialized;

    public static event EventHandler<OnRoundEventArgs> OnRoundStart;
    public static event EventHandler<OnRoundEventArgs> OnRoundEnd;

    public class OnRoundEventArgs : EventArgs
    {
        public int roundIndex;
        public int totalRounds;
    }

    protected void OnGameInitializedMethod() => OnGameInitialized?.Invoke(this, EventArgs.Empty);   
    protected void OnGameWinningMethod() => OnGameWinning?.Invoke(this, EventArgs.Empty);
    protected void OnGameWonMethod() => OnGameWon?.Invoke(this, EventArgs.Empty);
    protected void OnGameLosingMethod() => OnGameLosing?.Invoke(this, EventArgs.Empty);
    protected void OnGameLostMethod() => OnGameLost?.Invoke(this, EventArgs.Empty);

    protected void OnRoundStartMethod(int roundIndex, int totalRounds) => OnRoundStart?.Invoke(this, new OnRoundEventArgs { roundIndex = roundIndex, totalRounds = totalRounds });
    protected void OnRoundEndMethod(int roundIndex, int totalRounds) => OnRoundEnd?.Invoke(this, new OnRoundEventArgs { roundIndex = roundIndex, totalRounds = totalRounds });

    public abstract bool CanPassTime();
}
