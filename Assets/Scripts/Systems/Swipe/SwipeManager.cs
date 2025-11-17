using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class SwipeManager : MonoBehaviour
{
    public static SwipeManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField, Range(50f, 200f)] private float minSwipeDistance = 100f; // In pixels

    private Vector2 startPos;
    private bool isSwiping = false;

    public static event EventHandler OnSwipeLeft;
    public static event EventHandler OnSwipeRight;
    public static event EventHandler OnSwipeUp;
    public static event EventHandler OnSwipeDown;

    private void Awake()
    {
        SetSingleton();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There is more than one UIManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        UpdateSwipe();
    }
   
    private void UpdateSwipe()
    {
        if (!CanSwipe())
        {
            isSwiping = false;
            return;
        }

        if (Pointer.current == null) return;

        if (Pointer.current.press.wasPressedThisFrame && !isSwiping)
        {
            startPos = Pointer.current.position.ReadValue();
            isSwiping = true;
        }

        if (Pointer.current.press.wasReleasedThisFrame && isSwiping)
        {
            isSwiping = false;

            Vector2 endPos = Pointer.current.position.ReadValue();
            Vector2 swipeDelta = endPos - startPos;

            if (swipeDelta.magnitude < minSwipeDistance) return; //Swipe too short

            swipeDelta.Normalize();

            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
            {
                if (swipeDelta.x > 0)
                {
                    OnSwipeRight?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    OnSwipeLeft?.Invoke(this, EventArgs.Empty);
                }              
            }
            else
            {
                if (swipeDelta.y > 0)
                {
                    OnSwipeUp?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    OnSwipeDown?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }

    protected virtual bool CanSwipe() => true;
}
