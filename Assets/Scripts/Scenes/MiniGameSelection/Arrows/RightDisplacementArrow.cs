using UnityEngine;

public class RightDisplacementArrow : DisplacementArrow
{
    private void OnEnable()
    {
        SnappingScrollMenuUI.OnLastItemReachedStart += SnappingScrollMenuUI_OnLastItemReachedStart;
        SnappingScrollMenuUI.OnLastItemReached += SnappingScrollMenuUI_OnLastItemReached;
        SnappingScrollMenuUI.OnLastItemAway += SnappingScrollMenuUI_OnLastItemAway;
    }

    private void OnDisable()
    {
        SnappingScrollMenuUI.OnLastItemReachedStart -= SnappingScrollMenuUI_OnLastItemReachedStart;
        SnappingScrollMenuUI.OnLastItemReached -= SnappingScrollMenuUI_OnLastItemReached;
        SnappingScrollMenuUI.OnLastItemAway -= SnappingScrollMenuUI_OnLastItemAway;
    }

    protected override void ArrowDisplacement() => snappingScrollMenuUI.DisplaceRightCommand();

    private void SnappingScrollMenuUI_OnLastItemReachedStart(object sender, System.EventArgs e)
    {
        HideUIImmediately();
    }

    private void SnappingScrollMenuUI_OnLastItemReached(object sender, System.EventArgs e)
    {
        HideUI();
    }

    private void SnappingScrollMenuUI_OnLastItemAway(object sender, System.EventArgs e)
    {
        ShowUI();
    }
}
