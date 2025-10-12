using UnityEngine;
using UnityEngine.UI;

public class CollectableUI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private CollectableSO collectableSO;

    [Header("UI Components")]
    [SerializeField] private Button collectableButton;
    [SerializeField] private Image collectableImage;
}
