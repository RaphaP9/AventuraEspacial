using UnityEngine;

[CreateAssetMenu(fileName = "LanguageSettingSO", menuName = "ScriptableObjects/Language/LanguageSetting")]
public class LanguageSettingSO : ScriptableObject
{
    public Language language;
    public string languageName;
    public string languageCode;
    public Sprite languageFlag;
}
