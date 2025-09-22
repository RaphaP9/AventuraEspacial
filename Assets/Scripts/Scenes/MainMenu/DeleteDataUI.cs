using UnityEngine;
using UnityEngine.UI;

public class DeleteDataUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Button deleteDataButton;

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        deleteDataButton.onClick.AddListener(DeleteData);
    }

    private void DeleteData()
    {
        DataContainer.Instance.ResetData();
        DataUtilities.WipeData();
    }
}
