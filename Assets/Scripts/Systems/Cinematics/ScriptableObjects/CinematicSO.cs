using UnityEngine;

[CreateAssetMenu(fileName = "NewCinematicSO", menuName = "ScriptableObjects/Cinematics/Cinematic")]
public class CinematicSO : ScriptableObject
{
    public int id;
    public string localizationTable;
    public string localizationBinding;
}
