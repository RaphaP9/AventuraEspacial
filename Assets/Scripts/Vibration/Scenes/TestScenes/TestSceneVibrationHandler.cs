using UnityEngine;
using System.Collections.Generic;
using Lofelt.NiceVibrations;

public class TestSceneVibrationHandler : SceneVibrationHandler
{
    [Header("Settings")]
    [SerializeField] private List<IndexHapticPresetRelationship> indexHapticPresetRelationshipList;

    [System.Serializable]
    public class IndexHapticPresetRelationship
    {
        public int index;
        public HapticPatterns.PresetType presetType;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        TestSceneButtonsUI.OnTestButtonClicked += TestSceneButtonsUI_OnTestButtonClicked;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        TestSceneButtonsUI.OnTestButtonClicked -= TestSceneButtonsUI_OnTestButtonClicked;
    }

    private void TriggerHaptic(int index)
    {
        HapticPatterns.PresetType presetType = FindHapticPresetTypeByIndex(index);
        HapticManager.Instance.PlayHaptic(presetType, false);
    }

    private HapticPatterns.PresetType FindHapticPresetTypeByIndex(int index)
    {
        foreach(IndexHapticPresetRelationship relationship in indexHapticPresetRelationshipList)
        {
            if (index == relationship.index) return relationship.presetType;
        }

        return HapticPatterns.PresetType.None; //PresetType.None will be ignored by HapticPatterns.PlayPreset
    } 

    private void TestSceneButtonsUI_OnTestButtonClicked(object sender, TestSceneButtonsUI.OnTestButtonClickedEventArgs e)
    {
        TriggerHaptic(e.buttonIndexRelationship.assignedIndex);
    }
}
