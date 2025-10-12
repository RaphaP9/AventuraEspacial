using UnityEngine;

[CreateAssetMenu(fileName = "NewCollectableSO", menuName = "ScriptableObjects/Collectables/Collectable")]
public class CollectableSO : ScriptableObject
{
    public int id;
    public Sprite collectableSprite;
    public Material notCollectedMaterial;
}