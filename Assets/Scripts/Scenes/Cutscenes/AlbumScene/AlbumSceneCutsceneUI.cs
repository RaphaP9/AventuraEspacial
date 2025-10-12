using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class AlbumSceneCutsceneUI : MonoBehaviour
{
    public static AlbumSceneCutsceneUI Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private Button skipCutsceneButton;
    [SerializeField] private UIPointerDetector skipCutscenePanelDetector;

    [Header("Test")]
    [SerializeField] private CutsceneSO cutsceneA;
    [SerializeField] private CutsceneSO cutsceneB;

    private void OnEnable()
    {
        skipCutscenePanelDetector.OnPointerPressed += SkipCutscenePanelDetector_OnPointerPressed;
    }

    private void OnDisable()
    {
        skipCutscenePanelDetector.OnPointerPressed -= SkipCutscenePanelDetector_OnPointerPressed;
    }

    private void Awake()
    {
        IntializeButtonsListeners();
        SetSingleton();
    }

    private void Update()
    {
        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            PlayCutscene(cutsceneA);
        }

        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            PlayCutscene(cutsceneB);
        }
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There is more than one AlbumSceneCutsceneUI instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void IntializeButtonsListeners()
    {
        skipCutsceneButton.onClick.AddListener(SkipCutscenePanel);
    }

    private void SkipCutscenePanel() => AlbumSceneCutsceneUIHandler.Instance.SkipCutscene();
    public void PlayCutscene(CutsceneSO cutsceneSO) => AlbumSceneCutsceneUIHandler.Instance.PlayCutscene(cutsceneSO);  

    #region Subscriptions
    private void SkipCutscenePanelDetector_OnPointerPressed(object sender, EventArgs e)
    {
        AlbumSceneCutsceneUIHandler.Instance.SkipCutscenePanel();
    }
    #endregion
}
