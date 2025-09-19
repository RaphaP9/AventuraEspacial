using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SilhouettesRound
{
    public Sprite backpackSprite;
    [Range(1, 10)] public int silhouettesCount;
    [Space]
    public List<SilhouetteSO> silhouettesPool;
}