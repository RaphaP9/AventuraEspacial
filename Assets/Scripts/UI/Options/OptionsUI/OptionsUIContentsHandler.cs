using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsUIContentsHandler : MonoBehaviour
{
    [Header("Contents")]
    [SerializeField] private Animator mainContentAnimator;
    [SerializeField] private Animator parentsContentAnimator;

    private const string SHOW_TRIGGER = "Show";
    private const string HIDE_TRIGGER = "Hide";

    private const string SHOWING_CONTENT_ANIMATION = "Showing";
    private const string HIDDEN_CONTENT_ANIMATION = "Hidden";

    private void Start()
    {
        ResetContents();
    }

    public void ShowMainContent()
    {
        mainContentAnimator.ResetTrigger(HIDE_TRIGGER);

        mainContentAnimator.SetTrigger(SHOW_TRIGGER);
        parentsContentAnimator.SetTrigger(HIDE_TRIGGER);
    }

    public void ShowParentsContent()
    {
        parentsContentAnimator.ResetTrigger(HIDE_TRIGGER);

        parentsContentAnimator.SetTrigger(SHOW_TRIGGER);
        mainContentAnimator.SetTrigger(HIDE_TRIGGER);
    }

    public void ResetContents()
    {
        mainContentAnimator.ResetTrigger(HIDE_TRIGGER);
        parentsContentAnimator.ResetTrigger(HIDE_TRIGGER);

        mainContentAnimator.Play(SHOWING_CONTENT_ANIMATION);
        parentsContentAnimator.Play(HIDDEN_CONTENT_ANIMATION);
    }
}
