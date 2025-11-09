using UnityEngine;

public class CardSelectedVFXHandler : MonoBehaviour
{
    public void SetScaleByFactor(float factor)
    {
        transform.localScale = transform.localScale * factor;
    }
}
