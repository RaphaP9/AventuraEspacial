using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MinigameFinalScoreSettingsSO", menuName = "ScriptableObjects/Minigames/FinalScoreSettings")]
public class MinigameFinalScoreSettingsSO : ScriptableObject
{
    [Header("General Settings")]
    public string stringLocalizationTable;
    public string assetLocalizationTable;

    [Header("Lists")]
    public List<MinigameFinalScoreSetting> minigameFinalScoreSettingsList;
}
