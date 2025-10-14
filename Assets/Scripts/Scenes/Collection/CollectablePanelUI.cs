using UnityEngine;
using UnityEngine.UI;

public class CollectablePanelUI : MonoBehaviour
{
    public static CollectablePanelUI Instance { get; private set; }

    [Header("UI Components")]
    [SerializeField] private Button closeButton;
    [SerializeField] private UIPointerDetector exitUIDetector;

    private void OnEnable()
    {
        exitUIDetector.OnPointerClicked += ExitUIDetector_OnPointerClicked;
    }

    private void OnDisable()
    {
        exitUIDetector.OnPointerClicked -= ExitUIDetector_OnPointerClicked;
    }

    private void Awake()
    {
        SetSingleton();
        InitializeButtonsListeners();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //Debug.LogWarning("There is more than one CollectablePanelUI instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void InitializeButtonsListeners()
    {
        closeButton.onClick.AddListener(CloseCollectablePanel);
    }

    public void OpenCollectablePanel(CollectableSO collectableSO, bool isCollected)
    {
        CollectablePanelUIHandler.Instance.ShowCollectablePanel(collectableSO, isCollected);
    }

    public void CloseCollectablePanel()
    {
        CollectablePanelUIHandler.Instance.CloseCollectablePanel();
    }

    #region Subscriptions
    private void ExitUIDetector_OnPointerClicked(object sender, System.EventArgs e)
    {
        CloseCollectablePanel();
    }
    #endregion
}
