using System;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private Language language;

    public static event EventHandler<OnLanguageEventArgs> OnLanguageSet;

    public class OnLanguageEventArgs : EventArgs
    {
        public Language language;
    }

    private void Awake()
    {
        SetSingleton();
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

    public void SetLanguage(Language language)
    {
        this.language = language;
        OnLanguageSet?.Invoke(this, new OnLanguageEventArgs { language = language });
    }
}
