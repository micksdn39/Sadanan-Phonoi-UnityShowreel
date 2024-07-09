using System;
using Script.DialogBox;
using Script.Language;
using Script.Ui.MainMenu;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Game
{
    public class GameMainMenu : MonoBehaviour
    {
        [SerializeField] private string loadSceneName;  
        [SerializeField] private PanelMainMenu panelMainMenu;
        private void Start()
        {
            if (!GameInstance.IsInitialized)
            {
                SceneManager.LoadScene("LoginScene");
                return;
            }
            SubscribePlayerProfile();
            SubscribeCurrency();
        } 
        private void OnDestroy()
        {
            if(!GameInstance.IsInitialized) return;
            GameInstance.PlayerCtrl.playerInfo.currencyInfo.OnPlayerCurrencyChanged -= panelMainMenu.SetCurrency;
            GameInstance.PlayerCtrl.playerInfo.OnPlayerProfileChanged -= panelMainMenu.SetProfileInfo;
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
