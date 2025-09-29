using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data : DataModel
{
    public int timesEnteredGame;
    public List<int> cinematicsUnlockedIDs;

    public MemoryMinigameData memoryMinigameData;
    public SilhouettesMinigameData silhouettesMinigameData;

    public Data()
    {
        InitializeGeneralData();
        InitializeMemoryMinigameData();
        InitializeSilhouettesMinigameData();
    }

    public override void Initialize()
    {
        InitializeGeneralData();
        InitializeMemoryMinigameData();
        InitializeSilhouettesMinigameData();
    }

    public void InitializeGeneralData()
    {
        timesEnteredGame = 0;
        cinematicsUnlockedIDs = new List<int>();
    }

    public void InitializeMemoryMinigameData() => memoryMinigameData = new MemoryMinigameData();
    public void InitializeSilhouettesMinigameData() => silhouettesMinigameData = new SilhouettesMinigameData();
}
