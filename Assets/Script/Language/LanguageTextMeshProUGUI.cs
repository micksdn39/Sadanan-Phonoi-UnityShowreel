using System;
using Script.Game;
using Sirenix.OdinInspector;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.Language
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LanguageTextMeshProUGUI : MonoBehaviour
    {
        [SerializeField] private bool isGetFontOnly = false;
        [HideIf("isGetFontOnly")]
        [SerializeField] private string key; 

        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            if(!GameInstance.IsInitialized)
            {
                GameInstance.OnInitializedCompleted += Start;
                return;
            } 
            SetText();
            GameInstance.LanguageManager.OnLanguageChanged += SetText; 
        }  
        private void OnDestroy()
        { 
            GameInstance.LanguageManager.OnLanguageChanged -= SetText; 
        } 
        private void SetText()
        {
            _text.font = GameInstance.LanguageManager.GetFont();
            if(isGetFontOnly) return;
            _text.text = GameInstance.LanguageManager.GetText(key);
        }
    }
}
