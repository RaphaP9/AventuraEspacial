using UnityEngine;

public class SilhouetteMatchVFXTrigger : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform VFXContainer;
    [SerializeField] private FigureHandler figureHandler;
    [SerializeField] private Transform silhouetteMatchVFXHandler;

    private void OnEnable()
    {
        figureHandler.OnFigureMatch += FigureHandler_OnFigureMatch;
    }

    private void OnDisable()
    {
        figureHandler.OnFigureMatch -= FigureHandler_OnFigureMatch;
    }

    private void CreateVFX()
    {
        Transform prefabTransform = Instantiate(silhouetteMatchVFXHandler, VFXContainer);
    }

    private void FigureHandler_OnFigureMatch(object sender, System.EventArgs e)
    {
        CreateVFX();
    }
}
