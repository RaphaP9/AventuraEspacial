using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LanguageInfoSO", menuName = "ScriptableObjects/Language/LanguageInfo")]
public class LanguageInfoSO : ScriptableObject
{
    public List<LanguageAttributes> languageAttributesList;

    public LanguageAttributes GetLanguageAttributesByLanguage(Language language)
    {
        foreach (LanguageAttributes attribute in languageAttributesList)
        {
            if (attribute.language == language) return attribute;
        }

        return null;
    }
}

[System.Serializable]
public class LanguageAttributes
{
    public Language language;
    public string languageName;
    public Sprite languageFlag;
}
