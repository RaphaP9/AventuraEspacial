using UnityEngine;

public class BoolTimePassingHandler : TimePassingHandler
{
    [Header("Settings")]
    [SerializeField] private bool canPassTime;

    public override bool CanPassTime()
    {
        if (!base.CanPassTime()) return false;

        return canPassTime;
    }


}
