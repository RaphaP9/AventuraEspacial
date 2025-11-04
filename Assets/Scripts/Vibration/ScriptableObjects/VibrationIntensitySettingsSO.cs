using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewVibrationIntensitySettingsSO", menuName = "ScriptableObjects/Vibration/VibrationIntensitySettings")]
public class VibrationIntensitySettingsSO : ScriptableObject
{
    public List<VibrationIntensityValue> vibrationIntensityValueList;

    private const int NOT_FOUND_INTENSITY_VALUE = 0;

    public int GetVibrationValueByLevel(VibrationIntensity vibrationIntensity)
    {
        foreach (VibrationIntensityValue vibrationIntensityValue in vibrationIntensityValueList)
        {
            if(vibrationIntensity == vibrationIntensityValue.vibrationIntensity) return vibrationIntensityValue.value;
        }

        return NOT_FOUND_INTENSITY_VALUE;
    }


    [System.Serializable]
    public class VibrationIntensityValue
    {
        public VibrationIntensity vibrationIntensity;
        [Range(1,255)] public int value;
    }
}

