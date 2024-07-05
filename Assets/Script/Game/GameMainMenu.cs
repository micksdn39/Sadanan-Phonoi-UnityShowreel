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
        }

        private void OnDestroy()
        {
            GameInstance.PlayerCtrl.playerInfo.OnPlayerInfoChanged -= panelMainMenu.SetProfileInfo;
        } 
        private void SubscribePlayerProfile()
        { 
            panelMainMenu.SetProfileInfo(GameInstance.PlayerCtrl.playerInfo);
            GameInstance.PlayerCtrl.playerInfo.OnPlayerInfoChanged += panelMainMenu.SetProfileInfo;
        }
    }
}
