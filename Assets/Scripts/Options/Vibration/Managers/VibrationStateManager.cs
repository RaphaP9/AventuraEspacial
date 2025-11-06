using System;
using UnityEngine;
using UnityEngine.Audio;

public class VibrationStateManager : MonoBehaviour
{
    public static VibrationStateManager Instance { get; private set; }

    [Header("Vibration Settings")]
    [SerializeField] protected bool initialState;

    [Header("Load Settings")]
    [SerializeField] private string playerPrefsKey;

    [Header("Runtime Filled")]
    [SerializeField] private bool vibrationEnabled;

    public bool VibrationEnabled => vibrationEnabled;

    public static event EventHandler OnVibrationStateManagerInitialized;
    public static event EventHandler<OnVibrationChangedEventArgs> OnVibrationStateChanged;

    public bool VibrationManagerInitialized { get; private set; } = false;

    public class OnVibrationChangedEventArgs : EventArgs
    {
        public bool newState;
    }

    private void Awake()
    {
        SetSingleton();
    }

    private void Start()
    {
        LoadVibrationPlayerPrefs();
        InitializeVibration();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //Debug.LogWarning("There is more than one VibrationStateManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    protected virtual void InitializeVibration()
    {
        ChangeState(initialState, false);
        OnVibrationStateManagerInitialized?.Invoke(this, EventArgs.Empty);

        VibrationManagerInitialized = true;
    }

    #region PlayerPrefs
    protected void LoadVibrationPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey(playerPrefsKey))
        {
            int startingValueInt = FormattingUtilities.TranstaleBoolToInt(initialState);
            PlayerPrefs.SetInt(playerPrefsKey, startingValueInt);
        }

        int loadedInitialStateInt = PlayerPrefs.GetInt(playerPrefsKey);
        initialState = FormattingUtilities.TranslateIntToBool(loadedInitialStateInt);
    }

    public void SaveVibrationPlayerPrefs(bool state)
    {
        int stateInt = FormattingUtilities.TranstaleBoolToInt(state);
        PlayerPrefs.SetInt(playerPrefsKey, stateInt);
    }
    #endregion

    public void ChangeState(bool state, bool saveToPlayerPrefs)
    {
        vibrationEnabled = state;

        OnVibrationStateChanged?.Invoke(this, new OnVibrationChangedEventArgs { newState = state });
        if (saveToPlayerPrefs) SaveVibrationPlayerPrefs(state);
    }
}
