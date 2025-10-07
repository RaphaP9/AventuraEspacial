using UnityEngine;
using UnityEngine.UI;

public class CutscenePanelUIHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Image panelImage;

    public void SetPanel(CutscenePanel cutscenePanel)
    {
        panelImage.sprite = cutscenePanel.panelSprite;
    }
}
