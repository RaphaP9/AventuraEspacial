using UnityEngine;
using UnityEngine.InputSystem;

public static class InputUtilities
{
    public static Vector2 GetPointerPosition()
    {
        // Using Mobile or Emulator(Bluestacks)
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed) return Touchscreen.current.primaryTouch.position.ReadValue();
        // Mouse input
        if (Mouse.current != null) return Mouse.current.position.ReadValue();

        return Vector2.zero;
    }
}
