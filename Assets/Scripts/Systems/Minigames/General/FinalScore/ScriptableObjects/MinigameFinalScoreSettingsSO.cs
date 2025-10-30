using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MinigameFinalScoreSettingsSO", menuName = "ScriptableObjects/Minigames/FinalScoreSettings")]
public class MinigameFinalScoreSettingsSO : ScriptableObject
{
    [Header("Lists")]
    public List<MinigameFinalScoreSetting> minigameFinalScoreSettingsList;
}
