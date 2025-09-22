using System.IO;
using UnityEngine;

public static class DataUtilities 
{
    private const string DATA_PATH = "data.json";

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
}
