using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GameLogoHandler : MonoBehaviour
{
    [Header("Lists")]
    [SerializeField] List<GameLogoUILanguageRelationship> gameLogoUILanguageRelationships;

    [Header("Settings")]
    [SerializeField, Range(0f, 3f)] private float timeToPlayAppearAnimation;
    [SerializeField, Range(3f, 10f)] private float timeToPlayIdleMovementAnimation;

    [System.Serializable]
    public class GameLogoUILanguageRelationship
    {
        public GameLogoSingleUI gameLogoSingleUI;
        public Language language;
    }

    private void OnEnable()
    {
        LanguageManager.OnLanguageSet += LanguageManager_OnLanguageSet;
    }

    private void OnDisable()
    {
        LanguageManager.OnLanguageSet -= LanguageManager_OnLanguageSet;
    }

    private void Start()
    {
        EnableGameLogoUIByCurrentLanguage();
        StartCoroutine(GameLogoCoroutine());

    }

    #region Coroutines
    private IEnumerator GameLogoCoroutine()
    {
        yield return new WaitForSecondsRealtime(timeToPlayAppearAnimation);

        PlayAppearAnimations();

        while (true)
        {
            yield return new WaitForSecondsRealtime(timeToPlayIdleMovementAnimation);
            PlayIdleMovementAnimations();
        }
    }
    #endregion

    #region Logic
    private void EnableGameLogoUIByCurrentLanguage()
    {
        DisableAllGameLogos();
        EnableGameLogoByLanguage(LanguageManager.Instance.CurrentLanguageSetting.language);
    }

    private void DisableAllGameLogos()
    {
        foreach (GameLogoUILanguageRelationship relationship in gameLogoUILanguageRelationships)
        {
            relationship.gameLogoSingleUI.DisableImage();
        }
    }

    private void EnableGameLogoByLanguage(Language language)
    {
        foreach(GameLogoUILanguageRelationship relationship in gameLogoUILanguageRelationships)
        {
            if (relationship.language == language)
            {
                relationship.gameLogoSingleUI.EnableImage();
                return;
            }
        }
    }
    #endregion

    #region Animations
    private void PlayAppearAnimations()
    {
        foreach(GameLogoUILanguageRelationship relationship in gameLogoUILanguageRelationships)
        {
            relationship.gameLogoSingleUI.PlayAppearAnimation();
        }
    }

    private void PlayIdleMovementAnimations()
    {
        foreach (GameLogoUILanguageRelationship relationship in gameLogoUILanguageRelationships)
        {
            relationship.gameLogoSingleUI.PlayIdleMovementAnimation();
        }
    }
    #endregion

    #region Subscriptions
    private void LanguageManager_OnLanguageSet(object sender, LanguageManager.OnLanguageEventArgs e)
    {
        EnableGameLogoUIByCurrentLanguage();
    }
    #endregion
}
