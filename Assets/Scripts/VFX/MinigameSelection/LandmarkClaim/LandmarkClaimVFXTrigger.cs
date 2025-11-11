using UnityEngine;

public class LandmarkClaimVFXTrigger : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private LandmarkUI landmarkUI;
    [SerializeField] private Transform VFXContainer;
    [SerializeField] private Transform landmarkClaimVFXPrefab;

    private void OnEnable()
    {
        landmarkUI.OnLandmarkClaimed += LandmarkUI_OnLandmarkClaimed;
    }

    private void OnDisable()
    {
        landmarkUI.OnLandmarkClaimed -= LandmarkUI_OnLandmarkClaimed;
    }

    private void CreateVFX()
    {
        Transform prefabTransform = Instantiate(landmarkClaimVFXPrefab, VFXContainer);
    }

    private void LandmarkUI_OnLandmarkClaimed(object sender, System.EventArgs e)
    {
        CreateVFX();
    }
}
