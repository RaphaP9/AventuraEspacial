using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class GeneralDataManager : MonoBehaviour
{
    public static GeneralDataManager Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private ConcreteJSONDataManager JSONDataManager;

    public static event EventHandler OnDataLoadStart;
    public static event EventHandler OnDataLoadComplete;

    public static event EventHandler OnDataSaveStart;
    public static event EventHandler OnDataSaveComplete;    

    private void Awake()
    {
        SetSingleton();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #region JSON Load
    public void DataLoad()
    {
        LoadJSONData();
        InjectAllDataFromContainers();
    }

    public async Task DataLoadAsync()
    {
        await LoadJSONDataAsync();
        InjectAllDataFromContainers();
    }

    public void LoadJSONData()
    {
        OnDataLoadStart?.Invoke(this, EventArgs.Empty);

        JSONDataManager.LoadData(); //NOTE: Order is important

        OnDataLoadComplete?.Invoke(this, EventArgs.Empty);
    }
    public async Task LoadJSONDataAsync()
    {
        OnDataLoadStart?.Invoke(this, EventArgs.Empty);

        try
        {
            await JSONDataManager.LoadDataAsync();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Save failed: {ex}");
        }

        OnDataLoadComplete?.Invoke(this, EventArgs.Empty);
    }

    public void LoadJSONDataAsyncWrapper()
    {
        _ = LoadJSONDataAsync();
    }

    #endregion

    #region Data Containers Injection
    public void InjectAllDataFromContainers()
    {
        List<DataContainerInjectorExtractor> sessionDataSaveLoaders = FindObjectsByType<DataContainerInjectorExtractor>(FindObjectsSortMode.None).ToList(); //DataContainerInjectorExtractor Vary on each scene, must use FindObjectsOfType

        foreach (DataContainerInjectorExtractor sessionDataSaveLoader in sessionDataSaveLoaders)
        {
            sessionDataSaveLoader.InjectAllDataFromDataContainers();
        }
    }
    #endregion

    ////////////////////////////////////////////////////////////////////////////////////////////

    #region JSON Save
    public void DataSave()
    {
        ExtractAllDataToContainers();
        SaveJSONData();
    }

    public async Task DataSaveAsync()
    {
        ExtractAllDataToContainers();
        await SaveJSONDataAsync();
    }

    public void SaveJSONData()
    {
        OnDataSaveStart?.Invoke(this, EventArgs.Empty);

        JSONDataManager.SaveData();

        OnDataSaveComplete?.Invoke(this, EventArgs.Empty);
    }

    public async Task SaveJSONDataAsync()
    {
        OnDataSaveStart?.Invoke(this, EventArgs.Empty);

        try
        {
            await JSONDataManager.SaveDataAsync();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Save failed: {ex}");
        }

        OnDataSaveComplete?.Invoke(this, EventArgs.Empty);
    }

    public void SaveJSONDataAsyncWrapper()
    {
        _ = SaveJSONDataAsync();
    }
    #endregion

    #region Data Containers Extraction
    public void ExtractAllDataToContainers()
    {
        List<DataContainerInjectorExtractor> sessionDataSaveLoaders = FindObjectsByType<DataContainerInjectorExtractor>(FindObjectsSortMode.None).ToList();

        foreach (DataContainerInjectorExtractor sessionDataSaveLoader in sessionDataSaveLoaders)
        {
            sessionDataSaveLoader.ExtractAllDataToDataContainers();
        }
    }
    #endregion
}
