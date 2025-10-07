using UnityEngine;

public class CutscenePanelSentenceAnimationController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;

    private const string SHOW_TRIGGER = "Show";

    public void ShowSentence()
    {
        animator.SetTrigger(SHOW_TRIGGER);
    }
}
