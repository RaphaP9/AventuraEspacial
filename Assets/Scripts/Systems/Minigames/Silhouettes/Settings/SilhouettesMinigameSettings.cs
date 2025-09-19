using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SilhouettesMinigameSettingsSO", menuName = "ScriptableObjects/Minigames/Silhouettes/Settings")]
public class SilhouettesMinigameSettings : ScriptableObject
{
    [Header("Rounds Settings")]
    public List<SilhouettesRound> rounds;

    [Header("Score Settings")]
    [Range(1, 10)] public int baseScorePerSilhouetteMatch;
    [Range(2, 10)] public int maxCombo;
    [Range(1, 10)] public int bonusScorePerCombo;

    [Header("Time Settings")]
    [SerializeField, Range(0f, 600f)] public float gameTime;
    [SerializeField, Range(0f, 10f)] public float timeBonusOnMatch;
    [SerializeField, Range(0f, 10f)] public float timePenaltyOnFail;

    [Header("Low Level Timers")]
    [Range(0f, 5f)] public float startingGameTime;
    //[Range(0f, 5f)] public float timeBetweenPairs;
    //[Range(0f, 5f)] public float allPairsMatchTime;
    [Range(0f, 5f)] public float switchRoundTimer;
    [Range(0f, 5f)] public float endLastRoundTimer;
    [Range(0f, 5f)] public float endingGameTime;
}
