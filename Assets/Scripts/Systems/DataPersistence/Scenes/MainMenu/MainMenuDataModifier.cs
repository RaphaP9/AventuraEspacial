using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuDataModifier : MonoBehaviour
{
    private static bool hasEnteredGame = false;

    private void Start()
    {
        HandleEnteringGameDataModification();
    }

    private void HandleEnteringGameDataModification()
    {
        if (hasEnteredGame) return;

        hasEnteredGame = true;

        DataContainer.Instance.IncreaseTimesEnteredGame();
        GeneralDataManager.Instance.SaveJSONDataAsyncWrapper();
    }
}
