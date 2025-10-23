using System;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneButtonUI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private CutsceneSO cutsceneSO;

    [Header("UI Components")]
    [SerializeField] private Button cutsceneButton;
    [SerializeField] private Image cutsceneThumbnailImage;
    [SerializeField] private Transform notUnlockedCover;

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void Start()
    {
        SetCutsceneThumbnail();
        HandleNotUnlockedCover();
    }
    private void InitializeButtonsListeners()
    {
        cutsceneButton.onClick.AddListener(PlayCutscene);
    }

    private void SetCutsceneThumbnail()
    {
        cutsceneThumbnailImage.sprite = cutsceneSO.cutsceneThumbnail;
    }

    private void HandleNotUnlockedCover()
    {
        bool unlocked = DataContainer.Instance.HasUnlockedCutscene(cutsceneSO);

        if (unlocked) notUnlockedCover.gameObject.SetActive(false);
        else notUnlockedCover.gameObject.SetActive(true);
    }

    private void PlayCutscene() => AlbumSceneCutsceneUI.Instance.PlayCutscene(cutsceneSO);
}
