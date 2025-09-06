using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TimerSelectorSettingsSO", menuName = "ScriptableObjects/Settings/TimerSelectorSettings")]
public class TimerSelectorSettingsSO : ScriptableObject
{
    public List<TimerSetting> timerSettings;
}

[System.Serializable]
public class TimerSetting
{
    public int time; //In Secconds
    [TextArea(3, 10)] public string description;
}
