using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "MemoryCardSO", menuName = "ScriptableObjects/Minigames/Memory/MemoryCard")]
public class MemoryCardSO : ScriptableObject
{
    public int id;
    [ShowAssetPreview] public Sprite sprite;
}
