using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ConcreteJSONDataManager))]

public class ConcreteJSONDataManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ConcreteJSONDataManager concreteJSONDataManager = (ConcreteJSONDataManager)target;

        if (GUILayout.Button("Delete Data File"))
        {
            concreteJSONDataManager.DeleteGameData();
        }
    }

}
