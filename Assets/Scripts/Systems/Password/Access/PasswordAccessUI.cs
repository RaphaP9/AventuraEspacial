using System;
using UnityEngine;

public abstract class PasswordAccessUI : MonoBehaviour
{
    public event EventHandler OnPasswordUIUnlock;

    protected void UnlockUI()
    {
        OnPasswordUIUnlock?.Invoke(this, EventArgs.Empty);
    }
}
