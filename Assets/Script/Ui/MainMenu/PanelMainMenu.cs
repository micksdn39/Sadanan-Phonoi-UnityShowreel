using System;
using Script.Game;
using Script.Player;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Ui.MainMenu
{
    public class PanelMainMenu : MonoBehaviour
    {
        [Title("Profile")]
        [SerializeField] private Image profileImage;
        [SerializeField] private TextMeshProUGUI playerName;
        [Title("Currency")]
        [SerializeField] private TextMeshProUGUI goldText;
        [SerializeField] private TextMeshProUGUI gemText;
 
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
        
    }
}
