using System;
using UnityEngine;
using UnityEngine.Localization.Settings;

public static class FormattingUtilities 
{
    private const int CONSTANT_60 = 60;

    public static string FormatTimeByTimerSettingSO(TimerSettingSO timerSettingSO)
    {
        int timeSecondsRounded = Mathf.CeilToInt(timerSettingSO.time);
        string localizationTable = timerSettingSO.suffixlocalizationTable;

        string stringNumber;

        if (timeSecondsRounded < CONSTANT_60)
        {
            stringNumber = timeSecondsRounded.ToString();
        }
        else
        {
            int timeMinutes = timeSecondsRounded / 60;
            stringNumber = timeMinutes.ToString();
        }

        string localizedSuffix = LocalizationSettings.StringDatabase.GetLocalizedString(localizationTable, timerSettingSO.suffixLocalizationBinding);
        string formattedTime = $"{stringNumber} {localizedSuffix}";

        return formattedTime;
    }

    public static string FormatTime(int totalSeconds)
    {
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public static string GetCurrentTime24HS() => DateTime.Now.ToString("HH:mm:ss");
    public static string GetCurrentTime12HS() => DateTime.Now.ToString("hh:mm:ss tt");

    public static int TranstaleBoolToInt(bool value)
    {
        if (value) return 1;
        return 0;
    }

    public static bool TranslateIntToBool(int value)
    {
        if(value > 0) return true;
        return false;
    }
}
