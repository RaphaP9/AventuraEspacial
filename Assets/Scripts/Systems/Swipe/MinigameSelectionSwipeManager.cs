using UnityEngine;

public class MinigameSelectionSwipeManager : SwipeManager
{
    private bool optionsOpen = false;

    private void OnEnable()
    {
        OptionsUI.OnOptionsUIOpen += OptionsUI_OnOptionsUIOpen;
        OptionsUI.OnOptionsUIClose += OptionsUI_OnOptionsUIClose;
    }

    private void OnDisable()
    {
        OptionsUI.OnOptionsUIOpen -= OptionsUI_OnOptionsUIOpen;
        OptionsUI.OnOptionsUIClose -= OptionsUI_OnOptionsUIClose;
    }

    protected override bool CanSwipe() => !optionsOpen;

    #region Subscriptions
    private void OptionsUI_OnOptionsUIOpen(object sender, System.EventArgs e)
    {
        optionsOpen = true;
    }

    private void OptionsUI_OnOptionsUIClose(object sender, System.EventArgs e)
    {
        optionsOpen = false;
    }
    #endregion
}
