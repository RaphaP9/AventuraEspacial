using UnityEngine;

public class PasswordConfigurationIndicatorsContainerHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PasswordConfigurationUIHandler passwordConfigurationUIHandler;
    [SerializeField] private Transform indicatorsContainer;
    [SerializeField] private Transform indicatorPrefab;

    private const string SHAKE_TRIGGER = "Shake";

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
            CreateIndicator();
        }
    }

    private void CreateIndicator()
    {
        Transform indicatorTransform = Instantiate(indicatorPrefab, indicatorsContainer);

        
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
        CreateIndicators(e.passwordItemsCount);
    }
    #endregion 
}
