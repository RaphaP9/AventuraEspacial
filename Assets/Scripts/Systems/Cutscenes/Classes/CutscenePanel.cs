using UnityEngine;

[System.Serializable]
public class CutscenePanel
{
    public Sprite panelSprite;
    [Space]
    public bool hasSentence;
    public string sentenceLocalizationTable;
    public string sentenceLocalizationBinding;
    [Space]
    public bool hasAudio;
    public string audioLocalizationTable;
    public string audioLocalizationBinding;
    [Space]
    public CutscenePanelTransition transition;
    [Space]
    [Range(0f,5f)] public float timeToSkipPanel;
    [Range(0f,5f)] public float timeToAppearSentence;
}
