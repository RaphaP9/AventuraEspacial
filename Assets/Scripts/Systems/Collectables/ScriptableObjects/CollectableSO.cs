using UnityEngine;

[CreateAssetMenu(fileName = "NewCollectableSO", menuName = "ScriptableObjects/Collectables/Collectable")]
public class CollectableSO : ScriptableObject
{
    [Header("Settings")]
    public int id;
    public Sprite collectableSprite;
    public Material notCollectedMaterial;
    [Space]
    public Color collectedBackgroundColor;
    public Color notCollectedBackgroundColor;

    [Header("Localization")]
    public string localizationTable;
    [Space]
    public string nameLocalizationBinding;
    public string descriptionLocalizationBinding;
}