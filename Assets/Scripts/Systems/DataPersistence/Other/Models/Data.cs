using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data : DataModel
{
    public int timesEnteredGame;
    public MemoryMinigameData memoryMinigameData;
    public SilhouettesMinigameData silhouettesMinigameData;

    public Data()
    {
        InitializeGeneralData();
        InitializeMemoryMinigameData();
    }

    public override void Initialize()
    {
        InitializeMemoryMinigameData();
    }

    public void InitializeGeneralData()
    {
        timesEnteredGame = 0;
    }

    public void InitializeMemoryMinigameData() => memoryMinigameData = new MemoryMinigameData();
    public void InitializeSilhouettesMinigameData() => silhouettesMinigameData = new SilhouettesMinigameData();
}
