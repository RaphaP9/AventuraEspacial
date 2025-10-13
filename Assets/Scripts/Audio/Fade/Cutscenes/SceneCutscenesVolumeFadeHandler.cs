using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCutscenesVolumeFadeHandler : SceneVolumeFadeHandler
{
    private void Start()
    {
        SetVolumeFadeManager(CutscenesVolumeFadeManager.Instance);
    }
}

