using UnityEngine;
using UnityEngine.SceneManagement;

public class DisclaimerCheckManager : MonoBehaviour
{
    [Header("PlayerPrefs Settings")]
    [SerializeField] private string disclaimerAuthorizationKey;

    [Header("Disclaimer Rejected Settings")]
    [SerializeField] private string disclaimerRejectedScene;
    [SerializeField] private string disclaimerAcceptedScene;

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
        
        SceneManager.LoadScene(targetScene);
    }
}
