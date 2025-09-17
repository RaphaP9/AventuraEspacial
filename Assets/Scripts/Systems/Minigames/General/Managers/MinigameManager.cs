using UnityEngine;
using System;

public class MinigameManager : MonoBehaviour
{
    public static event EventHandler OnGameWinning;
    public static event EventHandler OnGameWon;

    public static event EventHandler OnGameLosing;
    public static event EventHandler OnGameLost;

    protected void OnGameWinningMethod() => OnGameWinning?.Invoke(this, EventArgs.Empty);
    protected void OnGameWonMethod() => OnGameWon?.Invoke(this, EventArgs.Empty);
    protected void OnGameLosingMethod() => OnGameLosing?.Invoke(this, EventArgs.Empty);
    protected void OnGameLostMethod() => OnGameLost?.Invoke(this, EventArgs.Empty);
}
