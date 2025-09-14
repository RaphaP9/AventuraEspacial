using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class LanguageSelectionSceneTitleHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI titleText;

    [Header("Lists")]
    [SerializeField] private List<string> textsToShow;

    [Header("Settings")]
    [SerializeField,Range(3f,10f)] private float timeShowingText;

    private const string SWITCH_TRIGGER = "Switch";
    private int currentTextIndex = 0;

    private void Start()
    {
        SetText(currentTextIndex);
        StartCoroutine(TitleCoroutine());
    }

    private IEnumerator TitleCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeShowingText);
            animator.SetTrigger(SWITCH_TRIGGER);
        }
    }

    public void SwitchToNextText()
    {
        if(currentTextIndex >= textsToShow.Count - 1)
        {
            currentTextIndex = 0;
        }
        else
        {
            currentTextIndex++;
        }

        SetText(currentTextIndex);
    }

    private void SetText(int textIndex) => titleText.text = textsToShow[textIndex];

}