using System;
using UnityEngine;
using System.Collections.Generic;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private Language defaultLanguage;

    [Header("PlayerPrefs Settings")]
    [SerializeField] private string languageKey;

    [Header("Lists")]
    [SerializeField] private List<LanguageIdentified> languagesIdentified;

    [Header("Runtime Filled")]
    [SerializeField] private Language currentLanguage;

    public static event EventHandler<OnLanguageEventArgs> OnLanguageSet;

    public class OnLanguageEventArgs : EventArgs
    {
        public Language language;
    }

    private void Awake()
    {
        SetSingleton();
    }

    private void Start()
    {
        LoadLanguage();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadLanguage()
    {
        Language loadedLanguage;

        if (!PlayerPrefs.HasKey(languageKey))
        {
            loadedLanguage = defaultLanguage;

            int defaultLanguageID = FindLanguageIdentified(defaultLanguage).id;
            PlayerPrefs.SetInt(languageKey, defaultLanguageID);
            PlayerPrefs.Save();
        }
        else
        {
            int storedLanguageID = PlayerPrefs.GetInt(languageKey);
            loadedLanguage = FindLanguageIdentified(storedLanguageID).language;
        }

        SetLanguage(loadedLanguage);
    }

    private LanguageIdentified FindLanguageIdentified(Language language)
    {
        foreach(LanguageIdentified languageIdentified in languagesIdentified)
        {
            if (languageIdentified.language == language) return languageIdentified;
        }

        return null;
    }

    private LanguageIdentified FindLanguageIdentified(int id)
    {
        foreach (LanguageIdentified languageIdentified in languagesIdentified)
        {
            if (languageIdentified.id == id) return languageIdentified;
        }

        return null;
    }

    public void SetLanguage(Language language)
    {
        currentLanguage = language;
        OnLanguageSet?.Invoke(this, new OnLanguageEventArgs { language = language });
    }
}

[System.Serializable]
public class LanguageIdentified
{
    public int id;
    public Language language;
}
