using System;
using UnityEngine;
using System.Collections;

public class RectDimensionsChangeDetector : MonoBehaviour
{
    [Header("Serttings")]
    [SerializeField] private bool forceCanvasUpdateOnDimensionsChanged;

    public event EventHandler OnRectDimensionsChanged;

    private bool isUpdating = false;

    private void OnRectTransformDimensionsChange()
    {
        StartCoroutine(DelayedUpdateCoroutine());
    }

    private IEnumerator DelayedUpdateCoroutine()
    {
        if (isUpdating) yield break;

        isUpdating = true;

        // Wait one frame
        yield return null;

        if (forceCanvasUpdateOnDimensionsChanged) Canvas.ForceUpdateCanvases();

        OnRectDimensionsChanged?.Invoke(this, EventArgs.Empty);

        isUpdating = false;
    }
}
