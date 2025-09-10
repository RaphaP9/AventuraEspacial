using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.InputSystem;

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

    [Header("Debug")]
    [SerializeField] private bool debug;

    public static event EventHandler<OnLanguageEventArgs> OnLanguageSet;

    public class OnLanguageEventArgs : EventArgs
    {
        public Language language;
    }

    private void Awake()
    {
        SetSingleton();
        InitializeLanguage();
    }

    private void Update()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            if(currentLanguage == Language.Español)
            {
                SetLanguage(Language.English);
            }
            else
            {
                SetLanguage(Language.Español);
            }
        }
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

    private void InitializeLanguage()
    {
        Language loadedLanguage;

        if (!PlayerPrefs.HasKey(languageKey))
        {
            loadedLanguage = defaultLanguage;

            string defaultLanguageCode = FindLanguageIdentified(defaultLanguage).code;
            PlayerPrefs.SetString(languageKey, defaultLanguageCode);
            PlayerPrefs.Save();
        }
        else
        {
            string storedLanguageCode = PlayerPrefs.GetString(languageKey);
            loadedLanguage = FindLanguageIdentified(storedLanguageCode).language;
        }

        SetLanguage(loadedLanguage);
    }

    private LanguageIdentified FindLanguageIdentified(Language language)
    {
        foreach(LanguageIdentified languageIdentified in languagesIdentified)
        {
            if (languageIdentified.language == language) return languageIdentified;
        }

        if (debug) Debug.Log($"No Language Identified found for: {language}");
        return null;
    }

    private LanguageIdentified FindLanguageIdentified(string code)
    {
        foreach (LanguageIdentified languageIdentified in languagesIdentified)
        {
            if (languageIdentified.code == code) return languageIdentified;
        }

        if (debug) Debug.Log($"No Language Identified found for code: {code}");
        return null;
    }

    public void SetLanguage(Language language)
    {
        currentLanguage = language;
        SelectLocale(language);

        string languageCode = FindLanguageIdentified(language).code;

        PlayerPrefs.SetString(languageKey, languageCode);
        PlayerPrefs.Save();

        OnLanguageSet?.Invoke(this, new OnLanguageEventArgs { language = language });
    }

    private void SelectLocale(Language language)
    {
        string languageCode = FindLanguageIdentified(language).code;

        List<Locale> locales = LocalizationSettings.AvailableLocales.Locales;

        foreach (Locale locale in locales)
        {
            if (locale.Identifier.Code == languageCode)
            {
                LocalizationSettings.SelectedLocale = locale;
                break;
            }
        }

        if (debug) Debug.Log($"No locale found for Language: {language} - {languageCode}");
    }
}

[System.Serializable]
public class LanguageIdentified
{
    public string code;
    public Language language;
}
