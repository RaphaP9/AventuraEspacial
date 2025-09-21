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

    #region Setters
    public void SetBackpack(Sprite sprite)
    {
        isFull = false;
        SetBackpackImage(sprite);
    }

    private void SetBackpackImage(Sprite sprite) => backpackImage.sprite = sprite;
    #endregion

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
