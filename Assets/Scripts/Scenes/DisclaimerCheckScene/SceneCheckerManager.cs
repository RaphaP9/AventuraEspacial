using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCheckerManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string firstTimeEnteringGameScene;
    [SerializeField] private string regularSceneScene;

    [Header("Other Settings")]
    [SerializeField, Range(0f,1f)] private float timeToLoadNextScene;

    private void Start()
    {
        CheckFirstTimeEnteringGame();
    }

    private void CheckFirstTimeEnteringGame()
    {
        string targetScene = regularSceneScene;

        if (DataContainer.Instance.IsFirstTimeEnteringGame())
        {
            targetScene = firstTimeEnteringGameScene;
        }

        StartCoroutine(LoadNextSceneAfterTime(targetScene));
    }

    private IEnumerator LoadNextSceneAfterTime(string targetScene)
    {
        yield return new WaitForSeconds(timeToLoadNextScene);
        SceneManager.LoadScene(targetScene);
    }
}
