using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data : DataModel
{
    #region General Data
    public int timesEnteredGame;
    #endregion

    #region Memory Minigame Data
    public int memoryMinigame_timesEntered;
    public int memoryMinigame_timesWon;
    public int memoryMinigame_timesLost;

    public int memoryMinigame_highscore;
    public int memoryMinigame_totalScore;
    #endregion

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

    public void InitializeMemoryMinigameData()
    {
        memoryMinigame_timesEntered = 0;
        memoryMinigame_timesLost = 0;
        memoryMinigame_timesWon = 0;

        memoryMinigame_highscore = 0;
        memoryMinigame_totalScore = 0;
    }
}
