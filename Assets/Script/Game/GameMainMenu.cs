using System;
using Script.Ui.MainMenu;
using UnityEngine;

namespace Script.Game
{
    public class GameMainMenu : MonoBehaviour
    {
        [SerializeField] private PanelMainMenu panelMainMenu;
        private void Start()
        {
            SubscribePlayerProfile();
            SubscribeCurrency();
        } 
        private void OnDestroy()
        {
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
