using UnityEngine;

public class UISFXHandler : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] protected SFXPool SFXPool;

    public void PlaySFX_Pausable(AudioClip[] audioclips) => PausableSFXManager.Instance.PlaySound(audioclips);
    public void PlaySFX_Unpausable(AudioClip[] audioclips) => UnpausableSFXManager.Instance.PlaySound(audioclips);
}
