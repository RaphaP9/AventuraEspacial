using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSFXPoolSO", menuName = "ScriptableObjects/Audio/SFXPool")]
public class SFXPool : ScriptableObject
{
    [Header("General")]
    public AudioClip[] genericButtonA;
    public AudioClip[] genericButtonB;
    public AudioClip[] genericButtonC;
    public AudioClip[] genericButtonD;
    [Space]
    public AudioClip[] sliderRelease;
    public AudioClip[] toggleRelease;

    [Header("MainMenu")]
    public AudioClip[] playButton;
    public AudioClip[] albumButton;
    public AudioClip[] collectionButton;

    [Header("Collectables")]
    public AudioClip[] collectableCollected;
    public AudioClip[] collectableNotCollectedSelected;
    public AudioClip[] collectableCollectedSelected;

    [Header("Album")]
    public AudioClip[] albumPageSelected;
    public AudioClip[] albumCutscenePlay;

    [Header("Minigame Selection")]
    public AudioClip[] memoryMinigameSelected;
    public AudioClip[] silhouettesMinigameSelected;
    public AudioClip[] cutsceneUnlocked;

    [Header("General Feedback")]
    public AudioClip[] regularHit;
    public AudioClip[] maxComboHit;
    public AudioClip[] comboX2;
    public AudioClip[] comboX3;
    public AudioClip[] comboX4;
    public AudioClip[] comboX5;
    [Space]
    public AudioClip[] winMinigame;
    public AudioClip[] loseMinigame;
    public AudioClip[] timeWarning;

    [Header("Memory Minigame")]
    public AudioClip[] firstCardSelected;
    public AudioClip[] secondCardSelected;
    [Space]
    public AudioClip[] pairMatch;
    public AudioClip[] pairFail;
    [Space]
    public AudioClip[] memoryRoundBegin;
    public AudioClip[] memoryRoundCompleted;
    public AudioClip[] cardsRevealTimeEnd;

    [Header("Silhouettes Minigame")]
    public AudioClip[] figureDragStart;
    [Space]
    public AudioClip[] figureReturn;
    public AudioClip[] silhouetteMatch;
    public AudioClip[] silhouetteFail;
    [Space]
    public AudioClip[] silhouettesRoundBegin;
    public AudioClip[] silhouettesRoundCompleted;
}
