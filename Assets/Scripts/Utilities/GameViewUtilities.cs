#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEditor;

public static class GameViewUtilities
{
    private const int PORTRAIT_RESOLUTION_INDEX = 7;
    private const int LANDSCAPE_RESOLUTION_INDEX = 8;

    private const float PORTRAIT_SCREEN_SCALE = 0.24f;
    private const float LANDSCAPE_SCREEN_SCALE = 0.47f;

    public enum GameViewSizeType { AspectRatio, FixedResolution }

    private static object gameViewSizesInstance;
    private static MethodInfo getGroup;

    static GameViewUtilities()
    {
        Type sizesType = typeof(Editor).Assembly.GetType("UnityEditor.GameViewSizes");
        Type singleType = typeof(ScriptableSingleton<>).MakeGenericType(sizesType);
        PropertyInfo instanceProp = singleType.GetProperty("instance");

        gameViewSizesInstance = instanceProp.GetValue(null, null);
        getGroup = sizesType.GetMethod("GetGroup");
    }

    public static void SetSize(int index)
    {
        Type gvWndType = typeof(Editor).Assembly.GetType("UnityEditor.GameView");
        PropertyInfo selectedSizeIndexProp = gvWndType.GetProperty("selectedSizeIndex", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        EditorWindow gameViewWindow = EditorWindow.GetWindow(gvWndType);
        selectedSizeIndexProp.SetValue(gameViewWindow, index, null);
    }

    public static void SetScale(float scale)
    {
        Type gvWndType = typeof(Editor).Assembly.GetType("UnityEditor.GameView");
        EditorWindow gameViewWindow = EditorWindow.GetWindow(gvWndType);

        FieldInfo zoomAreaField = gvWndType.GetField("m_ZoomArea", BindingFlags.Instance | BindingFlags.NonPublic);
        Object zoomAreaObj = zoomAreaField.GetValue(gameViewWindow);

        FieldInfo scaleField = zoomAreaObj.GetType().GetField("m_Scale", BindingFlags.Instance | BindingFlags.NonPublic);
        scaleField.SetValue(zoomAreaObj, new UnityEngine.Vector2(scale, scale));

        gameViewWindow.Repaint();
    }

    public static void SwitchToLandscape()
    {
        SetSize(LANDSCAPE_RESOLUTION_INDEX);
        SetScale(LANDSCAPE_SCREEN_SCALE);
    }

    public static void SwitchToPortrait()
    {
        SetSize(PORTRAIT_RESOLUTION_INDEX);
        SetScale(PORTRAIT_SCREEN_SCALE);
    }
}
#endif