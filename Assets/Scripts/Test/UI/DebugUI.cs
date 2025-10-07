using TMPro;
using UnityEngine;

public class DebugUI : MonoBehaviour
{
    public static DebugUI Instance { get; private set; }

    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI debugText;

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
            //Debug.LogWarning("There is more than one DebugUI instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    public void Debug(string text)
    {
        debugText.text = text;
    }
}
