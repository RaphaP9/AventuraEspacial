using UnityEngine;
using System.Collections.Generic;
using System;

public class CollectableContainerPopulatorHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform collectablesContainer;
    [SerializeField] private Transform collectableUIPrefab;

    [Header("Lists")]
    [SerializeField] private List<CollectableSO> collectableList;

    [Header("Runtime Filled")]
    [SerializeField] private List<CollectableUI> collectableUIList;

    [Header("Debug")]
    [SerializeField] private bool debug;

    public static event EventHandler<OnCollectablesPopulatedEventArgs> OnCollectablesPopulated;

    public class OnCollectablesPopulatedEventArgs : EventArgs
    {
        public List<CollectableUI> collectableUIList;
    }

    private void Start()
    {
        ClearCollectableContainer();
        PopulateCollectableContainer();
    }

    private void ClearCollectableContainer()
    {
        foreach(Transform child in collectablesContainer)
        {
            Destroy(child.gameObject);
        }
    }

    private void PopulateCollectableContainer()
    {
        foreach(CollectableSO collectableSO in collectableList)
        {
            CreateCollectableUI(collectableSO);
        }

        OnCollectablesPopulated?.Invoke(this, new OnCollectablesPopulatedEventArgs { collectableUIList = collectableUIList });
    }

    private void CreateCollectableUI(CollectableSO collectableSO)
    {
        Transform collectableUITransform = Instantiate(collectableUIPrefab, collectablesContainer);

        CollectableUI collectableUI = collectableUITransform.GetComponent<CollectableUI>();

        if(collectableUI == null)
        {
            if (debug) Debug.Log("Instantiated prefab does not contain a CollectableUI Component.");
            return;
        }

        collectableUI.SetCollectable(collectableSO);
        collectableUIList.Add(collectableUI);
    }
}
