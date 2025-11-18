using UnityEngine;

public class PasswordAccessIndicatorsContainerHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private ItemPasswordAccessUIHandler itemPasswordAccessUIHandler;
    [SerializeField] private Transform indicatorsContainer;
    [SerializeField] private Transform indicatorPrefab;
    [Space]
    [SerializeField] private Animator animator;

    [Header("Debug")]
    [SerializeField] private bool debug;

    private const string SHAKE_TRIGGER = "Shake"; 

    private void OnEnable()
    {
        itemPasswordAccessUIHandler.OnPasswordHandlerInitialized += ItemPasswordAccessUIHandler_OnPasswordHandlerInitialized;
        itemPasswordAccessUIHandler.OnCompletePasswordTypedWrongPre += ItemPasswordAccessUIHandler_OnCompletePasswordTypedWrongPre;
    }

    private void OnDisable()
    {
        itemPasswordAccessUIHandler.OnPasswordHandlerInitialized -= ItemPasswordAccessUIHandler_OnPasswordHandlerInitialized;
        itemPasswordAccessUIHandler.OnCompletePasswordTypedWrongPre -= ItemPasswordAccessUIHandler_OnCompletePasswordTypedWrongPre;
    }

    private void CreateIndicators(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            CreateIndicator(i);
        }
    }

    private void CreateIndicator(int index)
    {
        Transform indicatorTransform = Instantiate(indicatorPrefab, indicatorsContainer);

        PasswordAccessIndicatorHandler indicatorHandler = indicatorTransform.GetComponent<PasswordAccessIndicatorHandler>();

        if (indicatorHandler == null)
        {
            if (debug) Debug.Log("Instantiated prefab does not contain a PasswordAccessIndicatorHandler component");
            return;
        }

        indicatorHandler.SetIndicator(index, itemPasswordAccessUIHandler);
    }

    private void ClearContainer()
    {
        foreach (Transform child in indicatorsContainer)
        {
            Destroy(child.gameObject);
        }
    }

    private void ShakeContainer()
    {
        animator.SetTrigger(SHAKE_TRIGGER);
    }

    #region Subscriptions
    private void ItemPasswordAccessUIHandler_OnPasswordHandlerInitialized(object sender, ItemPasswordAccessUIHandler.OnHandlerInitializedEventArgs e)
    {
        ClearContainer();
        CreateIndicators(e.passwordItemsCount);
    }

    private void ItemPasswordAccessUIHandler_OnCompletePasswordTypedWrongPre(object sender, System.EventArgs e)
    {
        ShakeContainer();
    }
    #endregion 
}
