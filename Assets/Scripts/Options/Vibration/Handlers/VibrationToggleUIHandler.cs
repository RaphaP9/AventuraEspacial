using UnityEngine;
using System;

public class VibrationToggleUIHandler : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] protected AnimatedToggle animatedToggle;

    public static event EventHandler<OnVibrationToggledEventArgs> OnVibrationToggled;

    public class OnVibrationToggledEventArgs : EventArgs
    {
        public bool isOn;
    }

    private void OnEnable()
    {
        VibrationStateManager.OnVibrationStateManagerInitialized += VibrationStateManager_OnVibrationStateManagerInitialized;
        VibrationStateManager.OnVibrationStateChanged += VibrationStateManager_OnVibrationStateChanged;
    }

    private void OnDisable()
    {
        VibrationStateManager.OnVibrationStateManagerInitialized -= VibrationStateManager_OnVibrationStateManagerInitialized;
        VibrationStateManager.OnVibrationStateChanged -= VibrationStateManager_OnVibrationStateChanged;
    }

    private void Awake()
    {
        IntializeToggleListeners();
    }

    private void Start()
    {
        InitializationLogic();
    }

    private void IntializeToggleListeners()
    {
        animatedToggle.onValueChanged.AddListener((value) => ChangeState(value, true, false));
    }

    private void InitializationLogic()
    {
        if (!VibrationStateManager.Instance.VibrationManagerInitialized) return; //Only trigger this method if the VibrationStateManager was already initialized

        ChangeStateImmediately(VibrationStateManager.Instance.VibrationEnabled, false, false);
    }

    private void ChangeState(bool newState, bool changeManagerState, bool notify)
    {
        if (notify) animatedToggle.isOn = newState;
        else animatedToggle.SetIsOnWithoutNotify(newState);

        if (changeManagerState)
        {
            VibrationStateManager.Instance.ChangeState(newState, true);
            OnVibrationToggled?.Invoke(this, new OnVibrationToggledEventArgs { isOn = newState });
        }
        
        if (newState) animatedToggle.TurnOn();
        else animatedToggle.TurnOff();
    }

    private void ChangeStateImmediately(bool newState, bool changeManagerState, bool notify)
    {
        if (notify) animatedToggle.isOn = newState;
        else animatedToggle.SetIsOnWithoutNotify(newState);

        if (changeManagerState)
        {
            VibrationStateManager.Instance.ChangeState(newState, true);
            OnVibrationToggled?.Invoke(this, new OnVibrationToggledEventArgs { isOn = newState });
        }

        if (newState) animatedToggle.TurnOnInmediately();
        else animatedToggle.TurnOffInmediately();
    }

    #region Subscriptions
    private void VibrationStateManager_OnVibrationStateManagerInitialized(object sender, System.EventArgs e)
    {
        ChangeStateImmediately(VibrationStateManager.Instance.VibrationEnabled, false, false);
    }

    private void VibrationStateManager_OnVibrationStateChanged(object sender, VibrationStateManager.OnVibrationChangedEventArgs e)
    {
        //
    }
    #endregion
}
