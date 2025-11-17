using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
public class PasswordAccessUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;

    [Header("UI Components")]
    [SerializeField] private List<ButtonPasswordAccessedUIRelationship> buttonPasswordAccessedUIRelationships;
    [SerializeField] private Button closeButton;

    [Header("Debug")]
    [SerializeField] private bool debug;

    public static event EventHandler OnPasswordAccessUIOpen;
    public static event EventHandler OnPasswordAccessUIClose;
    public static event EventHandler OnPasswordAccessUIUnlock;

    public event EventHandler OnThisPasswordAccessUIOpen;
    public event EventHandler OnThisPasswordAccessUICloseCompletely;

    private const string SHOW_TRIGGER = "Show";
    private const string HIDE_TRIGGER = "Hide";

    private IPasswordAccessedUI currentPasswordAccessedUI;

    [System.Serializable]
    public class ButtonPasswordAccessedUIRelationship
    {
        public Button button;
        public Component passwordAccessedUIComponent;
    }

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        foreach (ButtonPasswordAccessedUIRelationship relationship in buttonPasswordAccessedUIRelationships)
        {
            GeneralUtilities.TryGetGenericFromComponent(relationship.passwordAccessedUIComponent, out IPasswordAccessedUI passwordAccessedUI);

            if (passwordAccessedUI == null)
            {
                if(debug) Debug.Log("Could not find an IPasswordAccessedUI interface in component");
                continue;
            }

            relationship.button.onClick.AddListener(() => OpenUI(passwordAccessedUI));
        }

        closeButton.onClick.AddListener(CloseUI);
    }

    public void OpenUI(IPasswordAccessedUI passwordAccessedUI)
    {
        ShowPasswordUI();
        OnPasswordAccessUIOpen?.Invoke(this, EventArgs.Empty);
        OnThisPasswordAccessUIOpen?.Invoke(this, EventArgs.Empty);

        currentPasswordAccessedUI = passwordAccessedUI;
    }

    public void UnlockUI()
    {
        if (currentPasswordAccessedUI == null) return;

        HidePasswordUI();
        OnPasswordAccessUIClose?.Invoke(this, EventArgs.Empty);
        OnPasswordAccessUIUnlock?.Invoke(this, EventArgs.Empty);

        currentPasswordAccessedUI.AccessUI();
        currentPasswordAccessedUI = null;
    }

    private void CloseUI()
    {
        HidePasswordUI();
        OnPasswordAccessUIClose?.Invoke(this, EventArgs.Empty);
    }

    #region Animation Methods
    public void ShowPasswordUI()
    {
        animator.ResetTrigger(HIDE_TRIGGER);
        animator.SetTrigger(SHOW_TRIGGER);
    }

    public void HidePasswordUI()
    {
        animator.ResetTrigger(SHOW_TRIGGER);
        animator.SetTrigger(HIDE_TRIGGER);
    }
    #endregion

    #region AnimationEvent Methods
    public void OnUIClosedCompletelyMethod()
    {
        OnThisPasswordAccessUICloseCompletely?.Invoke(this, EventArgs.Empty);
    }
    #endregion
}
