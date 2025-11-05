using UnityEngine;

public class SceneRotationManager : MonoBehaviour
{
    public static SceneRotationManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private SceneRotation defaultSceneRotation;
    [Space]
    [SerializeField] private bool rotateToDefaultOnStart;
    [SerializeField] private bool rotateToDefaultOnQuit;

    private void Awake()
    {
        SetSingleton();
    }

    private void Start()
    {
        HandleRotationOnStart();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //Debug.LogWarning("There is more than one SceneRotationManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void RotateToDefault()
    {
        switch (defaultSceneRotation)
        {
            case SceneRotation.Portrait:
                RotateToPortrait();
                break;
            case SceneRotation.Landscape:
                RotateToLandscape();
                break;
        }
    }

    public void RotateToPortrait()
    {
        #if UNITY_EDITOR
            GameViewUtilities.SwitchToPortrait();
        #else
            Screen.orientation = ScreenOrientation.Portrait;

            Screen.autorotateToPortrait = false;
            Screen.autorotateToLandscapeRight = false;
            Screen.autorotateToLandscapeLeft = false;
            Screen.autorotateToPortraitUpsideDown = false;
        #endif
    }

    public void RotateToLandscape()
    {
        #if UNITY_EDITOR
            GameViewUtilities.SwitchToLandscape();
        #else
            Screen.orientation = ScreenOrientation.LandscapeLeft;

            Screen.autorotateToPortrait = false;
            Screen.autorotateToLandscapeRight = false;
            Screen.autorotateToLandscapeLeft = false;
            Screen.autorotateToPortraitUpsideDown = false;
        #endif
    }

    private void HandleRotationOnStart()
    {
        if (!rotateToDefaultOnStart) return;
        RotateToDefault();
    }

    private void HandleRotationOnQuit()
    {
        if (!rotateToDefaultOnQuit) return;
        RotateToDefault();
    }

    private void OnApplicationQuit()
    {
        HandleRotationOnQuit();
    }
}
