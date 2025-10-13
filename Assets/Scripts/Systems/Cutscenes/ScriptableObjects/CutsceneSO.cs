using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewCutsceneSO", menuName = "ScriptableObjects/Cutscenes/Cutscene")]
public class CutsceneSO : ScriptableObject
{
    public int id;
    [Space]
    public string nameLocalizationTable;
    public string nameLocalizationBinding;
    [Space]
    public List<CutscenePanel> cutscenePanels;
    [Space]
    public AudioClip cutsceneBGM;

    public bool IsLastCutscenePanel(CutscenePanel cutscenePanel) => cutscenePanel == cutscenePanels[^1];
}
