using UnityEngine;

public class CutsceneSceneUIHandler : MonoBehaviour
{
    public static CutsceneSceneUIHandler Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private CutsceneSO cutscene;

    [Header("UI Components")]
    [SerializeField] private Transform cutscenePanelsContainer;
    [SerializeField] private Transform cutscenePanelPrefab;

    private void Awake()
    {
        SetSingleton();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There is more than one CutsceneSceneManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    public void SkipCutscenePanel()
    {

    }
}
