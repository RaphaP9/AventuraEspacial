using UnityEngine;

public class PasswordEntryIndicatorsContainerHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PasswordEntryUIHandler passwordEntryUIHandler;
    [SerializeField] private Transform indicatorsContainer;
    [SerializeField] private Transform indicatorPrefab;
    [Space]
    [SerializeField] private Animator animator;

    [Header("Debug")]
    [SerializeField] private bool debug;

    private const string SHAKE_TRIGGER = "Shake";

    private void OnEnable()
    {
        passwordEntryUIHandler.OnPasswordHandlerInitialized += PasswordEntryUIHandler_OnPasswordHandlerInitialized;
        passwordEntryUIHandler.OnCompletePasswordTypedWrongPre += PasswordEntryUIHandler_OnCompletePasswordTypedWrongPre;
    }

    private void OnDisable()
    {
        passwordEntryUIHandler.OnPasswordHandlerInitialized -= PasswordEntryUIHandler_OnPasswordHandlerInitialized;
        passwordEntryUIHandler.OnCompletePasswordTypedWrongPre -= PasswordEntryUIHandler_OnCompletePasswordTypedWrongPre;
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

        PasswordEntryIndicatorHandler indicatorHandler = indicatorTransform.GetComponent<PasswordEntryIndicatorHandler>();

        if (indicatorHandler == null)
        {
            if (debug) Debug.Log("Instantiated prefab does not contain a PasswordEntryIndicatorHandler component");
            return;
        }

        indicatorHandler.SetIndicator(index, passwordEntryUIHandler);
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
    private void PasswordEntryUIHandler_OnPasswordHandlerInitialized(object sender, PasswordEntryUIHandler.OnHandlerInitializedEventArgs e)
    {
        ClearContainer();
        CreateIndicators(e.passwordItemsCount);
    }

    private void PasswordEntryUIHandler_OnCompletePasswordTypedWrongPre(object sender, System.EventArgs e)
    {
        ShakeContainer();
    }
    #endregion 
}
