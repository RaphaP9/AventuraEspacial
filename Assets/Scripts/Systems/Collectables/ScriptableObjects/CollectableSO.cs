using UnityEngine;

[CreateAssetMenu(fileName = "NewCollectableSO", menuName = "ScriptableObjects/Collectables/Collectable")]
public class CollectableSO : ScriptableObject
{
    public int id;
    public Sprite collectableSprite;
    public Material notCollectedMaterial;

    [Header("Strings")]
    public string localizationTable;
    [Space]
    public string nameLocalizationBinding;
    public string descriptionLocalizationBinding;
}