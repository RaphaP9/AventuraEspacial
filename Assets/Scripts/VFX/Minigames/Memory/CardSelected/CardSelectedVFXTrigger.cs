using UnityEngine;

public class CardSelectedVFXTrigger : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Transform VFXContainer;
    [SerializeField] private MemoryCardHandler memoryCardHandler;
    [SerializeField] private Transform cardSelectedVFXPrefab;

    [Header("Settings")]
    [SerializeField, Range(100, 300)] private float defaultScaleReferenceSize;

    private void OnEnable()
    {
        memoryCardHandler.OnThisCardRevealed += MemoryCardHandler_OnCardOnThisCardRevealed;
    }

    private void OnDisable()
    {
        memoryCardHandler.OnThisCardRevealed -= MemoryCardHandler_OnCardOnThisCardRevealed;
    }

    private void CreateVFX()
    {
        Transform prefabTransform = Instantiate(cardSelectedVFXPrefab, VFXContainer);

        CardSelectedVFXHandler cardSelectedVFXHandler = prefabTransform.GetComponent<CardSelectedVFXHandler>();

        if (cardSelectedVFXHandler == null) return;

        cardSelectedVFXHandler.SetSizeByFactor(GetFactorByRectWidth());
    }

    private float GetFactorByRectWidth()
    {
        float factor = rectTransform.rect.width / defaultScaleReferenceSize;
        return factor;
    }

    private void MemoryCardHandler_OnCardOnThisCardRevealed(object sender, System.EventArgs e)
    {
        CreateVFX();
    }
}