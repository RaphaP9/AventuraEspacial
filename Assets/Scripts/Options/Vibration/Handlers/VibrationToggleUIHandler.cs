using UnityEngine;

public class VibrationToggleUIHandler : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] protected AnimatedToggle animatedToggle;

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

    private void IntializeToggleListeners()
    {
        animatedToggle.onValueChanged.AddListener(ChangeState);
    }

    private void ChangeState(bool newState)
    {
        animatedToggle.isOn = newState;
        VibrationStateManager.Instance.ChangeState(newState, true);

        if (newState) animatedToggle.TurnOn();
        else animatedToggle.TurnOff();
    }

    private void ChangeStateImmediately(bool newState)
    {
        animatedToggle.isOn = newState;
        VibrationStateManager.Instance.ChangeState(newState, true);

        if (newState) animatedToggle.TurnOnInmediately();
        else animatedToggle.TurnOffInmediately();
    }

    #region Subscriptions
    private void VibrationStateManager_OnVibrationStateManagerInitialized(object sender, System.EventArgs e)
    {
        ChangeStateImmediately(VibrationStateManager.Instance.VibrationEnabled);
    }

    private void VibrationStateManager_OnVibrationStateChanged(object sender, VibrationStateManager.OnVibrationChangedEventArgs e)
    {
        //
    }
    #endregion
}
