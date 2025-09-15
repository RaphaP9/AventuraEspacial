using UnityEngine;

[System.Serializable]
public class MemoryRound
{
    public Sprite cardBackSprite;
    [Range(1, 10)] public int pairCount;
    [Range(1f, 10f)] public float revealTime;
    [Space]
    [Range(2, 10)] public int gridColumnCount;
}
