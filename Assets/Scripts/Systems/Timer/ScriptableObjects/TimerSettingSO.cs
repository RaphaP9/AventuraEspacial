using UnityEngine;

[CreateAssetMenu(fileName = "TimerSettingSO", menuName = "ScriptableObjects/Timer/TimerSetting")]
public class TimerSettingSO : ScriptableObject
{
    [Range(0,3600)] public int time; //In Secconds
    [Space]
    public string suffixlocalizationTable;
    public string suffixLocalizationBinding;
    [Space]
    public string descriptionLocalizationTable;
    public string descriptionLocalizationBinding;
}
