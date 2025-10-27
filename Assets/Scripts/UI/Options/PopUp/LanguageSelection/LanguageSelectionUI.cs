using UnityEngine;
using System;

public class LanguageSelectionUI : PopUpUI
{
    public event EventHandler OnLanguageSelectionUIClosedByButtonClick;

    protected override void CloseUIByButtonClick()
    {
        base.CloseUIByButtonClick();
        OnLanguageSelectionUIClosedByButtonClick?.Invoke(this, EventArgs.Empty);
    }
}
