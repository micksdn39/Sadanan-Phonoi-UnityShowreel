using System.Collections.Generic;
using Script.Game;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Language
{
    public class LanguageSwitch : SerializedMonoBehaviour
    {
        [OdinSerialize] Dictionary<string, Sprite> languageSprites = new Dictionary<string, Sprite>();
        
        private Image image;
        private void Start()
        {
            if(!GameInstance.IsInitialized) return;
            image = this.GetComponent<Image>();
            SetImage();
        }

        private void SetImage()
        {
            if (languageSprites.ContainsKey(GameInstance.LanguageManager.CurrentLanguageKey))
            {
                image.sprite = languageSprites[GameInstance.LanguageManager.CurrentLanguageKey];
            }
        }
        public void ChangeLanguage()
        {
            string key = "";
            if (GameInstance.LanguageManager.CurrentLanguageKey == "ENG")
                key = "TH";
            else if (GameInstance.LanguageManager.CurrentLanguageKey == "TH")
                key = "ENG";
            GameInstance.LanguageManager.ChangeLanguage(key); 
            SetImage();
        }
    }
}
