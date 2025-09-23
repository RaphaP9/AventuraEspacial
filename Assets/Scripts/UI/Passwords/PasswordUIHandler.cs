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

    [Header("Components")]
    [SerializeField] private Button deletePasswordButton;

    [Header("Runtime Filled")]
    [SerializeField] private List<PasswordNumberSO> typedPassword;

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        deletePasswordButton.onClick.AddListener(DeletePassword);
    }

    private void DeletePassword()
    {

    }
}
