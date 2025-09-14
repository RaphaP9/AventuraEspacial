using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class OptionsUI : UILayer
{
    [Header("Components")]
    [SerializeField] private Animator optionsUIAnimator;

    [Header("UI Components")]
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button switchButton;
    [SerializeField] private Button closeButton;

    [Header("Runtime Filled")]
    [SerializeField] private OptionsState optionsState;

    private enum OptionsState { Closed, OpenChild, OpenParents}

    public static event EventHandler OnOptionsUIOpen;
    public static event EventHandler OnOptionsUIClose;
    public static event EventHandler OnOptionsUISwitch;

    private const string SHOW_TRIGGER = "Show";
    private const string HIDE_FROM_CHILD_TRIGGER = "HideFromChild";
    private const string HIDE_FROM_PARENTS_TRIGGER = "HideFromParents";
    private const string SWITCH_CHILD_TO_PARENTS_TRIGGER = "SwitchChildToParents";
    private const string SWITCH_PARENTS_TO_CHILD_TRIGGER = "SwitchParentsToChild";

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void Start()
    {
        SetUIState(State.Closed);
        SetOptionsState(OptionsState.Closed);   
    }

    private void InitializeButtonsListeners()
    {
        optionsButton.onClick.AddListener(OpenUI);
        switchButton.onClick.AddListener(SwitchUI);
        closeButton.onClick.AddListener(CloseUI);
    }

    private void SetOptionsState(OptionsState state) => this.optionsState = state;

    public void OpenUI()
    {
        if (state != State.Closed) return;
        if (optionsState != OptionsState.Closed) return;

        SetUIState(State.Open);
        AddToUILayersList();

        ShopOptionsUI();
        OnOptionsUIOpen?.Invoke(this, EventArgs.Empty);
    }

    private void CloseUI()
    {
        if (state != State.Open) return;
        if (optionsState == OptionsState.Closed) return;

        SetUIState(State.Closed);
        RemoveFromUILayersList();
        OnOptionsUIClose?.Invoke(this, EventArgs.Empty);

        switch (optionsState)
        {
            case OptionsState.OpenChild:
                HideFromChildUI();
                break;
            case OptionsState.OpenParents:
                HideFromParentsUI();
                break;
        }
    }

    private void SwitchUI()
    {
        if (state != State.Open) return;
        if (optionsState == OptionsState.Closed) return;

        OnOptionsUISwitch?.Invoke(this, EventArgs.Empty);

        switch (optionsState)
        {
            case OptionsState.OpenChild:
                SwitchChildToParents();
                break;
            case OptionsState.OpenParents:
                SwitchParentsToChild();
                break;
        }
    }

    protected override void CloseFromUI() => CloseUI();


    #region Animation Methods
    public void ShopOptionsUI()
    {
        optionsUIAnimator.ResetTrigger(HIDE_FROM_CHILD_TRIGGER);
        optionsUIAnimator.ResetTrigger(HIDE_FROM_PARENTS_TRIGGER);
        optionsUIAnimator.ResetTrigger(SWITCH_CHILD_TO_PARENTS_TRIGGER);
        optionsUIAnimator.ResetTrigger(SWITCH_PARENTS_TO_CHILD_TRIGGER);

        optionsUIAnimator.SetTrigger(SHOW_TRIGGER);

        SetOptionsState(OptionsState.OpenChild);
    }

    public void HideFromChildUI()
    {
        optionsUIAnimator.ResetTrigger(SHOW_TRIGGER);
        optionsUIAnimator.ResetTrigger(HIDE_FROM_PARENTS_TRIGGER);
        optionsUIAnimator.ResetTrigger(SWITCH_CHILD_TO_PARENTS_TRIGGER);
        optionsUIAnimator.ResetTrigger(SWITCH_PARENTS_TO_CHILD_TRIGGER);

        optionsUIAnimator.SetTrigger(HIDE_FROM_CHILD_TRIGGER);

        SetOptionsState(OptionsState.Closed);
    }

    public void HideFromParentsUI()
    {
        optionsUIAnimator.ResetTrigger(SHOW_TRIGGER);
        optionsUIAnimator.ResetTrigger(HIDE_FROM_CHILD_TRIGGER);
        optionsUIAnimator.ResetTrigger(SWITCH_CHILD_TO_PARENTS_TRIGGER);
        optionsUIAnimator.ResetTrigger(SWITCH_PARENTS_TO_CHILD_TRIGGER);

        optionsUIAnimator.SetTrigger(HIDE_FROM_PARENTS_TRIGGER);

        SetOptionsState(OptionsState.Closed);
    }

    public void SwitchChildToParents()
    {
        optionsUIAnimator.ResetTrigger(SHOW_TRIGGER);
        optionsUIAnimator.ResetTrigger(HIDE_FROM_CHILD_TRIGGER);
        optionsUIAnimator.ResetTrigger(HIDE_FROM_PARENTS_TRIGGER);
        optionsUIAnimator.ResetTrigger(SWITCH_PARENTS_TO_CHILD_TRIGGER);

        optionsUIAnimator.SetTrigger(SWITCH_CHILD_TO_PARENTS_TRIGGER);

        SetOptionsState(OptionsState.OpenParents);
    }

    public void SwitchParentsToChild()
    {
        optionsUIAnimator.ResetTrigger(SHOW_TRIGGER);
        optionsUIAnimator.ResetTrigger(HIDE_FROM_CHILD_TRIGGER);
        optionsUIAnimator.ResetTrigger(HIDE_FROM_PARENTS_TRIGGER);
        optionsUIAnimator.ResetTrigger(SWITCH_CHILD_TO_PARENTS_TRIGGER);

        optionsUIAnimator.SetTrigger(SWITCH_PARENTS_TO_CHILD_TRIGGER);

        SetOptionsState(OptionsState.OpenChild);
    }
    #endregion
}
