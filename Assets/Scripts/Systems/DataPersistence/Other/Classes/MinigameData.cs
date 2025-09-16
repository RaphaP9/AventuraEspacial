using UnityEngine;

public abstract class MinigameData 
{
    public int timesEntered;
    public int timesWon;
    public int timesLost;

    public int highscore;
    public int totalScore;

    public MinigameData()
    {
        timesEntered = 0;
        timesLost = 0;
        timesWon = 0;

        highscore = 0;
        totalScore = 0;
    }
}
