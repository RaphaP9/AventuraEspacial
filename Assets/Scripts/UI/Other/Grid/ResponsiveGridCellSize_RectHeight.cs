using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResponsiveGridCellSize_RectHeight : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private RectDimensionsChangeDetector rectDimensionsChangeDetector;

    [Header("Settings")]
    [SerializeField, Range(0.01f,1f)] private float cellSizeFactor;

    private RectTransform rectTransform;
    private GridLayoutGroup gridLayoutGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
    }

    private void OnEnable()
    {
        rectDimensionsChangeDetector.OnRectDimensionsChanged += RectDimensionsChangeDetector_OnRectDimensionsChanged;
    }

    private void OnDisable()
    {
        rectDimensionsChangeDetector.OnRectDimensionsChanged -= RectDimensionsChangeDetector_OnRectDimensionsChanged;
    }

    private void Start()
    {
        InitializeGridLayout();
    }

    private void InitializeGridLayout()
    {
        float rectHeight = rectTransform.rect.height;

        float calculatedCellSize  = cellSizeFactor * rectHeight;
        gridLayoutGroup.cellSize = new Vector2(calculatedCellSize, calculatedCellSize);
    }

    #region Subscriptions
    private void RectDimensionsChangeDetector_OnRectDimensionsChanged(object sender, System.EventArgs e)
    {
        InitializeGridLayout();
    }
    #endregion
}
