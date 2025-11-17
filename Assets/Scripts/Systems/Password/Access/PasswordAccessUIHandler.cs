using System;
using UnityEngine;

public abstract class PasswordAccessUIHandler : MonoBehaviour
{
    public event EventHandler OnPasswordUIUnlock;

    protected void UnlockUI()
    {
        OnPasswordUIUnlock?.Invoke(this, EventArgs.Empty);
    }
}
