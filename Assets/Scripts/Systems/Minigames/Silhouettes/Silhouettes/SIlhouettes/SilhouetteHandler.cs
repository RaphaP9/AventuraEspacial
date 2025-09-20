using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SilhouetteHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI Components")]
    [SerializeField] private Image silhouetteImage;

    [Header("Components")]
    [SerializeField] private SilhouetteAnimationController animatorController;

    [Header("Runtime Filled")]
    [SerializeField] private SilhouetteSO silhouetteSO;
    [SerializeField] private bool isPointerOn;
    [Space]
    [SerializeField] private bool isMatched;
    [SerializeField] private bool isFailing; //Managed by animation events

    public SilhouetteSO SilhouetteSO => silhouetteSO;

    public static event EventHandler<OnSilhouetteEventArgs> OnSilhouettePointerEnter;
    public static event EventHandler<OnSilhouetteEventArgs> OnSilhouettePointerExit;

    public class OnSilhouetteEventArgs : EventArgs
    {
        public SilhouetteHandler silhouetteHandler;
    }

    #region Setters
    public void SetSilhouette(SilhouetteSO silhouetteSO)
    {
        this.silhouetteSO = silhouetteSO;

        isMatched = false;
        isFailing = false;

        SetSilhouetteImage(silhouetteSO.sprite);
        SetSilhouetteMaterial(silhouetteSO.silhouetteMaterial);
    }

    private void SetSilhouetteImage(Sprite sprite) => silhouetteImage.sprite = sprite;
    private void SetSilhouetteMaterial(Material material) => silhouetteImage.material = material;

    #endregion

    #region Pointer Methods

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOn = true;
        OnSilhouettePointerEnter?.Invoke(this, new OnSilhouetteEventArgs { silhouetteHandler = this });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOn = false;
        OnSilhouettePointerExit?.Invoke(this, new OnSilhouetteEventArgs { silhouetteHandler = this });
    }
    #endregion

    #region Public Methods
    public void MatchSilhouette()
    {
        isMatched = true;
        animatorController.PlayMatchAnimation();
    }

    public void FailMatch()
    {
        isFailing = true;
        animatorController.PlayFailAnimation();
    }
    #endregion

    public void OnFailCompleted() => isFailing = false;
}
