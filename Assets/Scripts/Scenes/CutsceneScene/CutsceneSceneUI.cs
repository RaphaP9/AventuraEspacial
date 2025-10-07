using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneSceneUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Button skipCutscenePanelButton;

    private void Awake()
    {
        IntializeButtonsListeners();
    }

    private void IntializeButtonsListeners()
    {
        skipCutscenePanelButton.onClick.AddListener(SkipCutscenePanel);
    }

    private void SkipCutscenePanel() => CutsceneSceneUIHandler.Instance.SkipCutscenePanel();

}
