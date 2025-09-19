using UnityEngine;

[CreateAssetMenu(fileName = "SilhouetteSO", menuName = "ScriptableObjects/Minigames/Silhouettes/Silhouette")]
public class SilhouetteSO : ScriptableObject
{
    public int id;
    public Sprite sprite;
    [Space]
    public Material silhouetteMaterial;
}
