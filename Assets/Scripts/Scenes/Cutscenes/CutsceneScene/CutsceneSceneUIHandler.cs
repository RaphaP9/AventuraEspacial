using UnityEngine;

public class CutsceneSceneUIHandler : MonoBehaviour
{
    public static CutsceneSceneUIHandler Instance { get; private set; }

    [Header("Next Scene Settings")]
    [SerializeField] private string nextScene;
    [SerializeField] private TransitionType nextSceneTransitionType;

    [Header("Components")]
    [SerializeField] private CutsceneSO cutsceneSO;

    [Header("UI Components")]
    [SerializeField] private Transform cutscenePanelsContainer;
    [SerializeField] private Transform cutscenePanelPrefab;

    [Header("Settings")]
    [SerializeField,Range(3,5)] private int maxCutscenePanels;

    [Header("Runtime Filled")]
    [SerializeField] private CutscenePanelUIHandler currentCutscenePanelUI;
    [SerializeField] private CutscenePanel currentCutscenePanel;
    [SerializeField] private int currentCutscenePanelIndex;

    [Header("Debug")]
    [SerializeField] private bool debug;

    private void Awake()
    {
        SetSingleton();
    }

    private void Start()
    {
        InitializeVariables();
        CreateCutscenePanel(currentCutscenePanelIndex);
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There is more than one CutsceneSceneManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void InitializeVariables()
    {
        currentCutscenePanelIndex = 0;
    }

    #region Panels

    private void CreateCutscenePanel(int index)
    {
        Transform cutscenePanelTransform = Instantiate(cutscenePanelPrefab, cutscenePanelsContainer);
        CutscenePanelUIHandler cutscenePanelUIHandler = cutscenePanelTransform.GetComponentInChildren<CutscenePanelUIHandler>();

        if (cutscenePanelUIHandler == null)
        {
            if (debug) Debug.Log("Instantiated CutscenePanelUI does not contain a CutscenePanelUIHandler.");
            return;
        }

        CutscenePanel cutscenePanel = cutsceneSO.cutscenePanels[index];

        cutscenePanelUIHandler.SetPanel(cutscenePanel);

        currentCutscenePanelUI = cutscenePanelUIHandler;
        currentCutscenePanel = cutscenePanel;

        EvaluatePanelContainerClearance();
    }


    private void CreateNextCutscenePanel()
    {
        if(currentCutscenePanelUI != null)
        {
            currentCutscenePanelUI.DisposePanel();
        }

        currentCutscenePanelIndex++;
        CreateCutscenePanel(currentCutscenePanelIndex);
    }

    private void EvaluatePanelContainerClearance()
    {
        if(cutscenePanelsContainer.childCount > maxCutscenePanels)
        {
            Destroy(cutscenePanelsContainer.GetChild(0).gameObject); //Destroy the first child
        }
    }
    #endregion

    #region Public Methods
    public void SkipCutscenePanel()
    {
        if (!currentCutscenePanelUI.CanSkipPanel) return;

        if (cutsceneSO.IsLastCutscenePanel(currentCutscenePanel))
        {
            SkipCutscene();
        }
        else
        {
            CreateNextCutscenePanel();
        }
    }

    public void SkipCutscene() => ScenesManager.Instance.TransitionLoadTargetScene(nextScene,nextSceneTransitionType);
    #endregion
}
