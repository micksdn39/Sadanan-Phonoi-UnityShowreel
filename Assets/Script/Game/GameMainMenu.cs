using System;
using System.Collections.Generic;
using Script.Database.Character;
using Script.DialogBox;
using Script.Language;
using Script.Ui.MainMenu;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Game
{
    public class GameMainMenu : SerializedMonoBehaviour
    {
        [SerializeField] private string loadSceneName;  
        [SerializeField] private PanelMainMenu panelMainMenu;
        [Space]
        [SerializeField] private Dictionary<int,Transform> dictionaryOfCharacterPosition; 
        private void Start()
        {
            if (!GameInstance.IsInitialized)
            {
                SceneManager.LoadScene("LoginScene");
                return;
            }
            SubscribePlayerProfile();
            SubscribeCurrency();
            SubscribePlayerCharacterChange();
        } 
        private void OnDestroy()
        {
            if(!GameInstance.IsInitialized) return;
            GameInstance.PlayerCtrl.playerInfo.currencyInfo.OnPlayerCurrencyChanged -= panelMainMenu.SetCurrency;
            GameInstance.PlayerCtrl.playerInfo.OnPlayerProfileChanged -= panelMainMenu.SetProfileInfo;
            GameInstance.PlayerCtrl.playerInfo.OnPlayerCharacterChanged -= OnPlayerCharacterChange;
        } 
        private void SubscribePlayerProfile()
        { 
            panelMainMenu.SetProfileInfo(GameInstance.PlayerCtrl.playerInfo);
            GameInstance.PlayerCtrl.playerInfo.OnPlayerProfileChanged += panelMainMenu.SetProfileInfo; 
        }
        private void SubscribeCurrency()
        {
            panelMainMenu.SetCurrency(GameInstance.PlayerCtrl.playerInfo.currencyInfo);
            GameInstance.PlayerCtrl.playerInfo.currencyInfo.OnPlayerCurrencyChanged += panelMainMenu.SetCurrency; 
        } 
        private void SubscribePlayerCharacterChange()
        {
            GameInstance.PlayerCtrl.playerInfo.UpdateCharacterPrefab(dictionaryOfCharacterPosition);
            GameInstance.PlayerCtrl.playerInfo.OnPlayerCharacterChanged += OnPlayerCharacterChange; 
        }
        private void OnPlayerCharacterChange(CharacterSO character,int position,
            Action<int, GameObject> callback)
        { 
            var gameObject = character.InstantiatePrefab(dictionaryOfCharacterPosition[position]); 
            callback?.Invoke(position,gameObject);
        }
        
        public void OnButtonClick_Logout()
        {
            GameInstance.GameService.LogoutAnonymous();
            Dialog.BasicMessageOK(GameInstance.LanguageManager.GetText(GameText.TITLE_LOGOUT),
                result =>
                {
                    SceneManager.LoadScene(loadSceneName);
                });
        } 
        public void OnButtonClick_Development()
        { 
            Dialog.BasicMessageOK(GameInstance.LanguageManager.GetText(GameText.TITLE_ON_DEVELOPMENT),
                result => { });
        }
    }
}
