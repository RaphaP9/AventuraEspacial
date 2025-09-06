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

    private bool isRunning = false;
    private bool hasBeenSet = false;

    public class OnTimeEventArgs : EventArgs
    {
        public float time;
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

        OnTimeSet?.Invoke(this, new OnTimeEventArgs { time = initialTime });
    }

    private void HandleTimePass()
    {
        if (!CanPassTime()) return;

        if(remainingTime > 0f)
        {
            remainingTime-= Time.deltaTime;
            isRunning = true;
        }

        if(remainingTime <= 0f)
        {
            OnTimeEnded?.Invoke(this, new OnTimeEventArgs { time = initialTime });

            remainingTime = 0f;

            isRunning = false;
            hasBeenSet = false;
        }
    }

    private bool CanPassTime()
    {
        if (!hasBeenSet) return false;

        if (ScenesManager.Instance != null)
        {
            if(!ScenesManager.Instance.IsSceneOnIdle()) return false;
        }

        return true;
    }
}
