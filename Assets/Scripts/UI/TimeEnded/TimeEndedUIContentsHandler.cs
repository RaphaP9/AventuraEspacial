using UnityEngine;
using UnityEngine.UI;

public class TimeEndedUIContentsHandler : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        quitButton.onClick.AddListener(Quit);
    }

    private void Quit() => ScenesManager.Instance.QuitGame();
}
