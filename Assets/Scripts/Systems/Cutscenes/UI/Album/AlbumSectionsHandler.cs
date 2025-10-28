using UnityEngine;
using System;
using System.Collections.Generic;

public class AlbumSectionsHandler : MonoBehaviour
{
    [Header("Lists")]
    [SerializeField] private List<AlbumSectionButtonUIRelationship> albumSectionButtonUIRelationships;

    [Header("Settings")]
    [SerializeField] private AlbumSectionButton startingSelectedButton;

    [Header("Runtime Filled")]
    [SerializeField] private AlbumSectionButtonUIRelationship currentSelectedRelationship;

    [Header("Debug")]
    [SerializeField] private bool debug;

    [Serializable]
    public class AlbumSectionButtonUIRelationship
    {
        public AlbumSectionButton albumSectionButton;
        public AlbumSectionUI albumSectionUI;
    }

    private void OnEnable()
    {
        AlbumSectionButton.OnButtonClicked += AlbumSectionButton_OnButtonClicked;
    }

    private void OnDisable()
    {
        AlbumSectionButton.OnButtonClicked -= AlbumSectionButton_OnButtonClicked;
    }

    private void Start()
    {
        SelectRelationshipByButton(startingSelectedButton, true);
    }


    private void SelectRelationshipByButton(AlbumSectionButton albumSectionButton, bool instant)
    {
        AlbumSectionButtonUIRelationship relationship = GetRelationshipByButton(albumSectionButton);

        if(relationship == null)
        {
            if (debug) Debug.Log("AlbumSectionButtonUIRelationship is null.");
            return;
        }

        if (relationship == currentSelectedRelationship) return; //Already Selected

        if(IsValidRelationship(currentSelectedRelationship)) DeselectRelationship(currentSelectedRelationship, instant);

        SelectRelationship(relationship, instant);

        currentSelectedRelationship = relationship;
    }

    #region Utility Methods
    private AlbumSectionButtonUIRelationship GetRelationshipByButton(AlbumSectionButton albumSectionButton)
    {
        foreach(AlbumSectionButtonUIRelationship relationship in albumSectionButtonUIRelationships)
        {
            if(relationship.albumSectionButton == albumSectionButton) return relationship;
        }

        return null;
    }

    private void SelectRelationship(AlbumSectionButtonUIRelationship relationship, bool instant)
    {
        if (relationship == null) return;

        if (instant)
        {
            relationship.albumSectionButton.SelectInstant();
            relationship.albumSectionUI.SelectInstant();
        }
        else
        {
            relationship.albumSectionButton.Select();
            relationship.albumSectionUI.Select();
        }
    }

    private void DeselectRelationship(AlbumSectionButtonUIRelationship relationship, bool instant)
    {
        if (relationship == null) return;

        if (instant)
        {
            relationship.albumSectionButton.DeselectInstant();
            relationship.albumSectionUI.DeselectInstant();
        }
        else
        {
            relationship.albumSectionButton.Deselect();
            relationship.albumSectionUI.Deselect();
        }
    }

    private bool IsValidRelationship(AlbumSectionButtonUIRelationship relationship)
    {
        if( relationship == null) return false;
        if(relationship.albumSectionButton == null) return false;
        if (relationship.albumSectionUI == null) return false;

        return true;
    }
    #endregion


    #region Subscriptions
    private void AlbumSectionButton_OnButtonClicked(object sender, AlbumSectionButton.OnButtonClickedEventArgs e)
    {
        SelectRelationshipByButton(e.albumSectionButton, false);
    }
    #endregion
}
