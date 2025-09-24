using UnityEngine;

public class PasswordIndicatorsContainerHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PasswordUIHandler passwordUIHandler;
    [SerializeField] private Animator animator;

    private const string SHAKE_TRIGGER = "Shake";

    private void OnEnable()
    {
        passwordUIHandler.OnPasswordWrong += PasswordUIHandler_OnPasswordWrong;
    }

    private void OnDisable()
    {
        passwordUIHandler.OnPasswordWrong -= PasswordUIHandler_OnPasswordWrong;
    }

    private void ShakeUI()
    {
        animator.SetTrigger(SHAKE_TRIGGER);
    }

    private void PasswordUIHandler_OnPasswordWrong(object sender, System.EventArgs e)
    {
        ShakeUI();
    }
}
