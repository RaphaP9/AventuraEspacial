using UnityEngine;

public class PasswordIndicatorsContainerHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;

    private const string SHAKE_TRIGGER = "Shake";

    public void ShakeUI()
    {
        animator.SetTrigger(SHAKE_TRIGGER);
    }
}
