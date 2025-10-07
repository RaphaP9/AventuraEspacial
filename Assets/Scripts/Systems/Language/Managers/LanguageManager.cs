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
    [SerializeField] private LanguageSettingSO defaultLanguageSetting;

    [Header("PlayerPrefs Settings")]
    [SerializeField] private string languageKey;

    [Header("Lists")]
    [SerializeField] private List<LanguageSettingSO> languageSettingSOs;

    [Header("Runtime Filled")]
    [SerializeField] private LanguageSettingSO currentLanguageSetting;

    [Header("Debug")]
    [SerializeField] private bool debug;

    public static event EventHandler<OnLanguageEventArgs> OnLanguageSet;

    public LanguageSettingSO CurrentLanguageSetting => currentLanguageSetting;

    public class OnLanguageEventArgs : EventArgs
    {
        public LanguageSettingSO languageSetting;
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
            if(currentLanguageSetting == GetLanguageSettingSOByLanguage(Language.Español))
            {
                SetLanguage(GetLanguageSettingSOByLanguage(Language.English));
            }
            else
            {
                SetLanguage(GetLanguageSettingSOByLanguage(Language.Español));
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
        LanguageSettingSO loadedLanguageSetting;

        if (!PlayerPrefs.HasKey(languageKey))
        {
            loadedLanguageSetting = defaultLanguageSetting;

            PlayerPrefs.SetString(languageKey, defaultLanguageSetting.languageCode);
            PlayerPrefs.Save();
        }
        else
        {
            string storedLanguageCode = PlayerPrefs.GetString(languageKey);
            loadedLanguageSetting = GetLanguageSettingSOByCode(storedLanguageCode);
        }

        SetLanguage(loadedLanguageSetting);
    }

    public void SetLanguage(LanguageSettingSO languageSettingSO)
    {
        if(currentLanguageSetting == languageSettingSO) return;

        currentLanguageSetting = languageSettingSO;
        SelectLocale(languageSettingSO);

        PlayerPrefs.SetString(languageKey, languageSettingSO.languageCode);
        PlayerPrefs.Save();

        OnLanguageSet?.Invoke(this, new OnLanguageEventArgs { languageSetting = languageSettingSO });
    }

    private void SelectLocale(LanguageSettingSO languageSettingSO)
    {
        List<Locale> locales = LocalizationSettings.AvailableLocales.Locales;

        foreach (Locale locale in locales)
        {
            if (locale.Identifier.Code == languageSettingSO.languageCode)
            {
                LocalizationSettings.SelectedLocale = locale;
                break;
            }
        }

        if (debug) Debug.Log($"No locale found for Language: {languageSettingSO.languageName} - {languageSettingSO.languageCode}");
    }

    #region Seek Methods
    private LanguageSettingSO GetLanguageSettingSOByLanguage(Language language)
    {
        foreach(LanguageSettingSO setting in languageSettingSOs)
        {
            if(setting.language == language) return setting;    
        }

        if (debug) Debug.Log($"No Language Identified found for: {language}");
        return null;
    }

    private LanguageSettingSO GetLanguageSettingSOByCode(string code)
    {
        foreach (LanguageSettingSO setting in languageSettingSOs)
        {
            if (setting.languageCode == code) return setting;
        }

        if (debug) Debug.Log($"No Language Identified found for code: {code}");
        return null;
    }
    #endregion
}
