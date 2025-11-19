using JetBrains.Annotations;
using System.Collections;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;

public class ResponsiveWidthElementGridLayoutGroup : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private RectDimensionsChangeDetector rectDimensionsChangeDetector;

    [Header("Settings")]
    [SerializeField] private bool useFixedColummCount;
    [SerializeField] private int columnCount;

    [SerializeField] private bool useSquareCells = true;
    [SerializeField] private float minCellWidth = 100;
    [SerializeField] private float maxCellWidth = 300;

    private RectTransform rectTransform;
    private GridLayoutGroup gridLayoutGroup;

    private void OnEnable()
    {
        rectDimensionsChangeDetector.OnRectDimensionsChanged += RectDimensionsChangeDetector_OnRectDimensionsChanged;
    }

    private void OnDisable()
    {
        rectDimensionsChangeDetector.OnRectDimensionsChanged -= RectDimensionsChangeDetector_OnRectDimensionsChanged;
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
    }

    private void Start()
    {
        InitializeGridLayout();
    }

    private void InitializeGridLayout()
    {
        if (rectTransform.childCount <= 0) return;

        float width = rectTransform.rect.width;
        float leftPadding = gridLayoutGroup.padding.left;
        float rightPadding = gridLayoutGroup.padding.right;

        float usableWidth = width - leftPadding - rightPadding;

        if (usableWidth <= 0) return;

        int columnCount;

        if (useFixedColummCount)
        {
            columnCount = this.columnCount;
        }
        else
        {
            int minWidthCellsNumber = CalculateMinWidthCells(usableWidth);
            int maxWidthCellsNumber = CalculateMaxWidthCells(usableWidth);

            columnCount = Mathf.CeilToInt(((float)maxWidthCellsNumber + minWidthCellsNumber) / 2);
        }


        float cellWitdh = CalculateCellWitdh(usableWidth, columnCount);

        if (useSquareCells)
        {
            gridLayoutGroup.cellSize = new Vector2(cellWitdh, cellWitdh);
        }
        else
        {
            gridLayoutGroup.cellSize = new Vector2(cellWitdh, gridLayoutGroup.cellSize.y);
        }
    }

    private int CalculateMinWidthCells(float usableWitdh)
    {
        bool foundNumber = false;
        int lastValidNumber = 1;
        int iteratedCellsNumber = 1;

        while (!foundNumber)
        {
            float totalCellsWidth = iteratedCellsNumber * minCellWidth + gridLayoutGroup.spacing.x * (iteratedCellsNumber -1);

            if(totalCellsWidth < usableWitdh)
            {
                lastValidNumber = iteratedCellsNumber;
                iteratedCellsNumber++;
            }
            else
            {
                foundNumber = true;
            }
        }

        return lastValidNumber;
    }
    private int CalculateMaxWidthCells(float usableWitdh)
    {
        bool foundNumber = false;
        int lastValidNumber = 1;
        int iteratedCellsNumber = 1;

        while (!foundNumber)
        {
            float totalCellsWidth = iteratedCellsNumber * maxCellWidth + gridLayoutGroup.spacing.x * (iteratedCellsNumber - 1);

            if (totalCellsWidth < usableWitdh)
            {
                lastValidNumber = iteratedCellsNumber;
                iteratedCellsNumber++;
            }
            else
            {
                foundNumber = true;
            }
        }

        return lastValidNumber;
    }

    private float CalculateCellWitdh(float usableWitdh, int columnCount)
    {
        float spacingWidth = gridLayoutGroup.spacing.x * (columnCount - 1);
        float cellWitdh = (usableWitdh - spacingWidth)/columnCount;
        return cellWitdh;
    }

    #region Subscriptions
    private void RectDimensionsChangeDetector_OnRectDimensionsChanged(object sender, System.EventArgs e)
    {
        InitializeGridLayout();
    }
    #endregion
}