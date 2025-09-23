using System;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    public static VibrationManager Instance { get; private set; }

    #if UNITY_ANDROID && !UNITY_EDITOR
    private const string ANDROID_JAVA_CLASS_PATH = "com.unity3d.player.UnityPlayer";
    private static AndroidJavaObject unityActivity;
    private static AndroidJavaObject vibrator;
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
        #endif
        }
        else
        {
            //Debug.LogWarning("There is more than one AudioManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    public void Vibrate(bool forceVibration)
    {
        if (!forceVibration && !VibrationStateManager.Instance.VibrationEnabled) return;

        #if UNITY_ANDROID && !UNITY_EDITOR
        vibrator?.Call("vibrate", 100);
        #elif UNITY_IOS && !UNITY_EDITOR
        Handheld.Vibrate();
        #else
        Debug.Log("Vibrate called - Editor mode, no effect");
        #endif
    }

    public void Vibrate(long milliseconds, bool forceVibration)
    {
        if (!forceVibration && !VibrationStateManager.Instance.VibrationEnabled) return;

        #if UNITY_ANDROID && !UNITY_EDITOR
        vibrator?.Call("vibrate", milliseconds);
        #elif UNITY_IOS && !UNITY_EDITOR
        Handheld.Vibrate();
        #else
        Debug.Log($"Vibrate {milliseconds}ms called - Editor mode, no effect");
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
