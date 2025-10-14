using System;
using UnityEngine;
using UnityEngine.UI;

public class BackpackHandler : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Image backpackImage;

    [Header("Components")]
    [SerializeField] private BackpackAnimationController animationController;

    [Header("Runtime Filled")]
    [SerializeField] private bool isFull;

    private void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        isFull = false;
    }

    #region Public Methods
    public void AddItem()
    {
        animationController.PlayAddItemAnimation();
    }

    public void Full()
    {
        isFull = true;
        animationController.PlayFullAnimation();
    }
    #endregion
}
