using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MemoryMinigameSettingsSO", menuName = "ScriptableObjects/Minigames/Memory/Settings")]
public class MemoryMinigameSettings : ScriptableObject
{
    [Range(0f, 2f)] public float startingGameTime;
    [Range(0f, 2f)] public float roundIntervalTime;
    [Range(0f, 2f)] public float endingGameTime;
    [Space]
    public List<MemoryRound> rounds;
    [Space]
    public List<MemoryCardSO> cardPool;
}


