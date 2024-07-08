using System;
using System.Collections.Generic;
using Script.Enum;
using Script.Game;
using Script.Player;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Ui.MainMenu
{
    public class PanelMainMenu : SerializedMonoBehaviour
    {
        [Title("Panel Root")] 
        [SerializeField] private Dictionary<EMainMenuRoot,GameObject> mainMenuRoot; 
        [Title("Profile")]
        [SerializeField] private Image profileImage;
        [SerializeField] private TextMeshProUGUI playerName;
        [Title("Currency")]
        [SerializeField] private TextMeshProUGUI goldText;
        [SerializeField] private TextMeshProUGUI gemText;

        private void Start()
        {
            SetActiveRoot(EMainMenuRoot.MAIN_MENU);
        }

        enum EMainMenuRoot
        {
            MAIN_MENU = 0,
            CURRENCY_SHOP = 1,
            CHARACTER_SHOP = 2
        }
        public void SetCurrency(CurrencyInfo currency)
        {
            goldText.text = currency.gold.ToString();
            gemText.text = currency.gem.ToString();
        } 
        public void SetProfileInfo(PlayerInfo playerInfo)
        {
            var profileSprite = GameInstance.GameDatabase.GetProfileData(playerInfo.profileId).profileIcon;
            profileImage.sprite = profileSprite;
            playerName.text = playerInfo.playerName;
        } 
        public void OpenMenu(int menu)
        {
            SetActiveRoot((EMainMenuRoot)menu);
        }
        public void CloseMenu()
        {
            SetActiveRoot(EMainMenuRoot.MAIN_MENU);
        }
        
        private void SetActiveRoot(EMainMenuRoot root)
        {
            switch (root)
            {
                case EMainMenuRoot.MAIN_MENU:
                    foreach (var r in mainMenuRoot) 
                        r.Value.SetActive(false);                
                    break;
                case EMainMenuRoot.CURRENCY_SHOP:
                    foreach (var r in mainMenuRoot) 
                        r.Value.SetActive(r.Key == EMainMenuRoot.CURRENCY_SHOP); 
                    break;
                case EMainMenuRoot.CHARACTER_SHOP:
                    foreach (var r in mainMenuRoot) 
                        r.Value.SetActive(r.Key == EMainMenuRoot.CHARACTER_SHOP); 
                    break;
            }
        }
    }
}
