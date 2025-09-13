using UnityEngine;

public class MemoryMinigameManager : MonoBehaviour
{
    public static MemoryMinigameManager Instance {  get; private set; }

    [Header("RuntimeFilled")]
    [SerializeField] private MiniGameState miniGameState;
    private enum MiniGameState { StartingMinigame, NoCardSelected, WaitForSecondCard, FailPair, MatchPair, Win, Lose}


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
            //Debug.LogWarning("There is more than one MemoryMinigameManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    public bool CanFlipCard() => miniGameState == MiniGameState.NoCardSelected || miniGameState == MiniGameState.WaitForSecondCard;
}
