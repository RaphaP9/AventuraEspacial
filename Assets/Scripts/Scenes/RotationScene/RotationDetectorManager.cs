using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RotationDetectorManager : MonoBehaviour
{
    public static RotationDetectorManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private string nextScene;
    [SerializeField] private TransitionType nextSceneTransitionType;
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
            ForceRotationAndNextScene();
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

    private IEnumerator LoadNextSceneAfterTimeCoroutine()
    {
        yield return new WaitForSeconds(timeToLoadNextScene);
        ScenesManager.Instance.TransitionLoadTargetScene(nextScene, nextSceneTransitionType);   
    }


    private void SetBackgroundImage(Sprite sprite) => backgroundImage.sprite = sprite;
}
