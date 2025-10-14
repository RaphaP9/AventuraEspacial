using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CollectablePanelUI : MonoBehaviour
{
    public static CollectablePanelUI Instance { get; private set; }

    [Header("UI Components")]
    [SerializeField] private Button closeButton;
    [SerializeField] private UIPointerDetector exitUIDetector;

    [Header("Test")]
    [SerializeField] private CollectableSO testCollectable;

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

    private void Update()
    {
        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            OpenCollectablePanel(testCollectable,true);
        }

        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            OpenCollectablePanel(testCollectable,false);
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
