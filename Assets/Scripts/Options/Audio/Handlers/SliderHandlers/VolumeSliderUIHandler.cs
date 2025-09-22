using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class VolumeSliderUIHandler : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] protected EventDetectorSlider eventDetectorSlider;

    private void OnEnable()
    {
        eventDetectorSlider.OnDragEnd += EventDetectorSlider_OnDragEnd;
    }

    private void OnDisable()
    {
        eventDetectorSlider.OnDragEnd -= EventDetectorSlider_OnDragEnd;
    }

    private void Awake()
    {
        InitializeSliderListeners();
    }

    private void InitializeSliderListeners()
    {
        eventDetectorSlider.onValueChanged.AddListener(ChangeVolume);
    }

    protected abstract VolumeManager GetVolumeManager();

    private void ChangeVolume(float sliderValue)  => GetVolumeManager().ChangeVolume(sliderValue, false); //Do not save to player prefs: onValueChange performs continuously (not ideal for saving operations)

    protected void UpdateVisual()
    {
        float currentVolume = GetVolumeManager().GetLinearVolume();
        eventDetectorSlider.SetValueWithoutNotify(currentVolume);
    }

    protected void EventDetectorSlider_OnDragEnd(object sender, EventArgs e) //Only save on handle released
    {
        Debug.Log("Saving");
        GetVolumeManager().SaveVolumePlayerPrefs(eventDetectorSlider.value);
    }
}
