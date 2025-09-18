using UnityEngine;

public class SilhouettesMinigameTimerManager : MinigameTimerManager
{
    protected override bool CanPassTime() => true;
    protected override float GetGameTime() => 0f;
}
