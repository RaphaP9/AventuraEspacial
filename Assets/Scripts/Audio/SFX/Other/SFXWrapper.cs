using UnityEngine;

public class SFXWrapper : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SFXPool SFXPool;

    public void PlaySFX_ButtonA(bool pausable)
    {
        if (pausable) PausableSFXManager.Instance.PlaySound(SFXPool.genericButtonA);
        else UnpausableSFXManager.Instance.PlaySound(SFXPool.genericButtonA);
    }

    public void PlaySFX_ButtonB(bool pausable)
    {
        if (pausable) PausableSFXManager.Instance.PlaySound(SFXPool.genericButtonB);
        else UnpausableSFXManager.Instance.PlaySound(SFXPool.genericButtonB);
    }

    public void PlaySFX_ButtonC(bool pausable)
    {
        if (pausable) PausableSFXManager.Instance.PlaySound(SFXPool.genericButtonC);
        else UnpausableSFXManager.Instance.PlaySound(SFXPool.genericButtonC);
    }

    public void PlaySFX_ButtonD(bool pausable)
    {
        if (pausable) PausableSFXManager.Instance.PlaySound(SFXPool.genericButtonD);
        else UnpausableSFXManager.Instance.PlaySound(SFXPool.genericButtonD);
    }

    ///////////////////////////////////////////////////////////////////////////////////////

    public void PlaySFX_PlayButton(bool pausable)
    {
        if (pausable) PausableSFXManager.Instance.PlaySound(SFXPool.playButton);
        else UnpausableSFXManager.Instance.PlaySound(SFXPool.playButton);
    }

    public void PlaySFX_AlbumButton(bool pausable)
    {
        if (pausable) PausableSFXManager.Instance.PlaySound(SFXPool.albumButton);
        else UnpausableSFXManager.Instance.PlaySound(SFXPool.albumButton);
    }

    public void PlaySFX_CollectionButton(bool pausable)
    {
        if (pausable) PausableSFXManager.Instance.PlaySound(SFXPool.collectionButton);
        else UnpausableSFXManager.Instance.PlaySound(SFXPool.collectionButton);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////

    public void PlaySFX_AlbumPageSelected(bool pausable)
    {
        if (pausable) PausableSFXManager.Instance.PlaySound(SFXPool.albumPageSelected);
        else UnpausableSFXManager.Instance.PlaySound(SFXPool.albumPageSelected);
    }

    public void PlaySFX_AlbumCutscenePlay(bool pausable)
    {
        if (pausable) PausableSFXManager.Instance.PlaySound(SFXPool.albumCutscenePlay);
        else UnpausableSFXManager.Instance.PlaySound(SFXPool.albumCutscenePlay);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////


    public void PlaySFX_MemoryMinigameSelected(bool pausable)
    {
        if (pausable) PausableSFXManager.Instance.PlaySound(SFXPool.memoryMinigameSelected);
        else UnpausableSFXManager.Instance.PlaySound(SFXPool.memoryMinigameSelected);
    }

    public void PlaySFX_SilhouettesMinigameSelected(bool pausable)
    {
        if (pausable) PausableSFXManager.Instance.PlaySound(SFXPool.silhouettesMinigameSelected);
        else UnpausableSFXManager.Instance.PlaySound(SFXPool.silhouettesMinigameSelected);
    }

    public void PlaySFX_CutsceneUnlocked(bool pausable)
    {
        if (pausable) PausableSFXManager.Instance.PlaySound(SFXPool.cutsceneUnlocked);
        else UnpausableSFXManager.Instance.PlaySound(SFXPool.cutsceneUnlocked);
    }
}
