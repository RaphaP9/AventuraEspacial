using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MemoryMinigameSettingsSO", menuName = "ScriptableObjects/Minigames/Memory/Settings")]
public class MemoryMinigameSettings : ScriptableObject
{
    [Header("Rounds Settings")]
    public List<MemoryRound> rounds;
    [Space]
    public List<MemoryCardSO> cardPool;

    [Header("Score Settings")]
    [SerializeField, Range(1,10)] public int baseScorePerPairMatch;
    [SerializeField, Range(2,10)] public int maxCombo;
    [SerializeField, Range(1,10)] public int bonusScorePerCombo;

    [Header("Low Level Timers")]
    [Range(0f, 5f)] public float startingGameTime;
    [Range(0f, 5f)] public float timeBetweenPairs;
    [Range(0f, 5f)] public float allPairsMatchTime;
    [Range(0f, 5f)] public float switchRoundTimer;
    [Range(0f, 5f)] public float endingGameTime;
}


