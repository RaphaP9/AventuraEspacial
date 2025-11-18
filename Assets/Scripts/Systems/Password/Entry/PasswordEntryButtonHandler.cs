using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PasswordEntryButtonHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PasswordEntryUIHandler passwordEntryUIHandler;
    [SerializeField] private Button passwordButton;
    [SerializeField] private Animator animator;
    [SerializeField] private Image image;

    [Header("Runtime Filled")]
    [SerializeField] private PasswordItemSO passwordItemSO;
    [SerializeField] private bool isPressed;

    public PasswordItemSO PasswordItemSO => passwordItemSO;

    public event EventHandler<OnPasswordButtonClickedEventArgs> OnPasswordButtonClicked;

    private const string PRESS_TRIGGER = "Press";
    private const string UNPRESS_TRIGGER = "Unpress";

    private const string PRESSED_ANIMATION_NAME = "Pressed";
    private const string UNPRESSED_ANIMATION_NAME = "Unpressed";

    public class OnPasswordButtonClickedEventArgs : EventArgs
    {
        public PasswordItemSO passwordItem;
    }

    private void OnEnable()
    {
        passwordEntryUIHandler.OnPasswordItemTyped += PasswordEntryUIHandler_OnPasswordItemTyped;
        passwordEntryUIHandler.OnPasswordItemDeleted += PasswordEntryUIHandler_OnPasswordItemDeleted;

        passwordEntryUIHandler.OnCompletePasswordTyped += PasswordEntryUIHandler_OnCompletePasswordTyped;
    }

    private void OnDisable()
    {
        passwordEntryUIHandler.OnPasswordItemTyped -= PasswordEntryUIHandler_OnPasswordItemTyped;
        passwordEntryUIHandler.OnPasswordItemDeleted -= PasswordEntryUIHandler_OnPasswordItemDeleted;

        passwordEntryUIHandler.OnCompletePasswordTyped -= PasswordEntryUIHandler_OnCompletePasswordTyped;
    }

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void Start()
    {
        UnpressImmediately();
    }

    private void InitializeButtonsListeners()
    {
        passwordButton.onClick.AddListener(OnButtonClickMethod);
    }

    private void OnButtonClickMethod()
    {
        if (passwordItemSO == null) return;
        OnPasswordButtonClicked?.Invoke(this, new OnPasswordButtonClickedEventArgs { passwordItem = passwordItemSO });
    }

    #region Animations
    public void PressImmediately()
    {
        animator.Play(PRESSED_ANIMATION_NAME);
        DisableButton();

        isPressed = true;
    }

    public void UnpressImmediately()
    {
        animator.Play(UNPRESSED_ANIMATION_NAME);
        EnableButton();

        isPressed = false;
    }

    public void Press()
    {
        animator.SetTrigger(PRESS_TRIGGER);
        DisableButton();

        isPressed = true;
    }

    public void Unpress()
    {
        animator.SetTrigger(UNPRESS_TRIGGER);
        EnableButton();

        isPressed = false;
    }
    #endregion

    #region Public Methods
    public void SetButton(PasswordItemSO passwordItemSO)
    {
        this.passwordItemSO = passwordItemSO;
        image.sprite = passwordItemSO.sprite;
    }
    #endregion

    #region Utility Methods
    private void EnableButton() => passwordButton.interactable = true;
    private void DisableButton() => passwordButton.interactable = false;
    #endregion

    #region Subscriptions
    private void PasswordEntryUIHandler_OnPasswordItemTyped(object sender, PasswordEntryUIHandler.OnPasswordItemEventArgs e)
    {
        if (e.passwordItemSO == passwordItemSO)
        {
            if (e.immediately) PressImmediately();
            else Press();
        }
    }

    private void PasswordEntryUIHandler_OnPasswordItemDeleted(object sender, PasswordEntryUIHandler.OnPasswordItemEventArgs e)
    {
        if (e.passwordItemSO == passwordItemSO)
        {
            if (e.immediately) UnpressImmediately();
            else Unpress();
        }
        else if (!isPressed)
        {
            EnableButton();
        }
    }

    private void PasswordEntryUIHandler_OnCompletePasswordTyped(object sender, EventArgs e)
    {
        DisableButton();
    }

    #endregion
}
