using UnityEngine;

[CreateAssetMenu(fileName = "NewPasswordNumberSO", menuName = "ScriptableObjects/UI/Password/PasswordNumber")]
public class PasswordNumberSO : ScriptableObject
{
    [Range(0, 9)] public int number;
    public string localizationTable;
    public string localizationBinding;
}
