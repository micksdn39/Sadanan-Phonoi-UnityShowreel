using System.Collections.Generic;
using Script.Game;
using UnityEngine;

namespace Script.Language
{  
    [System.Serializable]
    public class Language
    {
        public string languageKey;
        public List<LanguageData> dataList = new List<LanguageData>();

        public bool ContainKey(string key)
        {
            foreach (LanguageData entry in dataList)
            {
                if (string.IsNullOrEmpty(entry.key))
                    continue;
                if (entry.key.Equals(key))
                    return true;
            }
            return false;
        }

        public static string GetText(IEnumerable<LanguageData> langs, string defaultValue)
        {
            if (langs != null)
            {
                foreach (LanguageData entry in langs)
                {
                    if (string.IsNullOrEmpty(entry.key))
                        continue;
                    if (entry.key.Equals(GameInstance.LanguageManager.CurrentLanguageKey))
                        return entry.value;
                }
            }
            return defaultValue;
        }
    } 
    [System.Serializable]
    public struct LanguageData
    {
        public string key;
        [TextArea]
        public string value;
    }
}