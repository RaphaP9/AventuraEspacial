using UnityEngine;

public class MinigameLandmarkData
{
    public int landmarkState;

    public MinigameLandmarkData()
    {
        landmarkState = 0;
    }

    ///NOTE: 
    ///NotUnlocked = 0;
    ///Unlocked = 1;
    ///Claimed = 2;
}