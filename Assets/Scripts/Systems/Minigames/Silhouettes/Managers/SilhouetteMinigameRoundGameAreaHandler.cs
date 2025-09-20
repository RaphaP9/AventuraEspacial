using System.Collections.Generic;
using UnityEngine;

public class SilhouetteMinigameRoundGameAreaHandler : MonoBehaviour
{
    [Header("Holders")]
    [SerializeField] private RectTransform backpackHolder;
    [SerializeField] private List<Transform> figureHolders;
    [SerializeField] private List<Transform> silholuetteHolders;

    public RectTransform BackpackHolder => backpackHolder;
    public List<Transform> FigureHolders => figureHolders;
    public List<Transform> SilhouetteHolders => silholuetteHolders;
}
