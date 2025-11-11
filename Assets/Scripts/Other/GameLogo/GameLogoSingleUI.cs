using UnityEngine;
using UnityEngine.UI;

public class GameLogoSingleUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private Image image;

    private const string APPEAR_ANIMATION_NAME = "Appear";

    private const string IDLE_MOVEMENT_ANIMATION_NAME = "IdleMovement";

    public void EnableImage() => image.enabled = true;
    public void DisableImage() => image.enabled = false;

    public void PlayAppearAnimation() => animator.Play(APPEAR_ANIMATION_NAME);
    public void PlayIdleMovementAnimation() => animator.Play(IDLE_MOVEMENT_ANIMATION_NAME);
}
