using UnityEngine;
using System;

public class VibrationToggleUIHandler : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] protected AnimatedToggle animatedToggle;

    public static event EventHandler<OnVibrationToggledEventArgs> OnVibrationToggled;

    private bool canPerformChangeStateMethod = false;

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
        ChangeStateImmediately(VibrationStateManager.Instance.VibrationEnabled, false);
    }

    private void IntializeToggleListeners()
    {
        animatedToggle.onValueChanged.AddListener((value) => ChangeState(value, true));
    }

    private void ChangeState(bool newState, bool changeManagerState)
    {
        //When manager is initialized, ChangeStateImmediately method is executed, and the onValue event is triggered, triggering this event.
        //The next assertion prevents the method from being executed past the assertion, using VibrationStateManager.Instance.VibrationManagerInitialized property

        if (!VibrationStateManager.Instance.VibrationManagerInitialized) return;

        animatedToggle.isOn = newState;

        if (changeManagerState)
        {
            VibrationStateManager.Instance.ChangeState(newState, true);
            OnVibrationToggled?.Invoke(this, new OnVibrationToggledEventArgs { isOn = newState });
        }
        
        if (newState) animatedToggle.TurnOn();
        else animatedToggle.TurnOff();
    }

    private void ChangeStateImmediately(bool newState, bool changeManagerState)
    {
        animatedToggle.isOn = newState;

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
        ChangeStateImmediately(VibrationStateManager.Instance.VibrationEnabled, false);
    }

    private void VibrationStateManager_OnVibrationStateChanged(object sender, VibrationStateManager.OnVibrationChangedEventArgs e)
    {
        //
    }
    #endregion
}
