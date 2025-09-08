using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisclaimerCheckManager : MonoBehaviour
{
    [Header("PlayerPrefs Settings")]
    [SerializeField] private string disclaimerAuthorizationKey;

    [Header("Disclaimer Rejected Settings")]
    [SerializeField] private string disclaimerRejectedScene;
    [SerializeField] private string disclaimerAcceptedScene;

    [Header("Other Settings")]
    [SerializeField, Range(0f,1f)] private float timeToLoadNextScene;

    private void Start()
    {
        CheckDisclaimerAccepted();
    }

    private void CheckDisclaimerAccepted()
    {
        string targetScene = disclaimerRejectedScene;

        if (PlayerPrefs.GetInt(disclaimerAuthorizationKey, 0) == 1)
        {
            targetScene = disclaimerAcceptedScene;
        }

        StartCoroutine(LoadNextSceneAfterTime(targetScene));
    }

    private IEnumerator LoadNextSceneAfterTime(string targetScene)
    {
        yield return new WaitForSeconds(timeToLoadNextScene);
        SceneManager.LoadScene(targetScene);
    }
}
