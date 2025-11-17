using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class SpaceshipPropulsionVFXHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private VisualEffect visualEffect;

    [Header("Settings")]
    [SerializeField, Range(0.1f, 5f)] private float VFXActiveTime;

    private void OnEnable()
    {
        MinigameManager.OnRoundEnd += MinigameManager_OnRoundEnd;
    }

    private void OnDisable()
    {
        MinigameManager.OnRoundEnd -= MinigameManager_OnRoundEnd;
    }

    private void Start()
    {
        StopVFX();
    }

    private void PlayVFX() => visualEffect.Play();
    private void StopVFX() => visualEffect.Stop();

    private IEnumerator PlayVFXCoroutine()
    {
        PlayVFX();

        yield return new WaitForSeconds(VFXActiveTime);

        StopVFX();
    }

    private void MinigameManager_OnRoundEnd(object sender, MinigameManager.OnRoundEventArgs e)
    {
        StopAllCoroutines();
        StartCoroutine(PlayVFXCoroutine());
    }
}
