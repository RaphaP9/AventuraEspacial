using UnityEngine;

public class FramerateManager : MonoBehaviour
{
    public static FramerateManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField,Range(30,120)] private int targetFramerate;

    private const int UNLIMITED_FPS_NUMBER = -1;

    private void Awake()
    {
        SetSingleton();
        SetTargetFramerate();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Debug.LogWarning("There is more than one FramerateManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void SetTargetFramerate()
    {
        #if !UNITY_EDITOR
        Application.targetFrameRate = targetFramerate;
        #else
        Application.targetFrameRate = UNLIMITED_FPS_NUMBER;
        #endif
    }
}
