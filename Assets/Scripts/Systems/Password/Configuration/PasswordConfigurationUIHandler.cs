using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class PasswordConfigurationUIHandler : MonoBehaviour
{
    [Header("Lists")]
    [SerializeField] private List<PasswordConfigurationButtonHandler> passwordButtonHandlers;
    [Space]
    [SerializeField] private List<PasswordItemSO> passwordItemsPool;

    [Header("Components")]
    [SerializeField] private Button deletePasswordButton;
    [SerializeField] private Button confirmButton;

    [Header("Settings")]
    [SerializeField] private bool randomizeButtons;

    [Header("Runtime Filled")]
    [SerializeField] private List<PasswordItemSO> typedPassword;

    public event EventHandler<OnHandlerInitializedEventArgs> OnPasswordHandlerInitialized;

    public event EventHandler<OnPasswordItemEventArgs> OnPasswordItemTyped;
    public event EventHandler<OnPasswordItemEventArgs> OnPasswordItemDeleted;

    public event EventHandler OnCompletePasswordTyped;

    public List<PasswordItemSO> TypedPassword => typedPassword;

    public class OnPasswordItemEventArgs : EventArgs
    {
        public int index;
        public PasswordItemSO passwordItemSO;
        public bool immediately;
    }

    public class OnHandlerInitializedEventArgs : EventArgs
    {
        public int passwordItemsCount;
    }

    private void OnEnable()
    {
        foreach(PasswordConfigurationButtonHandler passwordConfigurationButtonHandler in passwordButtonHandlers)
        {
            passwordConfigurationButtonHandler.OnPasswordButtonClicked += PasswordConfigurationButtonHandler_OnPasswordButtonClicked;
        }
    }

    private void OnDisable()
    {
        foreach (PasswordConfigurationButtonHandler passwordConfigurationButtonHandler in passwordButtonHandlers)
        {
            passwordConfigurationButtonHandler.OnPasswordButtonClicked -= PasswordConfigurationButtonHandler_OnPasswordButtonClicked;
        }
    }

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void Start()
    {
        ClearTypedPassword(true);
        SetButtons();
        InitializePasswordHandler();
    }

    private void InitializeButtonsListeners()
    {
        deletePasswordButton.onClick.AddListener(() => DeleteLastTypedItem(false));
    }

    private void InitializePasswordHandler()
    {
        OnPasswordHandlerInitialized?.Invoke(this, new OnHandlerInitializedEventArgs { passwordItemsCount = PasswordUtilities.GetPasswordItemCount()});
    }


    #region Number Typing & Clear
    private void TypeItem(PasswordItemSO passwordItemSO, bool immediately)
    {
        if (typedPassword.Contains(passwordItemSO)) return; //Item alreadyTyped
        if (typedPassword.Count >= PasswordUtilities.GetPasswordItemCount()) return;

        int targetIndex = typedPassword.Count;

        typedPassword.Add(passwordItemSO);

        OnPasswordItemTyped?.Invoke(this, new OnPasswordItemEventArgs { index = targetIndex, passwordItemSO = passwordItemSO, immediately = immediately });
    }

    private void DeleteLastTypedItem(bool immediately)
    {
        if (typedPassword.Count <= 0) return;

        int targetIndex = typedPassword.Count - 1;
        PasswordItemSO passwordItemSO = typedPassword[targetIndex];

        typedPassword.Remove(typedPassword[^1]);

        OnPasswordItemDeleted?.Invoke(this, new OnPasswordItemEventArgs { index = targetIndex, passwordItemSO = passwordItemSO, immediately = immediately });
    }

    private void ClearTypedPassword(bool immediately)
    {
        while(typedPassword.Count > 0)
        {
            DeleteLastTypedItem(immediately);
        }
    }
    #endregion

    #region Utility Methods

    private void SetButtons()
    {
        List<PasswordItemSO> itemsPool = new List<PasswordItemSO>(passwordItemsPool);

        if(randomizeButtons) itemsPool = GeneralUtilities.FisherYatesShuffle(passwordItemsPool);

        for (int i = 0; i < passwordButtonHandlers.Count; i++)
        {
            //ItemsPool has to be at least the same size as passwordButtonHandlers
            passwordButtonHandlers[i].SetButton(itemsPool[i]);
        }
    }

    public bool CompletePasswordTyped() => typedPassword.Count >= PasswordUtilities.GetPasswordItemCount();
    #endregion

    #region Subscriptions
    private void PasswordConfigurationButtonHandler_OnPasswordButtonClicked(object sender, PasswordConfigurationButtonHandler.OnPasswordButtonClickedEventArgs e)
    {
        TypeItem(e.passwordItem, false);
    }
    #endregion
}
