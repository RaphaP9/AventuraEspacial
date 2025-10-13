using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutscenesVolumeFadeManager : VolumeFadeManager
{
    public static CutscenesVolumeFadeManager Instance { get; private set; }

    private const string CUTSCENES_FADE_VOLUME = "CutscenesFadeVolume";

    protected override void Awake()
    {
        base.Awake();
        SetFadeVolumeKey(CUTSCENES_FADE_VOLUME);
    }

    protected override void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //Debug.LogWarning("There is more than one CutscenesVolumeFadeManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }
}

