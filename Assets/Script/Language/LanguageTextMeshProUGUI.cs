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

        private TextMeshProUGUI text;
        private void Start()
        {
            if(!GameInstance.IsInitialized)
            {
                GameInstance.OnInitializedCompleted += DoStart;
                return;
            } 
            DoStart(); 
        } 
        void DoStart()
        { 
            text = GetComponent<TextMeshProUGUI>();
            text.font = GameInstance.LanguageManager.GetFont();
            
            if(isGetFontOnly) return;
            
            text.text = GameInstance.LanguageManager.GetText(key);
            GameInstance.LanguageManager.OnLanguageChanged += SubscribeOnLanguageChanged; 
        }
        private void OnDestroy()
        {
            if(!GameInstance.IsInitialized)
            {  
                GameInstance.OnInitializedCompleted -= DoStart; 
            } 
        } 
        private void SubscribeOnLanguageChanged()
        {
            text.font = GameInstance.LanguageManager.GetFont();
            text.text = GameInstance.LanguageManager.GetText(key);
        }
    }
}
