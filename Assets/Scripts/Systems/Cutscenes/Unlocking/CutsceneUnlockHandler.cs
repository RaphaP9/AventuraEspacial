using System;
using UnityEngine;

public class CutsceneUnlockHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CutsceneSO cutsceneSO;

    public static event EventHandler<OnCutsceneUnlockedEventArgs> OnCutsceneUnlocked;

    public class OnCutsceneUnlockedEventArgs : EventArgs
    {
        public CutsceneSO cutsceneSO;
    }

    protected void HandleCutsceneUnlocking()
    {
        if (DataContainer.Instance.UnlockCutscene(cutsceneSO))
        {
            GeneralDataManager.Instance.SaveJSONDataAsyncWrapper();
            OnCutsceneUnlocked?.Invoke(this, new OnCutsceneUnlockedEventArgs { cutsceneSO = cutsceneSO });
        }
    }
}
