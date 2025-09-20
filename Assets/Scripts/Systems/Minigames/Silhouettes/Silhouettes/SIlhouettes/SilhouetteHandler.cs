using UnityEngine;

public class SilhouetteHandler : MonoBehaviour
{
    [Header("Runtime Filled")]
    [SerializeField] private SilhouetteSO silhouetteSO;

    public SilhouetteSO SilhouetteSO => silhouetteSO;

}
