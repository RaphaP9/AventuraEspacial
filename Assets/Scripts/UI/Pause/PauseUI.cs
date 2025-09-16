using UnityEngine;
using System;
using UnityEngine.UI;
public class PauseUI : UILayer
{
    [Header("Components")]
    [SerializeField] private Animator pauseUIAnimator;

    [Header("UI Components")]
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;

    [Header("Runtime Filled")]
    [SerializeField] private PauseState pauseState;

    private enum PauseState { Closed, Open }

    public static event EventHandler OnPauseUIOpen;
    public static event EventHandler OnPauseUIClose;

    private const string SHOW_TRIGGER = "Show";
    private const string HIDE_TRIGGER = "Hide";

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void Start()
    {
        SetUIState(State.Closed);
        SetPauseState(PauseState.Closed);
    }

    private void InitializeButtonsListeners()
    {
        pauseButton.onClick.AddListener(OpenUI);
        resumeButton.onClick.AddListener(CloseUI);
    }

    private void SetPauseState(PauseState state) => pauseState = state;

    public void OpenUI()
    {
        if (state != State.Closed) return;
        if (pauseState != PauseState.Closed) return;

        SetUIState(State.Open);
        AddToUILayersList();

        ShowPauseUI();
        OnPauseUIOpen?.Invoke(this, EventArgs.Empty);
    }

    private void CloseUI()
    {
        if (state != State.Open) return;
        if (pauseState == PauseState.Closed) return;

        SetUIState(State.Closed);

        RemoveFromUILayersList();
        HidePauseUI();

        OnPauseUIClose?.Invoke(this, EventArgs.Empty);
    }

    protected override void CloseFromUI() => CloseUI();


    public void ShowPauseUI()
    {
        pauseUIAnimator.ResetTrigger(HIDE_TRIGGER);
        pauseUIAnimator.SetTrigger(SHOW_TRIGGER);

        SetPauseState(PauseState.Open);
    }

    public void HidePauseUI()
    {
        pauseUIAnimator.ResetTrigger(SHOW_TRIGGER);
        pauseUIAnimator.SetTrigger(HIDE_TRIGGER);

        SetPauseState(PauseState.Closed);
    }
}
