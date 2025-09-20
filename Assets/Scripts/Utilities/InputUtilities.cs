using UnityEngine;
using UnityEngine.InputSystem;

public static class InputUtilities
{
    public static Vector2 GetMousePosition() => Mouse.current.position.ReadValue();
}
