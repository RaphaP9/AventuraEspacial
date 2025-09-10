using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data : DataModel
{
    public int timesEnteredGame;

    public Data()
    {
        timesEnteredGame = 0;
    }

    public override void Initialize()
    {

    }
}
