using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCheckerManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string unconfiguredGameScene;
    [SerializeField] private string regularSceneScene;

    [Header("Other Settings")]
    [SerializeField, Range(0f,1f)] private float timeToLoadNextScene;

    private void Start()
    {
        PerformSceneCheck();
    }

    private void PerformSceneCheck()
    {
        string targetScene = regularSceneScene;

        if (!DataContainer.Instance.HasConfiguredGame())
        {
            targetScene = unconfiguredGameScene;
        }

        StartCoroutine(LoadNextSceneAfterTime(targetScene));
    }

    private IEnumerator LoadNextSceneAfterTime(string targetScene)
    {
        yield return new WaitForSeconds(timeToLoadNextScene);
        SceneManager.LoadScene(targetScene);
    }
}
