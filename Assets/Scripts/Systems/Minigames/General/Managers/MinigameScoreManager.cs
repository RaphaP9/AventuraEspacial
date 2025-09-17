using System;
using UnityEngine;

public class MinigameScoreManager : MonoBehaviour
{
    [Header("Runtime Filled")]
    [SerializeField] protected int currentScore;

    public int CurrentScore => currentScore;

    public class OnScoreInitializedEventArgs : EventArgs
    {
        public int currentScore;
    }

    public class OnScoreIncreasedEventArgs : EventArgs
    {
        public int currentScore;
        public int increaseQuantity;
    }
}
