using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataContainer : MonoBehaviour
{
    public static DataContainer Instance { get; private set; }

    [Header("Data")]
    [SerializeField] private Data data;

    public Data Data => data;

    #region Initialization & Settings
    private void Awake() //Remember this Awake Happens before all JSON awakes, initialization of container happens before JSON Load
    {
        SetSingleton();
        InitializeDataContainer();
    }

    private void SetSingleton()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void InitializeDataContainer()
    {
        data = new Data();
        data.Initialize();
    }

    public void SetData(Data perpetualData) => this.data = perpetualData;
    
    public void ResetData()
    {
        InitializeDataContainer();
    }
    #endregion

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void IncreaseTimesEnteredGame() => data.timesEnteredGame +=1;
    public bool IsFirstTimeEnteringGame() => data.timesEnteredGame == 0;

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void IncreaseTimesEnteredMinigame(Minigame minigame)
    {
        switch (minigame)
        {
            case Minigame.MemoryMinigame:
                data.memoryMinigameData.timesEntered += 1;
                break;
            case Minigame.SilhouettesMinigame:
                data.silhouettesMinigameData.timesEntered += 1;
                break;
        }
    }

    public bool IsFirstTimeEnteringMinigame(Minigame minigame)
    {
        switch (minigame)
        {
            case Minigame.MemoryMinigame:
            default:
                return data.memoryMinigameData.timesEntered == 0;
            case Minigame.SilhouettesMinigame:
                return data.silhouettesMinigameData.timesEntered == 0;
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void IncreaseTimesWonMinigame(Minigame minigame)
    {
        switch (minigame)
        {
            case Minigame.MemoryMinigame:
                data.memoryMinigameData.timesWon += 1;
                break;
            case Minigame.SilhouettesMinigame:
                data.silhouettesMinigameData.timesWon += 1;
                break;
        }
    }

    public void IncreaseTimesLostMinigame(Minigame minigame)
    {
        switch (minigame)
        {
            case Minigame.MemoryMinigame:
                data.memoryMinigameData.timesLost += 1;
                break;
            case Minigame.SilhouettesMinigame:
                data.silhouettesMinigameData.timesLost += 1;
                break;
        }
    }

    public void IncreaseTotalScoreMinigame(Minigame minigame, int score)
    {
        switch (minigame)
        {
            case Minigame.MemoryMinigame:
                data.memoryMinigameData.totalScore += score;
                break;
            case Minigame.SilhouettesMinigame:
                data.silhouettesMinigameData.totalScore += score;
                break;
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public bool HasUnlockedCinematic(CinematicSO cinematicSO)
    {
        if (data.cinematicsUnlockedIDs.Contains(cinematicSO.id)) return true;
        return false;
    }

    public bool UnlockCinematic(CinematicSO cinematicSO) // Bool = success
    {
        if(HasUnlockedCinematic(cinematicSO)) return false;

        data.cinematicsUnlockedIDs.Add(cinematicSO.id);
        return true;
    }
}
