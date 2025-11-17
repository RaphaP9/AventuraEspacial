using UnityEngine;
using UnityEngine.UI;
using System;

public class ItemPasswordAccessUIHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PasswordAccessUI passwordAccessUI;

    [Header("Testing")]
    [SerializeField] private Button mockButton;

    private void Start()
    {
        mockButton.onClick.AddListener(() => passwordAccessUI.UnlockUI());
    }
}
