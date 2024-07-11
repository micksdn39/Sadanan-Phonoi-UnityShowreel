using System;
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
        [OdinSerialize] private Dictionary<string, Sprite> languageSprites = new Dictionary<string, Sprite>();
        
        private Image _image;

        private void Awake()
        {
            _image = this.GetComponent<Image>();
        } 
        private void Start()
        {
            if(!GameInstance.IsInitialized)  
                return; 
            SetImage();
        } 
        private void SetImage()
        {
            if (languageSprites.ContainsKey(GetLanguageKey()))
            {
                _image.sprite = languageSprites[GetLanguageKey()];
            }
        }
        public void ChangeLanguage()
        { 
            GameInstance.LanguageManager.ChangeLanguage(GetLanguageKey()); 
            SetImage();
        } 
        private string GetLanguageKey()
        {
            string key = "";
            if (GameInstance.LanguageManager.CurrentLanguageKey == "ENG")
                key = "TH";
            else if (GameInstance.LanguageManager.CurrentLanguageKey == "TH")
                key = "ENG";
            return key;
        }
    }
}
