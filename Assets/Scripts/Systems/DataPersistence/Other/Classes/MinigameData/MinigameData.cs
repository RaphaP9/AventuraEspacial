using UnityEngine;

public abstract class MinigameData 
{
    public int timesEntered;
    public int timesWon;
    public int timesLost;

    public int highscore;
    public int totalScore;

    public MinigameLandmarkData landmarkData1;
    public MinigameLandmarkData landmarkData2;
    public MinigameLandmarkData landmarkData3;

    public MinigameData()
    {
        timesEntered = 0;
        timesLost = 0;
        timesWon = 0;

        highscore = 0;
        totalScore = 0;

        landmarkData1 = new MinigameLandmarkData();
        landmarkData2 = new MinigameLandmarkData();
        landmarkData3 = new MinigameLandmarkData();
    }
}
