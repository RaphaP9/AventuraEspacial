using System;
using UnityEngine;
using UnityEngine.UI;

public class URLButton : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Button button;

    [Header("Settingss")]
    [SerializeField] private string url;

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        button.onClick.AddListener(GoToURL);
    }

    private void GoToURL()
    {
        Debug.Log($"Opening URL: {url}");
        Application.OpenURL(url);
    }
}
