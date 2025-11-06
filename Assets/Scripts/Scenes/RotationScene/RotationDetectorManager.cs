using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RotationDetectorManager : MonoBehaviour
{
    public static RotationDetectorManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private string regularStartingScene;
    [SerializeField] private TransitionType regularStartingSceneTransitionType;
    [Space]
    [SerializeField] private string firstSessionStartingScene;
    [SerializeField] private TransitionType firstSessionStartingSceneTransitionType;
    [Space]
    [SerializeField] private float timeToLoadNextScene;
    [Space]
    [SerializeField] private bool forceLandscapeAfterRotation;

    [Header("UI Settings")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Sprite landscapeSprite;

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
            //ForceRotationAndNextScene();
            EmergencyMethod();
        }
    }

    public void ForceRotationAndNextScene()
    {
        if (hasRotated) return;

        if (forceLandscapeAfterRotation)
        {
            SetBackgroundImage(landscapeSprite);
            SceneRotationManager.Instance.RotateToLandscape();
        }

        hasRotated = true;

        StartCoroutine(LoadNextSceneAfterTimeCoroutine());
    }

    public void EmergencyMethod()
    {
        if (hasRotated) return;

        if (forceLandscapeAfterRotation)
        {
            SetBackgroundImage(landscapeSprite);
            Screen.autorotateToPortrait = false;
            Screen.autorotateToLandscapeRight = false;
            Screen.autorotateToLandscapeLeft = false;
            Screen.autorotateToPortraitUpsideDown = false;

            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }

        hasRotated = true;

        StartCoroutine(LoadNextSceneAfterTimeCoroutine());
    }

    private IEnumerator LoadNextSceneAfterTimeCoroutine()
    {
        yield return new WaitForSeconds(timeToLoadNextScene);
        TransitionToStartingScene();
    }

    #region Starting Scene
    public bool ShouldTransitionToFirstSessionStartingScene() => DataContainer.Instance.Data.timesEnteredGame <= 0;

    public void TransitionToStartingScene()
    {
        if (ShouldTransitionToFirstSessionStartingScene()) ScenesManager.Instance.TransitionLoadTargetScene(firstSessionStartingScene, firstSessionStartingSceneTransitionType);
        else ScenesManager.Instance.TransitionLoadTargetScene(regularStartingScene, regularStartingSceneTransitionType);
    }
    #endregion

    private void SetBackgroundImage(Sprite sprite) => backgroundImage.sprite = sprite;
}
