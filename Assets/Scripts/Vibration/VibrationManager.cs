using System;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    public static VibrationManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private VibrationIntensitySettingsSO vibrationLevelSettingsSO;
    [SerializeField, Range(50, 200)] private int vibrateOneShotDurationMs;

    #if UNITY_ANDROID && !UNITY_EDITOR
    private const string ANDROID_JAVA_CLASS_PATH = "com.unity3d.player.UnityPlayer";
    private static AndroidJavaObject unityActivity;
    private static AndroidJavaObject vibrator;
    private static int androidSDKInt;
    #endif

    private void Awake()
    {
        SetSingleton();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            #if UNITY_ANDROID && !UNITY_EDITOR
                using (var unityPlayer = new AndroidJavaClass(ANDROID_JAVA_CLASS_PATH))
                {
                    unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                    vibrator = unityActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
                }
                    androidSDKInt = new AndroidJavaClass("android.os.Build$VERSION").GetStatic<int>("SDK_INT");
            #endif
        }
        else
        {
            //Debug.LogWarning("There is more than one VibrationManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    public void VibrateOneShot(bool forceVibration, VibrationIntensity vibrationIntensity)
    {
        VibrateOneShot(forceVibration, vibrationLevelSettingsSO.GetVibrationValueByLevel(vibrationIntensity));
    }

    public void VibrateOneShot(bool forceVibration, int intensityValue)
    {
        Vibrate(vibrateOneShotDurationMs, forceVibration, intensityValue);
    }

    public void Vibrate(int durationMs, bool forceVibration, VibrationIntensity vibrationIntensity)
    {
        Vibrate(durationMs, forceVibration, vibrationLevelSettingsSO.GetVibrationValueByLevel(vibrationIntensity));
    }

    public void Vibrate(int durationMs, bool forceVibration, int vibrationIntensity)
    {
        if (!forceVibration && !VibrationStateManager.Instance.VibrationEnabled) return;
        if (!HasVibrator()) return;

        #if UNITY_ANDROID && !UNITY_EDITOR

        if (vibrator == null) return;

        if (androidSDKInt >= 26)
            {
                // API 26+ supports amplitude (1–255)
                using (var vibrationEffectClass = new AndroidJavaClass("android.os.VibrationEffect"))
                {
                    // Clamp intensity for safety reasons
                    int amplitude = Mathf.Clamp(vibrationIntensity, 1, 255);
                    var vibrationEffect = vibrationEffectClass.CallStatic<AndroidJavaObject>("createOneShot", (long)durationMs, amplitude);
                    vibrator.Call("vibrate", vibrationEffect);
                }
            }
            else
            {
                // Legacy method: amplitude not supported
                vibrator.Call("vibrate", durationMs);
            }
        #elif UNITY_IOS && !UNITY_EDITOR
            Handheld.Vibrate();
        #else
            Debug.Log($"Vibrate {durationMs}ms called, {vibrationIntensity} intensity - Editor mode, no effect");
        #endif
    }

    private bool HasVibrator()
{
        #if UNITY_ANDROID && !UNITY_EDITOR
        return vibrator?.Call<bool>("hasVibrator") ?? false;
        #else
        return false;
        #endif
    }

    public void CancelVibration()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
        vibrator?.Call("cancel");
        #else
        Debug.Log("Cancel vibration called - Editor mode, no effect");
        #endif
    }
}
