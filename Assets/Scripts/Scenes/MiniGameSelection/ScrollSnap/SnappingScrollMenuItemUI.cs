using System;
using UnityEngine;

public class SnappingScrollMenuItemUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private RectTransform rectTransform;

    [Header("RuntimeFilled")]
    [SerializeField] private int assignedIndex;
    [SerializeField] private Vector2 refferencePosition;

    public event EventHandler OnItemSnap;

    public int AssignedIndex => assignedIndex;
    public RectTransform RectTransform => rectTransform;
    public Vector2 RefferencePosition => refferencePosition;

    public void SetAssignedIndex(int assignedIndex) => this.assignedIndex = assignedIndex;
    public void SetRefferencePosition(Vector2 refferencePosition) => this.refferencePosition = refferencePosition;

    public void TriggerSnapEvents()
    {
        OnItemSnap?.Invoke(this, EventArgs.Empty);
    } 
}
