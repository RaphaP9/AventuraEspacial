using UnityEngine;

public static class MappingUtilities 
{
    private const int CONSTANT_60 = 60;

    public static string MapTime(float timeSeconds)
    {
        int timeSecondsRounded = Mathf.CeilToInt(timeSeconds);
        string mappedTime;

        if (timeSecondsRounded < CONSTANT_60)
        {
            mappedTime = $"{timeSecondsRounded} segundos";
        }
        else
        {
            int timeMinutes = timeSecondsRounded / 60;
            mappedTime = timeMinutes > 1 ? $"{timeMinutes} minutos" : $"{timeMinutes} minuto";
        }

        return mappedTime;
    }
}
