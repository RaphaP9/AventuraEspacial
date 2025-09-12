using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEditor;
using System.Collections;

public class SnappingScrollMenuUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform content;
    [Space]
    [SerializeField] private ScrollRectDragDetector scrollRectDragDetector;

    [Header("Settings")]
    [SerializeField] private float snapSpeed = 10f;

    [Header("Runtime Filled")]
    [SerializeField] private List<ItemRefferencePosition> items;
    [SerializeField] private ItemRefferencePosition targetSnapItem;

    private bool initializationLogicPerformed = false;

    private const float STOP_SNAP_THRESHOLD = 0.1f;

    [System.Serializable]
    public class ItemRefferencePosition
    {
        public RectTransform rectTransform;
        public Vector2 refferencePosition;
    }

    private void Start()
    {
        StartCoroutine(InitializationCoroutine());
    }

    private IEnumerator InitializationCoroutine()
    {
        yield return null; //Wait one Frame
        InitializeItems();

        initializationLogicPerformed = true;
    }

    private void InitializeItems()
    {
        foreach(Transform child in content.transform)
        {
            RectTransform rectTransform = child.GetComponent<RectTransform>();
            Vector2 refferencePosition = UIUtilities.GetCanvasPosition(rectTransform, canvas) - UIUtilities.GetCanvasPosition(scrollRect.viewport, canvas);

            ItemRefferencePosition itemRefferencePosition = new ItemRefferencePosition { rectTransform = rectTransform, refferencePosition = refferencePosition};
            items.Add(itemRefferencePosition);
        }
    }

    
}

