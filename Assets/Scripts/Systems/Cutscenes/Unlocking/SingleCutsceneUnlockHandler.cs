using System;
using UnityEngine;

public class SingleCutsceneUnlockHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CutsceneSO cutsceneSO;

    protected void HandleCutsceneUnlocking()
    {
        CutsceneUnlockHandler.Instance.UnlockCutscene(cutsceneSO);
    }
}
