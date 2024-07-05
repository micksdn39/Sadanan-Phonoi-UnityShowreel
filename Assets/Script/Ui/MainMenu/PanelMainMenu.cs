using Script.Game;
using Script.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Ui.MainMenu
{
    public class PanelMainMenu : MonoBehaviour
    {
        [SerializeField] private Image profileImage;
        [SerializeField] private TextMeshProUGUI playerName;
         
        public void SetProfileInfo(PlayerInfo playerInfo)
        {
            var profileSprite = GameInstance.GameDatabase.GetProfileData(playerInfo.profileId).profileIcon;
            profileImage.sprite = profileSprite;
            playerName.text = playerInfo.playerName;
        }
        
    }
}
