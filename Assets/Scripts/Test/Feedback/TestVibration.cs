using UnityEngine;

public class TestVibration : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            VibrationManager.Instance.CancelVibration();
        }
    }
}
