using UnityEngine;

public class PasswordEntryUI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string nextScene;
    [SerializeField] private TransitionType nextSceneTransitionType;

    public void LoadNextScene()
    {
        ScenesManager.Instance.TransitionLoadTargetScene(nextScene, nextSceneTransitionType);
    }
}