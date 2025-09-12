using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public abstract class DisplacementArrow : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private Button arrowButton;
    [Space]
    [SerializeField] protected SnappingScrollMenuUI snappingScrollMenuUI;

    private const string HIDE_TRIGGER = "Hide";
    private const string SHOW_TRIGGER = "Show";

    private const string HIDDEN_ANIMATION_NAME = "Hidden";
    private const string SHOWING_ANIMATION_NAME = "Showing";

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        arrowButton.onClick.AddListener(ArrowDisplacement);
    }

    protected abstract void ArrowDisplacement();

    public void ShowUI()
    {
        animator.ResetTrigger(HIDE_TRIGGER);
        animator.SetTrigger(SHOW_TRIGGER);
    }

    public void HideUI()
    {
        animator.ResetTrigger(SHOW_TRIGGER);
        animator.SetTrigger(HIDE_TRIGGER);
    }

    public void ShowUIImmediately()
    {
        animator.ResetTrigger(HIDE_TRIGGER);
        animator.ResetTrigger(SHOW_TRIGGER);

        animator.Play(SHOWING_ANIMATION_NAME);
    }

    public void HideUIImmediately()
    {
        animator.ResetTrigger(HIDE_TRIGGER);
        animator.ResetTrigger(SHOW_TRIGGER);

        animator.Play(HIDDEN_ANIMATION_NAME);
    }
}
