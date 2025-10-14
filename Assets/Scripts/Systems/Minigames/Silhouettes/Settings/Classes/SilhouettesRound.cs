using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SilhouettesRound
{
    [Range(1, 10)] public int silhouettesCount;
    [Space]
    public List<SilhouetteSO> silhouettesPool;

    [Header("Prefab Settings")]
    public Transform backpackPrefab;
}