using UnityEngine;
using UnityEngine.Localization.Settings;

public static class FormattingUtilities 
{
    private const int CONSTANT_60 = 60;

    private const string LOCALIZATION_TABLE_NAME = "General";

    private const string LOCALIZATION_SECOND_BINDING_NAME = "general.second";
    private const string LOCALIZATION_SECONDS_BINDING_NAME = "general.seconds";
    private const string LOCALIZATION_MINUTE_BINDING_NAME = "general.minute";
    private const string LOCALIZATION_MINUTES_BINDING_NAME = "general.minutes";

    public static string FormatTime(float timeSeconds)
    {
        int timeSecondsRounded = Mathf.CeilToInt(timeSeconds);

        string localizationTable = LOCALIZATION_TABLE_NAME;

        string localizationBinding;
        string stringNumber;

        if (timeSecondsRounded < CONSTANT_60)
        {
            stringNumber = timeSecondsRounded.ToString();
            localizationBinding = timeSecondsRounded > 1 ? LOCALIZATION_SECONDS_BINDING_NAME : LOCALIZATION_SECOND_BINDING_NAME;
        }
        else
        {
            int timeMinutes = timeSecondsRounded / 60;
            stringNumber = timeMinutes.ToString();
            localizationBinding = timeMinutes > 1 ? LOCALIZATION_MINUTES_BINDING_NAME : LOCALIZATION_MINUTE_BINDING_NAME;
        }

        string localizedText = LocalizationSettings.StringDatabase.GetLocalizedString(localizationTable, localizationBinding);
        string mappedTime = $"{stringNumber} {localizedText}";

        return mappedTime;
    }
}
