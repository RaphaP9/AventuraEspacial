using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSceneDataModifier : MonoBehaviour
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
        GeneralDataManager.Instance.SaveJSONData(); //Do not use Async
    }
}
