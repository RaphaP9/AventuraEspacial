using UnityEngine;

[System.Serializable]   
public class MinigameFinalScoreSetting
{
    [Range(0, 1000)] public int minimunScore;
    public Sprite sprite;
    public string messageLocalizationBinding;
}
