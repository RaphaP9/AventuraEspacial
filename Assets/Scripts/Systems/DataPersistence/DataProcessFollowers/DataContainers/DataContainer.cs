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
}
