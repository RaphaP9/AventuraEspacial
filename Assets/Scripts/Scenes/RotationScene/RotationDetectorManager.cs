using System.Collections;
using UnityEngine;

public class RotationDetectorManager : MonoBehaviour
{
    public static RotationDetectorManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private string nextScene;
    [SerializeField] private TransitionType nextSceneTransitionType;
    [SerializeField] private float timeToLoadNextScene;

    [Header("Runtime Filled")]
    [SerializeField] private bool hasRotated;

    private void Awake()
    {
        SetSingleton();
    }

    private void Update()
    {
        HandleRotationDetection();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //Debug.LogWarning("There is more than one RotationDetectorManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void HandleRotationDetection()
    {
        if (hasRotated) return;

        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
        {
            ForceRotationAndNextScene();
        }
    }

    public void ForceRotationAndNextScene()
    {
        ForceLandscapeLeft();
        hasRotated = true;
        StartCoroutine(LoadNextSceneAfterTimeCoroutine());
    }

    private void ForceLandscapeLeft()
    {
        #if UNITY_EDITOR
            GameViewUtilities.SwitchToLandscape();
        #else
            Screen.autorotateToPortrait = false;
            Screen.autorotateToLandscapeRight = false;
            Screen.autorotateToLandscapeLeft = false;
            Screen.autorotateToPortraitUpsideDown = false;

            Screen.orientation = ScreenOrientation.LandscapeLeft;
        #endif
    }

    private IEnumerator LoadNextSceneAfterTimeCoroutine()
    {
        yield return new WaitForSeconds(timeToLoadNextScene);
        ScenesManager.Instance.TransitionLoadTargetScene(nextScene, nextSceneTransitionType);   
    }
}
