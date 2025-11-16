using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "NewPasswordItemSO", menuName = "ScriptableObjects/Password/PasswordItem")]
public class PasswordItemSO : ScriptableObject
{
    public int id;
    [ShowAssetPreview] public Sprite sprite;
}
