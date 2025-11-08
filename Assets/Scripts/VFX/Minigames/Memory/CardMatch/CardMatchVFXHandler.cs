using UnityEngine;

public class CardMatchVFXHandler : MonoBehaviour
{
    public void SetScaleByFactor(float factor)
    {
        transform.localScale = transform.localScale * factor;
    }
}
