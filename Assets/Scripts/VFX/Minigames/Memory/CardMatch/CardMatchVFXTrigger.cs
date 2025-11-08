using UnityEngine;

public class CardMatchVFXTrigger : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private MemoryCardHandler memoryCardHandler;
    [SerializeField] private Transform cardMatchVFXPrefab;

    [Header("Settings")]
    [SerializeField, Range(100,300)] private float defaultScaleReferenceSize;

    private void OnEnable()
    {
        memoryCardHandler.OnCardMatch += MemoryCardHandler_OnCardMatch;
    }

    private void OnDisable()
    {
        memoryCardHandler.OnCardMatch -= MemoryCardHandler_OnCardMatch;
    }

    private void CreateVFX()
    {
        Transform prefabTransform = Instantiate(cardMatchVFXPrefab, transform);

        CardMatchVFXHandler cardMatchVFXHandler = prefabTransform.GetComponent<CardMatchVFXHandler>();

        if (cardMatchVFXHandler == null) return;

        cardMatchVFXHandler.SetScaleByFactor(GetFactorByRectWidth());
    }

    private float GetFactorByRectWidth()
    {
        float factor = rectTransform.rect.width / defaultScaleReferenceSize;
        return factor;
    }

    private void MemoryCardHandler_OnCardMatch(object sender, System.EventArgs e)
    {
        CreateVFX();
    }
}
