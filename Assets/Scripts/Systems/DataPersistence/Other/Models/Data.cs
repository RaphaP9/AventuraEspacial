using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data : DataModel
{
    public List<int> passwordItemsIDs;

    public int timesEnteredGame;
    public List<int> cutscenesUnlockedIDs;
    public List<int> collectablesCollectedIDs;

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
        passwordItemsIDs = new List<int>();

        timesEnteredGame = 0;
        cutscenesUnlockedIDs = new List<int>();
        collectablesCollectedIDs = new List<int>();
    }

    public void InitializeMemoryMinigameData() => memoryMinigameData = new MemoryMinigameData();
    public void InitializeSilhouettesMinigameData() => silhouettesMinigameData = new SilhouettesMinigameData();
}