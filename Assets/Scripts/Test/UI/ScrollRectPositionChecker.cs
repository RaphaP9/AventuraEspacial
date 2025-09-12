using UnityEngine;

public class ScrollRectPositionChecker : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform content;

    [SerializeField] private Vector3 pos;
    [SerializeField ] private Vector3 relativePos;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        pos = rectTransform.anchoredPosition;
        relativePos = UIUtilities.GetCanvasPosition(rectTransform, canvas);
    }
}
