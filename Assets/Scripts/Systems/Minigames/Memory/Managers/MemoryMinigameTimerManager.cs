using UnityEngine;
using System;

public class MemoryMinigameTimerManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private MemoryMinigameSettings settings;

    [Header("Runtime Filled")]
    [SerializeField] private float totalTime;
    [SerializeField] private float currentTime;

    private bool totalTimeSet = false;
    private bool timeEnd = false;

    public static event EventHandler<OnTimeSetEventArgs> OnTimeSet;
    public static event EventHandler<OnTimeIncreasedEventArgs> OnTimeIncrease;
    public static event EventHandler<OnTimeDecreasedEventArgs> OnTimeDecrease;
    public static event EventHandler OnTimeEnd;

    public float TotalTime => totalTime;
    public float CurrentTime => currentTime;

    public class OnTimeSetEventArgs: EventArgs
    {
        public float time;
    }

    public class OnTimeIncreasedEventArgs : EventArgs
    {
        public float newTime;
        public float increaseQuantity;
    }

    public class OnTimeDecreasedEventArgs : EventArgs
    {
        public float newTime;
        public float decreaseQuantoty;
    }

    private void OnEnable()
    {
        MemoryMinigameManager.OnGameInitialized += MemoryMinigameManager_OnGameInitialized;

        MemoryMinigameManager.OnPairMatch += MemoryMinigameManager_OnPairMatch;
        MemoryMinigameManager.OnPairFailed += MemoryMinigameManager_OnPairFailed;
    }

    private void OnDisable()
    {
        MemoryMinigameManager.OnGameInitialized -= MemoryMinigameManager_OnGameInitialized;

        MemoryMinigameManager.OnPairMatch -= MemoryMinigameManager_OnPairMatch;
        MemoryMinigameManager.OnPairFailed -= MemoryMinigameManager_OnPairFailed;
    }

    private void Update()
    {
        HandleTime();
    }

    private void HandleTime()
    {
        if (timeEnd) return;
        if (!totalTimeSet) return;
        if (!MemoryMinigameManager.Instance.CanPassTime()) return;

        currentTime-= Time.deltaTime;
        CheckTimeEnd();
    }

    private void IncreaseTime(float time)
    {
        currentTime = currentTime + time > settings.gameTime? settings.gameTime: currentTime + time;
        OnTimeIncrease?.Invoke(this, new OnTimeIncreasedEventArgs { newTime = currentTime, increaseQuantity = time });

        CheckTimeEnd();
    }

    private void DecreaseTime(float time)
    {
        currentTime = currentTime - time < 0f ? 0f : currentTime - time;
        OnTimeDecrease?.Invoke(this, new OnTimeDecreasedEventArgs { newTime = currentTime, decreaseQuantoty = time });

        CheckTimeEnd();
    }
    private void CheckTimeEnd()
    {
        if (currentTime > 0) return;

        timeEnd = true;
        OnTimeEnd?.Invoke(this, EventArgs.Empty);
    }

    private void SetTotalTime(float time)
    {
        totalTime = time;
        currentTime = totalTime;

        totalTimeSet = true;

        OnTimeSet?.Invoke(this, new OnTimeSetEventArgs { time = time });    
    }

    #region Subscriptions
    private void MemoryMinigameManager_OnGameInitialized(object sender, System.EventArgs e)
    {
        SetTotalTime(settings.gameTime);
    }

    private void MemoryMinigameManager_OnPairMatch(object sender, EventArgs e)
    {
        IncreaseTime(settings.timeBonusOnMatch);
    }

    private void MemoryMinigameManager_OnPairFailed(object sender, EventArgs e)
    {
        DecreaseTime(settings.timePenaltyOnFail);   
    }
    #endregion
}
