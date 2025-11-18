using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class PasswordEntryUIHandler : MonoBehaviour
{
    [Header("Lists")]
    [SerializeField] private List<PasswordEntryButtonHandler> passwordButtonHandlers;
    [Space]
    [SerializeField] private List<PasswordItemSO> passwordItemsPool;

    [Header("Components")]
    [SerializeField] private PasswordEntryUI passwordEntryUI;
    [SerializeField] private Button deletePasswordButton;
    [SerializeField] private Button bypassButton;

    [Header("Settings")]
    [SerializeField] private bool randomizeButtons;
    [SerializeField, Range(0f, 0.5f)] private float checkTime;

    [Header("Runtime Filled")]
    [SerializeField] private List<PasswordItemSO> typedPassword;
    [SerializeField] private bool performingCheck;

    [Header("Testing")]
    [SerializeField] private Button mockButton;

    public event EventHandler<OnHandlerInitializedEventArgs> OnPasswordHandlerInitialized;

    public event EventHandler<OnPasswordItemEventArgs> OnPasswordItemTyped;
    public event EventHandler<OnPasswordItemEventArgs> OnPasswordItemDeleted;

    public event EventHandler OnCompletePasswordTyped;
    public event EventHandler OnCompletePasswordTypedCorrectly;
    public event EventHandler OnCompletePasswordTypedWrong;

    public event EventHandler OnCompletePasswordTypedCorrectlyPre;
    public event EventHandler OnCompletePasswordTypedWrongPre;


    public event EventHandler OnPasswordCleared;

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
        foreach (PasswordEntryButtonHandler passwordEntryButtonHandler in passwordButtonHandlers)
        {
            passwordEntryButtonHandler.OnPasswordButtonClicked += PasswordConfigurationButtonHandler_OnPasswordButtonClicked;
        }
    }

    private void OnDisable()
    {
        foreach (PasswordEntryButtonHandler passwordEntryButtonHandler in passwordButtonHandlers)
        {
            passwordEntryButtonHandler.OnPasswordButtonClicked -= PasswordConfigurationButtonHandler_OnPasswordButtonClicked;
        }
    }

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void Start()
    {
        InitializeVariables();
        ClearTypedPassword(true);
        SetButtons();
        InitializePasswordHandler();
    }

    private void InitializeButtonsListeners()
    {
        deletePasswordButton.onClick.AddListener(() => DeleteLastTypedItem(false));
        bypassButton.onClick.AddListener(UnlockUI);
    }

    private void InitializeVariables()
    {
        performingCheck = false;
    }

    private void InitializePasswordHandler()
    {
        OnPasswordHandlerInitialized?.Invoke(this, new OnHandlerInitializedEventArgs { passwordItemsCount = PasswordUtilities.GetPasswordItemCount() });
    }


    #region Number Typing & Clear
    private void TypeItem(PasswordItemSO passwordItemSO, bool immediately)
    {
        if (performingCheck) return;
        if (typedPassword.Contains(passwordItemSO)) return; //Item alreadyTyped
        if (typedPassword.Count >= PasswordUtilities.GetPasswordItemCount()) return;

        int targetIndex = typedPassword.Count;

        typedPassword.Add(passwordItemSO);

        OnPasswordItemTyped?.Invoke(this, new OnPasswordItemEventArgs { index = targetIndex, passwordItemSO = passwordItemSO, immediately = immediately });

        if (CompletePasswordTyped())
        {
            OnCompletePasswordTyped?.Invoke(this, EventArgs.Empty);
            StartCoroutine(PasswordCheckCoroutine());
        }
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
        while (typedPassword.Count > 0)
        {
            DeleteLastTypedItem(immediately);
        }

        OnPasswordCleared?.Invoke(this, EventArgs.Empty);
    }
    #endregion

    #region Coroutines
    private IEnumerator PasswordCheckCoroutine()
    {
        performingCheck = true;

        bool correctPassword = PasswordTypedCorrectely();

        if (correctPassword)
        {
            OnCompletePasswordTypedCorrectlyPre?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            OnCompletePasswordTypedWrongPre?.Invoke(this, EventArgs.Empty);
        }

        yield return new WaitForSecondsRealtime(checkTime);

        if (correctPassword)
        {
            UnlockUI();
            OnCompletePasswordTypedCorrectly?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            ClearTypedPassword(false);
            OnCompletePasswordTypedWrong?.Invoke(this, EventArgs.Empty);
        }

        performingCheck = false;
    }

    #endregion

    #region Utility Methods

    private void SetButtons()
    {
        List<PasswordItemSO> itemsPool = new List<PasswordItemSO>(passwordItemsPool);

        if (randomizeButtons) itemsPool = GeneralUtilities.FisherYatesShuffle(passwordItemsPool);

        for (int i = 0; i < passwordButtonHandlers.Count; i++)
        {
            //ItemsPool has to be at least the same size as passwordButtonHandlers
            passwordButtonHandlers[i].SetButton(itemsPool[i]);
        }
    }

    public bool CompletePasswordTyped() => typedPassword.Count >= PasswordUtilities.GetPasswordItemCount();

    private bool PasswordTypedCorrectely()
    {
        return DataContainer.Instance.PasswordMatches(typedPassword);
    }

    private void UnlockUI()
    {
        passwordEntryUI.LoadNextScene();
    }
    #endregion

    #region Subscriptions
    private void PasswordAccessUI_OnThisPasswordAccessUIOpen(object sender, EventArgs e)
    {
        SetButtons();
        ClearTypedPassword(true);
    }

    private void PasswordAccessUI_OnThisPasswordAccessUICloseCompletely(object sender, EventArgs e)
    {
        ClearTypedPassword(true);
    }

    private void PasswordConfigurationButtonHandler_OnPasswordButtonClicked(object sender, PasswordEntryButtonHandler.OnPasswordButtonClickedEventArgs e)
    {
        TypeItem(e.passwordItem, false);
    }
    #endregion
}
