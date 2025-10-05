using UnityEngine;

[CreateAssetMenu(fileName = "NewCinematicSO", menuName = "ScriptableObjects/Cinematics/Cinematic")]
public class CinematicSO : ScriptableObject
{
    public int id;
    [Space]
    public string nameLocalizationTable;
    public string nameLocalizationBinding;
    [Space]
    public string localizationTable;
    public string localizationBinding;
}
