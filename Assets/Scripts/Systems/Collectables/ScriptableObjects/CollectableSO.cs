using UnityEngine;

[CreateAssetMenu(fileName = "NewCollectableSO", menuName = "ScriptableObjects/Collectables/Collectable")]
public class CollectableSO : ScriptableObject
{
    public int id;
    public Sprite collectableSprite;
    public Color collectedColor;
    public Color notCollectedColor;

    [Header("Strings")]
    public string localizationTable;
    [Space]
    public string nameLocalizationBinding;
    public string descriptionLocalizationBinding;
}