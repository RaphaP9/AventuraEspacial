using UnityEngine;

public class PasswordConfigurationIndicatorsContainerHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PasswordConfigurationUIHandler passwordConfigurationUIHandler;
    [SerializeField] private Transform indicatorsContainer;
    [SerializeField] private Transform indicatorPrefab;

    [Header("Debug")]
    [SerializeField] private bool debug;

    private void OnEnable()
    {
        passwordConfigurationUIHandler.OnPasswordHandlerInitialized += PasswordConfigurationUIHandler_OnPasswordHandlerInitialized;
    }

    private void OnDisable()
    {
        passwordConfigurationUIHandler.OnPasswordHandlerInitialized -= PasswordConfigurationUIHandler_OnPasswordHandlerInitialized;
    }

    private void CreateIndicators(int quantity)
    {
        for(int i = 0; i < quantity; i++)
        {
            CreateIndicator(i);
        }
    }

    private void CreateIndicator(int index)
    {
        Transform indicatorTransform = Instantiate(indicatorPrefab, indicatorsContainer);

        PasswordConfigurationIndicatorHandler indicatorHandler = indicatorTransform.GetComponent<PasswordConfigurationIndicatorHandler>();

        if(indicatorHandler == null)
        {
            if (debug) Debug.Log("Instantiated prefab does not contain a PasswordConfigurationIndicatorHandler component");
            return;
        }

        indicatorHandler.SetIndicator(index, passwordConfigurationUIHandler);
    }

    private void ClearContainer()
    {
        foreach(Transform child in indicatorsContainer)
        {
            Destroy(child.gameObject);
        }
    }

    #region Subscriptions
    private void PasswordConfigurationUIHandler_OnPasswordHandlerInitialized(object sender, PasswordConfigurationUIHandler.OnHandlerInitializedEventArgs e)
    {
        ClearContainer();
        CreateIndicators(e.passwordItemsCount);
    }
    #endregion 
}
