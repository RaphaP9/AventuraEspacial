using UnityEngine;
using UnityEngine.UI;

public class AnimatedToggle : Toggle
{
    //An extern manager is the one that handles animations, as it decides whether they play immediately or not

    private const string ON_TRIGGER = "On";
    private const string OFF_TRIGGER = "Off";

    private const string ON_ANIMATION_NAME = "On";
    private const string OFF_ANIMATION_NAME = "Off";

    public void TurnOnInmediately()
    {
        animator.Play(ON_ANIMATION_NAME);
    }

    public void TurnOffInmediately()
    {
        animator.Play(OFF_ANIMATION_NAME);
    }

    public void TurnOn()
    {
        animator.ResetTrigger(OFF_TRIGGER);
        animator.SetTrigger(ON_TRIGGER);
    }

    public void TurnOff()
    {
        animator.ResetTrigger(ON_TRIGGER);
        animator.SetTrigger(OFF_TRIGGER);
    }
}
