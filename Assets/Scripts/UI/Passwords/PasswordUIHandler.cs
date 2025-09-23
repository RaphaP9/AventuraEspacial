using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
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
    [SerializeField] private PasswordIndicatorsContainerHandler passwordIndicatorsContainerHandler;
    [SerializeField] private Button deletePasswordButton;

    [Header("Runtime Filled")]
    [SerializeField] private List<PasswordNumberSO> correctPassword;
    [SerializeField] private List<PasswordNumberSO> typedPassword;

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
        deletePasswordButton.onClick.AddListener(DeletePassword);
    }

    private void RegeneratePasswordLogic()
    {
        GenerateNewPassword();
        RandomizeButtons();
    }

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

    private void UpdatePasswordText()
    {
        for (int i = 0; i < correctPassword.Count; i++)
        {
            passwordNumberTextHandlers[i].SetNumberText(correctPassword[i]);
        }
    }

    private void RandomizeButtons()
    {
        List<PasswordNumberSO> randomizedPool = GeneralUtilities.FisherYatesShuffle(passwordNumberPool);

        for(int i = 0; i < passwordButtonHandlers.Count; i++)
        {
            passwordButtonHandlers[i].SetButton(randomizedPool[i]);
        }
    }

    private void DeletePassword()
    {

    }

    #region Subscriptions
    private void PasswordUI_OnPasswordUIOpen(object sender, EventArgs e)
    {
        RegeneratePasswordLogic();
    }
    #endregion
}
