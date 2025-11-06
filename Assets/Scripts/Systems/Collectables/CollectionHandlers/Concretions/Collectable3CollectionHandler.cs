using UnityEngine;

public class Collectable3CollectionHandler : CollectableCollectionHandler
{
    //This collectable checks the first time user plays a cutscene in the Album
    private void OnEnable()
    {
        AlbumSceneCutsceneUIHandler.OnCutscenePlay += AlbumSceneCutsceneUIHandler_OnCutscenePlay;
    }

    private void OnDisable()
    {
        AlbumSceneCutsceneUIHandler.OnCutscenePlay -= AlbumSceneCutsceneUIHandler_OnCutscenePlay;
    }


    private void AlbumSceneCutsceneUIHandler_OnCutscenePlay(object sender, AlbumSceneCutsceneUIHandler.OnCutsceneEventArgs e)
    {
        CollectCollectable(false);
    }
}
