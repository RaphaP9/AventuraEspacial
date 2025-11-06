using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "SilhouetteSO", menuName = "ScriptableObjects/Minigames/Silhouettes/Silhouette")]
public class SilhouetteSO : ScriptableObject
{
    public int id;
    [ShowAssetPreview] public Sprite sprite;
    [Space]
    public Material silhouetteMaterial;
}
