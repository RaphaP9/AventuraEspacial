using UnityEngine;

public class LeftDisplacementArrow : DisplacementArrow
{
    private void OnEnable()
    {
        SnappingScrollMenuUI.OnFirstItemReachedStart += SnappingScrollMenuUI_OnFirstItemReachedStart;
        SnappingScrollMenuUI.OnFirstItemReached += SnappingScrollMenuUI_OnFirstItemReached;
        SnappingScrollMenuUI.OnFirstItemAway += SnappingScrollMenuUI_OnFirstItemAway;
    }

    private void OnDisable()
    {
        SnappingScrollMenuUI.OnFirstItemReachedStart -= SnappingScrollMenuUI_OnFirstItemReachedStart;
        SnappingScrollMenuUI.OnFirstItemReached -= SnappingScrollMenuUI_OnFirstItemReached;
        SnappingScrollMenuUI.OnFirstItemAway -= SnappingScrollMenuUI_OnFirstItemAway;
    }

    protected override void ArrowDisplacement() => snappingScrollMenuUI.DisplaceLeftCommand();

    private void SnappingScrollMenuUI_OnFirstItemReachedStart(object sender, System.EventArgs e)
    {
        HideUIImmediately();
    }

    private void SnappingScrollMenuUI_OnFirstItemReached(object sender, System.EventArgs e)
    {
        HideUI();
    }

    private void SnappingScrollMenuUI_OnFirstItemAway(object sender, System.EventArgs e)
    {
        ShowUI();
    }
}
