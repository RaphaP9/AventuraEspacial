using UnityEngine;
using System;

public class TimeSelectionUI : PopUpUI, IPasswordAccessedUI
{
    public event EventHandler OnTimeSelectionUIClosedByButtonClick;

    protected override void CloseUIByButtonClick()
    {
        base.CloseUIByButtonClick();
        OnTimeSelectionUIClosedByButtonClick?.Invoke(this, EventArgs.Empty);
    }

    public void AccessUI()
    {
        OpenUI();
    }
}
