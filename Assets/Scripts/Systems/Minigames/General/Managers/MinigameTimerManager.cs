using System;
using UnityEngine;

public abstract class MinigameTimerManager : MonoBehaviour
{
    [Header("Runtime Filled")]
    [SerializeField] protected float totalTime;
    [SerializeField] protected float currentTime;

    protected bool totalTimeSet = false;
    protected bool timeEnd = false;

    public static event EventHandler<OnTimeSetEventArgs> OnTimeSet;
    public static event EventHandler<OnTimeIncreasedEventArgs> OnTimeIncrease;
    public static event EventHandler<OnTimeDecreasedEventArgs> OnTimeDecrease;
    public static event EventHandler OnTimeEnd;

    public float TotalTime => totalTime;
    public float CurrentTime => currentTime;

    public class OnTimeSetEventArgs : EventArgs
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
        public float decreaseQuantity;
    }

    protected void Update()
    {
        HandleTime();
    }

    protected void HandleTime()
    {
        if (timeEnd) return;
        if (!totalTimeSet) return;
        if (!CanPassTime()) return;

        currentTime -= Time.deltaTime;
        CheckTimeEnd();
    }

    protected void IncreaseTime(float time)
    {
        currentTime = currentTime + time > GetGameTime() ? GetGameTime() : currentTime + time;
        OnTimeIncrease?.Invoke(this, new OnTimeIncreasedEventArgs { newTime = currentTime, increaseQuantity = time });

        CheckTimeEnd();
    }

    protected void DecreaseTime(float time)
    {
        currentTime = currentTime - time < 0f ? 0f : currentTime - time;
        OnTimeDecrease?.Invoke(this, new OnTimeDecreasedEventArgs { newTime = currentTime, decreaseQuantity = time });

        CheckTimeEnd();
    }
    protected void CheckTimeEnd()
    {
        if (currentTime > 0) return;

        timeEnd = true;
        OnTimeEnd?.Invoke(this, EventArgs.Empty);
    }

    protected void SetTotalTime(float time)
    {
        totalTime = time;
        currentTime = totalTime;

        totalTimeSet = true;

        OnTimeSet?.Invoke(this, new OnTimeSetEventArgs { time = time });
    }

    protected abstract float GetGameTime();
    protected abstract bool CanPassTime();
}
