using System;
using TMPro;
using UnityEngine;

public class CurrentTimeUIHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI currentTimeText;

    private string currentStringTime = "";

    private void Start()
    {
        InitializeVariables();
    }

    private void Update()
    {
        HandleCurrentTime();
    }
    private void InitializeVariables()
    {
        currentStringTime = "";

    }
    private void HandleCurrentTime()
    {
        string stringTime = FormattingUtilities.GetCurrentTime24HS();

        if (currentStringTime == stringTime) return;

        currentStringTime = stringTime;
        currentTimeText.text = currentStringTime;
    }

}
