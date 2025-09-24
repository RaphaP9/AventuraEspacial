using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PasswordButtonHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Button passwordButton;
    [SerializeField] private TextMeshProUGUI passwordButtonText;

    [Header("Runtime Filled")]
    [SerializeField] private PasswordNumberSO passwordNumberSO;

    public PasswordNumberSO PasswordNumberSO => passwordNumberSO;
    public Button PasswordButton => passwordButton;

    public event EventHandler OnPasswordButtonClicked;

    public class OnPasswordButtonClickedEventArgs : EventArgs
    {
        public PasswordNumberSO passwordNumberSO;
    }

    public void SetButton(PasswordNumberSO passwordNumberSO)
    {
        this.passwordNumberSO = passwordNumberSO;
        SetText(passwordNumberSO.number.ToString());
    }

    public void SetText(string text) => passwordButtonText.text = text;
}
