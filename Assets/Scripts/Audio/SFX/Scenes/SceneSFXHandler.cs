using UnityEngine;

public abstract class SceneSFXHandler : MonoBehaviour
{
    [Header("SFX Pool")]
    [SerializeField] protected SFXPool SFXPool;

    protected virtual void OnEnable()
    {

    }

    protected virtual void OnDisable()
    {

    }
}
