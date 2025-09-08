using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUIMainContentButtonsHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private OptionsUIContentsHandler optionsUIContentsHandler;

    [Header("UI Components")]
    [SerializeField] private Button parentsOptionsButton;

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        parentsOptionsButton.onClick.AddListener(ShowParentsContent);
    }

    private void ShowParentsContent() => optionsUIContentsHandler.ShowParentsContent();
}
