using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcreteJSONDataManager : JSONDataManager<Data>
{
    [Header("Perpetual Components")]
    [SerializeField] private ConcreteJSONDataContainerMiddleMan perpetualJSONDataContainerMiddleMan;

    public static ConcreteJSONDataManager Instance { get; private set; }

    protected override void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //Debug.LogWarning("There is more than one ConcreteJSONDataManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    protected override JSONDataContainerMiddleMan<Data> GetJSONDataContainerMiddleMan() => perpetualJSONDataContainerMiddleMan;
}