using System.IO;
using UnityEngine;

public static class DataUtilities 
{
    private const string DATA_PATH = "data.json";

    private const int LANDMARK_NOT_UNLOCKED_INT_DATA_VALUE = 0;
    private const int LANDMARK_UNLOCKED_INT_DATA_VALUE = 1;
    private const int LANDMARK_CLAIMED_INT_DATA_VALUE = 2;

    #region Files Handling

    public static bool HasSavedData()
    {
        return CheckIfDataPathExists(DATA_PATH);
    }

    public static void WipeData()
    {
        DeleteDataInPath(DATA_PATH);
    }

    public static bool CheckIfDataPathExists(string dataPath)
    {
        string dirPath = Application.persistentDataPath;
        string path = Path.Combine(dirPath, dataPath);

        if (File.Exists(path)) return true;

        return false;
    }

    public static void DeleteDataInPath(string dataPath)
    {
        string dirPath = Application.persistentDataPath;

        string path = Path.Combine(dirPath, dataPath);

        if (!File.Exists(path))
        {
            Debug.Log("No data to delete");
        }
        else
        {
            File.Delete(path);
            Debug.Log("Data Deleted");
        }
    }
    #endregion

    #region Data Translation
    public static LandmarkState TranslateLandmarkDataIntValueToState(int intValue)
    {
        switch (intValue)
        {
            case LANDMARK_NOT_UNLOCKED_INT_DATA_VALUE:
            default:
                return LandmarkState.NotUnlocked;
            case LANDMARK_UNLOCKED_INT_DATA_VALUE:
                return LandmarkState.Unlocked;
            case LANDMARK_CLAIMED_INT_DATA_VALUE:
                return LandmarkState.Claimed;
        }
    }

    public static int TranslateLandmarkStateToDataIntValue(LandmarkState landmarkState)
    {
        switch (landmarkState)
        {
            case LandmarkState.NotUnlocked:
            default:
                return LANDMARK_NOT_UNLOCKED_INT_DATA_VALUE;
            case LandmarkState.Unlocked:
                return LANDMARK_UNLOCKED_INT_DATA_VALUE;
            case LandmarkState.Claimed:
                return LANDMARK_CLAIMED_INT_DATA_VALUE;
        }
    }
    #endregion
}
