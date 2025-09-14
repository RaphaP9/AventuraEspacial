using UnityEngine;

public class OptionsPauseLinker : MonoBehaviour
{
    private void OnEnable()
    {
        OptionsUI.OnOptionsUIOpen += OptionsOpeningManager_OnOptionsOpen;
        OptionsUI.OnOptionsUIClose += OptionsOpeningManager_OnOptionsClose;
    }

    private void OnDisable()
    {
        OptionsUI.OnOptionsUIOpen -= OptionsOpeningManager_OnOptionsOpen;
        OptionsUI.OnOptionsUIClose -= OptionsOpeningManager_OnOptionsClose;
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
