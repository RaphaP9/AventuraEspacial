using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PasswordConfigurationButtonHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PasswordConfigurationUIHandler passwordConfigurationUIHandler;
    [SerializeField] private Button passwordButton;
    [SerializeField] private Animator animator;
    [SerializeField] private Image image;

    [Header("Runtime Filled")]
    [SerializeField] private PasswordItemSO passwordItemSO;

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
        passwordConfigurationUIHandler.OnPasswordItemTyped += PasswordConfigurationUIHandler_OnPasswordItemTyped;
        passwordConfigurationUIHandler.OnPasswordItemDeleted += PasswordConfigurationUIHandler_OnPasswordItemDeleted;
    }

    private void OnDisable()
    {
        passwordConfigurationUIHandler.OnPasswordItemTyped -= PasswordConfigurationUIHandler_OnPasswordItemTyped;
        passwordConfigurationUIHandler.OnPasswordItemDeleted -= PasswordConfigurationUIHandler_OnPasswordItemDeleted;
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
        passwordButton.interactable = false;
    }

    public void UnpressImmediately()
    {
        animator.Play(UNPRESSED_ANIMATION_NAME);
        passwordButton.interactable = true;
    }

    public void Press()
    {
        animator.SetTrigger(PRESS_TRIGGER);
        passwordButton.interactable = false;
    }

    public void Unpress()
    {
        animator.SetTrigger(UNPRESS_TRIGGER);
        passwordButton.interactable = true;
    }
    #endregion

    #region Public Methods
    public void SetButton(PasswordItemSO passwordItemSO)
    {
        this.passwordItemSO = passwordItemSO;
        image.sprite = passwordItemSO.sprite;
    }
    #endregion

    #region Subscriptions
    private void PasswordConfigurationUIHandler_OnPasswordItemTyped(object sender, PasswordConfigurationUIHandler.OnPasswordItemEventArgs e)
    {
        if (e.passwordItemSO != passwordItemSO) return;

        if (e.immediately) PressImmediately();
        else Press();
    }

    private void PasswordConfigurationUIHandler_OnPasswordItemDeleted(object sender, PasswordConfigurationUIHandler.OnPasswordItemEventArgs e)
    {
        if (e.passwordItemSO != passwordItemSO) return;

        if (e.immediately) UnpressImmediately();
        else Unpress();
    }
    #endregion
}
