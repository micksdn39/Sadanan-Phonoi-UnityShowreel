﻿using System;
using System.Collections.Generic;
using Script.Save;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using TMPro;
using UnityEngine; 

namespace Script.Language
{
    [CreateAssetMenu (fileName = "LanguageManager", menuName = "ScriptableObjects/LanguageManager")]
    public class LanguageManager : SerializedScriptableObject
    {  
        [OdinSerialize] public Dictionary<string,TMP_FontAsset> fonts = new Dictionary<string, TMP_FontAsset>();
        [OdinSerialize] public Dictionary<string, string> texts = new Dictionary<string, string>();
        public string CurrentLanguageKey { get; private set; } 
 
        private string defaultLanguageKey = "ENG";
        private string playerPrefsKey = "USER_LANG";
        public event Action OnLanguageChanged;
        private void Awake()
        { 
            CurrentLanguageKey = ClientSave.Load(playerPrefsKey, defaultLanguageKey);
             
            texts = DefaultLocale.GetLanguage(defaultLanguageKey);
            ChangeLanguage(CurrentLanguageKey);
        }
        [Button]
        public void ChangeLanguage(string languageKey)
        {
            if (!DefaultLocale.IsContainsKey(languageKey))
                return;
             
            CurrentLanguageKey = languageKey;
            texts = DefaultLocale.GetLanguage(languageKey);
            OnLanguageChanged?.Invoke();
            ClientSave.Save(playerPrefsKey, CurrentLanguageKey);
        }

        private void OnValidate()
        {
            ChangeLanguage(CurrentLanguageKey);
        }

        public string GetText(string key, string defaultValue = "")
        {
            if (!string.IsNullOrEmpty(key))
            {
                if (texts.ContainsKey(key))
                    return texts[key]; 
            }
            return defaultValue;
        }

        public TMP_FontAsset GetFont()
        {
            if (fonts.ContainsKey(CurrentLanguageKey))
                return fonts[CurrentLanguageKey];
            ChangeLanguage(defaultLanguageKey);
            return fonts[defaultLanguageKey];
        }
    }
}
