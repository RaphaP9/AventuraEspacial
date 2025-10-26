using UnityEngine;

public class CollectableCollectionUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform notificationsContainer;
    [SerializeField] private Transform collectableNotificationPrefab;

    [Header("Debug")]
    [SerializeField] private bool debug;

    private void OnEnable()
    {
        CollectableCollectionHandler.OnCollectableCollected += CollectableCollectionHandler_OnCollectableCollected;
    }

    private void OnDisable()
    {
        CollectableCollectionHandler.OnCollectableCollected -= CollectableCollectionHandler_OnCollectableCollected;
    }

    private void CreateNotification(CollectableSO collectableSO)
    {
        Transform notificationUI = Instantiate(collectableNotificationPrefab, notificationsContainer);

        CollectableCollectionNotificationUI collectableCollectionNotificationUI = notificationUI.GetComponent<CollectableCollectionNotificationUI>();

        if(collectableCollectionNotificationUI == null)
        {
            if (debug) Debug.Log("Instantiated notification does not contain a CollectableCollectionNotificationUI component.");
            return;
        }

        collectableCollectionNotificationUI.SetUI(collectableSO);
    }

    private void CollectableCollectionHandler_OnCollectableCollected(object sender, CollectableCollectionHandler.OnCollectableCollectedEventArgs e)
    {
        CreateNotification(e.collectableSO);
    }
}
