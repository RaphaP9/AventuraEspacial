using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MemoryMinigameSettingsSO", menuName = "ScriptableObjects/Minigames/Memory/Settings")]
public class MemoryMinigameSettings : ScriptableObject
{
    public List<MemoryRound> rounds;
    [Space]
    public List<MemoryCardSO> cardPool;

    [Header("Timers")]
    [Range(0f, 5f)] public float startingGameTime;
    [Range(0f, 5f)] public float timeBetweenPairs;
    [Range(0f, 5f)] public float allPairsMatchTime;
    [Range(0f, 5f)] public float switchRoundTimer;
    [Range(0f, 5f)] public float endingGameTime;
}


