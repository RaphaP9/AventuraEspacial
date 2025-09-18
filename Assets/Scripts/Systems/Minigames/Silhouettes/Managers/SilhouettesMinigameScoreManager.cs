using UnityEngine;

public class SilhouettesMinigameScoreManager : MinigameScoreManager
{
    protected override int GetBaseScorePerHit() => 0;
    protected override int GetBonusScorePerCombo() => 0;
    protected override int GetMaxCombo() => 0;

}
