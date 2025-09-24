using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
public class PasswordUIHandler : MonoBehaviour
{
    private const int PASSWORD_COUNT = 4; //Fixed at 4 numbers

    [Header("Lists")]
    [SerializeField] private List<PasswordButtonHandler> passwordButtonHandlers;
    [Space]
    [SerializeField] private List<PasswordNumberTextHandler> passwordNumberTextHandlers; //Count must match the PASSWORD_COUNT
    [Space]
    [SerializeField] private List<PasswordIndicatorHandler> passwordIndicatorHandlers; //Count must match the PASSWORD_COUNT
    [Space]
    [SerializeField] private List<PasswordNumberSO> passwordNumberPool;

    [Header("Components")]
    [SerializeField] private PasswordUI passwordUI;
    [SerializeField] private Button deletePasswordButton;

    [Header("Runtime Filled")]
    [SerializeField] private List<PasswordNumberSO> correctPassword;
    [SerializeField] private List<PasswordNumberSO> typedPassword;

    public event EventHandler OnPasswordWrong;

    private void OnEnable()
    {
        passwordUI.OnPasswordUIOpen += PasswordUI_OnPasswordUIOpen;
    }

    private void OnDisable()
    {
        passwordUI.OnPasswordUIOpen -= PasswordUI_OnPasswordUIOpen;
    }

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        deletePasswordButton.onClick.AddListener(DeleteLastTypedNumber);

        foreach(PasswordButtonHandler handler in passwordButtonHandlers)
        {
            handler.PasswordButton.onClick.AddListener(() => TypeNumber(handler.PasswordNumberSO));
        }
    }

    private void RegeneratePasswordLogic()
    {
        ClearTypedPassword(true);

        GenerateNewPassword();
        RandomizeButtons();
    }


    #region Number Typing & Clear
    private void ClearTypedPassword(bool immediately)
    {
        typedPassword.Clear();
        UpdatePasswordIndicatorVisual(immediately);
    }

    private void DeleteLastTypedNumber()
    {
        if (typedPassword.Count <= 0) return;

        typedPassword.Remove(typedPassword[^1]);

        UpdatePasswordIndicatorVisual(false);
    }

    private void TypeNumber(PasswordNumberSO passwordNumberSO)
    {
        if (typedPassword.Count >= PASSWORD_COUNT) return;

        typedPassword.Add(passwordNumberSO);

        UpdatePasswordIndicatorVisual(false);

        if (!HasTypedACompletePassword()) return;

        if (VerifyPassword())
        {
            passwordUI.CloseUI();
            //OpenTheOptionsUI
        }
        else
        {
            OnPasswordWrong?.Invoke(this, EventArgs.Empty);
            ClearTypedPassword(false);
        }

    }
    #endregion

    #region Visual Indicators
    private void UpdatePasswordText()
    {
        for (int i = 0; i < correctPassword.Count; i++)
        {
            passwordNumberTextHandlers[i].SetNumberText(correctPassword[i]);
        }
    }

    private void UpdatePasswordIndicatorVisual(bool immediately)
    {
        int numbersToFill = typedPassword.Count;

        foreach(PasswordIndicatorHandler handler in passwordIndicatorHandlers)
        {
            if(numbersToFill > 0)
            {
                numbersToFill--;

                if (handler.IsFilled) continue;

                if (immediately) handler.FillImmediately();
                else handler.Fill();
            }
            else
            {
                if(!handler.IsFilled) continue;

                if (immediately) handler.UnfillImmediately();
                else handler.Unfill();
            }
        }
    }
    #endregion

    #region Utility Methods
    private void GenerateNewPassword()
    {
        List<PasswordNumberSO> generatedPassword = GeneralUtilities.ChooseNRandomDifferentItemsFromPool(passwordNumberPool, PASSWORD_COUNT);

        correctPassword.Clear();

        for (int i = 0; i < PASSWORD_COUNT; i++)
        {
            correctPassword.Add(generatedPassword[i]);
        }

        UpdatePasswordText();
    }

    private void RandomizeButtons()
    {
        List<PasswordNumberSO> randomizedPool = GeneralUtilities.FisherYatesShuffle(passwordNumberPool);

        for (int i = 0; i < passwordButtonHandlers.Count; i++)
        {
            passwordButtonHandlers[i].SetButton(randomizedPool[i]);
        }
    }

    private bool VerifyPassword()
    {
        if(!HasTypedACompletePassword()) return false;

        for(int i = 0; i < typedPassword.Count; i++)
        {
            if(typedPassword[i] != correctPassword[i]) return false;
        }

        return true;
    }

    private bool HasTypedACompletePassword() => typedPassword.Count == correctPassword.Count;
    #endregion

    #region Subscriptions
    private void PasswordUI_OnPasswordUIOpen(object sender, EventArgs e)
    {
        RegeneratePasswordLogic();
    }
    #endregion
}
