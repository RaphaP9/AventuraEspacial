using UnityEngine;

public class TestSceneTransition : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ScenesManager.Instance.TransitionLoadTargetScene("Test", TransitionType.Slide);
        }
    }
}
