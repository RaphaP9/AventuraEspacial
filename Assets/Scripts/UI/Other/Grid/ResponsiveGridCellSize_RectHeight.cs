using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResponsiveGridCellSize_RectHeight : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, Range(0.01f,1f)] private float cellSizeFactor;

    private RectTransform rectTransform;
    private GridLayoutGroup gridLayoutGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
    }

    private void Start()
    {
        StartCoroutine(InitializationCoroutine());
    }

    private IEnumerator InitializationCoroutine()
    {
        yield return null; //Wait one Frame
        InitializeGridLayout();
    }

    private void InitializeGridLayout()
    {
        float rectHeight = rectTransform.rect.height;

        float calculatedCellSize  = cellSizeFactor * rectHeight;
        gridLayoutGroup.cellSize = new Vector2(calculatedCellSize, calculatedCellSize);
    }
}
