using System;
using Script.Ui.MainMenu;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Game
{
    public class GameMainMenu : MonoBehaviour
    {
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
    }
}
