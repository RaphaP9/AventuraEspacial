using UnityEngine;

public class OptionsPauseLinker : MonoBehaviour
{
    private void OnEnable()
    {
        OptionsOpeningManager.OnOptionsOpen += OptionsOpeningManager_OnOptionsOpen;
        OptionsOpeningManager.OnOptionsClose += OptionsOpeningManager_OnOptionsClose;
    }

    private void OnDisable()
    {
        OptionsOpeningManager.OnOptionsOpen -= OptionsOpeningManager_OnOptionsOpen;
        OptionsOpeningManager.OnOptionsClose -= OptionsOpeningManager_OnOptionsClose;
    }

    private void OptionsOpeningManager_OnOptionsOpen(object sender, System.EventArgs e)
    {
        PauseManager.Instance.PauseGame();
    }

    private void OptionsOpeningManager_OnOptionsClose(object sender, System.EventArgs e)
    {
        PauseManager.Instance.ResumeGame();
    }
}
