using UnityEngine;
using System;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance { get; private set; }

    [Header("Runtime Filled")]
    [SerializeField] private float initialTime;
    [SerializeField] private float remainingTime;

    public static event EventHandler<OnTimeEventArgs> OnTimeSet;
    public static event EventHandler<OnTimeEventArgs> OnTimeEnded;
    public static event EventHandler<OnTimeChangedEventArgs> OnTimeChanged;

    private bool isRunning = false;
    private bool hasBeenSet = false;

    private int previousTimeInt = 0;

    public float RemainingTime => remainingTime;
    public int RemainingTimeInt => Mathf.CeilToInt(remainingTime);   

    public class OnTimeEventArgs : EventArgs
    {
        public float time;
        public int timeInt;
    }

    public class OnTimeChangedEventArgs : EventArgs
    {
        public int timeInt;
    }

    private void Awake()
    {
        SetSingleton();
    }

    public void Start()
    {
        InitializeVariables();
    }

    private void Update()
    {
        HandleTimePass();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeVariables()
    {
        isRunning = false;
        hasBeenSet = false;
    }

    public void SetTime(float seconds)
    {
        initialTime = Mathf.Max(0f, seconds);
        remainingTime = initialTime;

        hasBeenSet = true;
        isRunning = false;

        OnTimeSet?.Invoke(this, new OnTimeEventArgs { time = remainingTime, timeInt = Mathf.CeilToInt(remainingTime)});
    }

    private void HandleTimePass()
    {
        if (!CanPassTime()) return;

        remainingTime -= Time.deltaTime;
        isRunning = true;

        if(remainingTime <= 0f)
        {
            OnTimeEnded?.Invoke(this, new OnTimeEventArgs { time = initialTime });

            remainingTime = 0f;

            isRunning = false;
            hasBeenSet = false;
        }

        HandleTimeChanged();
    }

    private void HandleTimeChanged()
    {
        if (!hasBeenSet) return;
        if (remainingTime < 0f) return;

        int remainingTimeInt = Mathf.CeilToInt(remainingTime);

        if (remainingTimeInt != previousTimeInt)
        {
            previousTimeInt = remainingTimeInt;
            OnTimeChanged?.Invoke(this, new OnTimeChangedEventArgs { timeInt = previousTimeInt });
        }
    }

    private bool CanPassTime()
    {
        if (!hasBeenSet) return false;
        if (remainingTime < 0f) return false;

        if(TimePassingHandler.Instance != null)
        {
            if(!TimePassingHandler.Instance.CanPassTime()) return false; //Each scene may have its own TimePassingHandler
        }

        return true;
    }
}
