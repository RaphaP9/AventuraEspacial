using UnityEngine;

public class SceneRotationManager : MonoBehaviour
{
    public static SceneRotationManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private SceneRotation defaultSceneRotation;
    [Space]
    [SerializeField] private bool rotateToDefaultOnAwake;
    [SerializeField] private bool rotateToDefaultOnQuit;

    private void Awake()
    {
        SetSingleton();
        HandleRotationOnAwake();
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

    public void EnableAutorotation()
    {
        #if UNITY_EDITOR
            //Do nothing
        #else
            Screen.autorotateToPortrait = true;
            Screen.autorotateToLandscapeRight = true;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToPortraitUpsideDown = true;
        #endif
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

    private void HandleRotationOnAwake()
    {
        if (!rotateToDefaultOnAwake) return;
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
