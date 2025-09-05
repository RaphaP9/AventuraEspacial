using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSFXPoolSO", menuName = "ScriptableObjects/Audio/MusicPool")]
public class MusicPoolSO : ScriptableObject
{
    [Header("Scenes")]
    public AudioClip menuMusic;
    public AudioClip creditsMusic;
}
