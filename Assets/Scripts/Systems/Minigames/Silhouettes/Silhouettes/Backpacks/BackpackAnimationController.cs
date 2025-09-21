using UnityEngine;

public class BackpackAnimationController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;

    private const string APPEAR_ANIMATION_NAME = "Appear";

    private const string ADD_ITEM_ANIMATION_NAME = "AddItem";
    private const string FULL_ANIMATION_NAME = "Full";

    public void PlayAppearAnimation() => animator.Play(APPEAR_ANIMATION_NAME);
    public void PlayAddItemAnimation() => animator.Play(ADD_ITEM_ANIMATION_NAME);
    public void PlayFullAnimation() => animator.Play(FULL_ANIMATION_NAME);
}
