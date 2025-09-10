using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ConcreteJSONDataContainerMiddleMan : JSONDataContainerMiddleMan<Data>
{
    [Header("Data Containers")]
    [SerializeField] private DataContainer dataContainer;

    public override void LoadDataToContainer(Data data)
    {
        dataContainer.SetData(data);
    }

    public override void SaveDataFromContainer(ref Data data)
    {
        data = dataContainer.Data;
    }
}
