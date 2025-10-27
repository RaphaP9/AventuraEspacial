using UnityEngine;
using System;

public class TimeSelectionUI : PopUpUI
{
    public event EventHandler OnTimeSelectionUIClosedByButtonClick;

    protected override void CloseUIByButtonClick()
    {
        base.CloseUIByButtonClick();
        OnTimeSelectionUIClosedByButtonClick?.Invoke(this, EventArgs.Empty);
    }
}
