using System;
using UnityEngine;

public abstract class CinematicUnlockHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CinematicSO cinematicSO;

    public static event EventHandler<OnCinematicUnlockedEventArgs> OnCinematicUnlocked;

    public class OnCinematicUnlockedEventArgs : EventArgs
    {
        public CinematicSO cinematicSO; 
    }

    protected void HandleCinematicUnlocking()
    {
        if (DataContainer.Instance.UnlockCinematic(cinematicSO))
        {
            GeneralDataManager.Instance.SaveJSONDataAsyncWrapper();
            OnCinematicUnlocked?.Invoke(this, new OnCinematicUnlockedEventArgs { cinematicSO = cinematicSO });
        }
    }
}
