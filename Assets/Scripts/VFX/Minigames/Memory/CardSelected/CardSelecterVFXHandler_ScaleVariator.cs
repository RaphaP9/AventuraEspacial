using UnityEngine;

public class CardSelecterVFXHandler_ScaleVariator : CardSelectedVFXHandler
{
    public override void SetSizeByFactor(float factor)
    {
        transform.localScale = transform.localScale * factor;
    }
}
