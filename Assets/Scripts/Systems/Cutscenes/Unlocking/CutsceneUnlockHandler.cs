using System;
using UnityEngine;

public class CutsceneUnlockHandler : MonoBehaviour
{
    public static CutsceneUnlockHandler Instance { get; private set; }

    public static event EventHandler<OnCutsceneUnlockEventArgs> OnCutsceneUnlock;

    public class OnCutsceneUnlockEventArgs : EventArgs
    {
        public CutsceneSO cutsceneSO;
    }

    private void Awake()
    {
        SetSingleton();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //Debug.LogWarning("There is more than one CutsceneUnlockHandler instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    public void UnlockCutscene(CutsceneSO cutsceneSO)
    {
        if (DataContainer.Instance.UnlockCutscene(cutsceneSO))
        {
            GeneralDataManager.Instance.SaveJSONData();
            OnCutsceneUnlock?.Invoke(this, new OnCutsceneUnlockEventArgs { cutsceneSO = cutsceneSO });
        }
    }
}
