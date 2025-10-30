using UnityEngine;

[System.Serializable]   
public class MinigameFinalScoreSetting
{
    [Range(0, 1000)] public int minimunScore;
    [Space]
    public Sprite sprite;
    [Space]
    public string messageLocalizationTable;
    public string messageLocalizationBinding;
}
