using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System;

public class TestSceneButtonsUI : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private List<ButtonIndexRelationship> buttonIndexRelationshipList;

    public static event EventHandler<OnTestButtonClickedEventArgs> OnTestButtonClicked; 

    public class OnTestButtonClickedEventArgs : EventArgs
    {
        public ButtonIndexRelationship buttonIndexRelationship;
    }

    [System.Serializable]
    public class ButtonIndexRelationship
    {
        public Button button;
        public int assignedIndex;
    }

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        foreach(ButtonIndexRelationship buttonRelationship in buttonIndexRelationshipList)
        {
            buttonRelationship.button.onClick.AddListener(() => OnTestButtonClickedMethod(buttonRelationship));
        }
    }

    private void OnTestButtonClickedMethod(ButtonIndexRelationship buttonIndexRelationship)
    {
        OnTestButtonClicked?.Invoke(this, new OnTestButtonClickedEventArgs { buttonIndexRelationship = buttonIndexRelationship });
    }
}
