using System;
using System.Collections.Generic;
using System.Linq;
using Script.Database.Character;
using Script.Game;
using UnityEngine;

namespace Script.Character
{
    public class CharacterInventoryCtrl : BaseCtrl<CharacterInventoryTab, CharacterInfo>
    {
        [SerializeField] private List<CharacterTeamTab> listOfTeamPosition;
        
        private CharacterInfo _currentInfo; 
        public event Action<CharacterInfo> OnAddTeamTab;
        private void OnEnable()
        {
            RefreshUi();
            
            foreach (var team in listOfTeamPosition)
            {
                team.OnButtonClickEvent += OnAddTeamClick;
            }
        }

        private void OnDisable()
        {
            foreach (var team in listOfTeamPosition)
            {
                team.OnButtonClickEvent -= OnAddTeamClick;
            }
        }

        protected override void InitInfo(Action Success = null, Action Failed = null)
        {
            GameInstance.GameService.GetUpdatePlayerInfo(result =>
            {
                GameInstance.PlayerCtrl.UpdatePlayerCharacter(result.player);
                listOfInfo = new List<CharacterInfo>();
                foreach (var character in result.player.characterInfo)
                {
                    listOfInfo.Add(character);
                } 
                listOfInfo = listOfInfo.OrderByDescending(ch => ch.characterId).ToList();
                Success?.Invoke();
            });

            foreach (var tab in listOfTeamPosition)
            {
                var character = GameInstance.PlayerCtrl.playerInfo.GetCharacterByPosition(tab.Position);
                if (character != null)
                {
                    tab.SetInfo(character);
                }
            } 
        }

        protected override void InitTabCallback()
        {
            foreach (var tab in listOfTabs)
            {
                OnAddTeamTab += tab.OnAddTeamClick;
            }
        }

        protected override void Disable()
        {
        }

        protected override void OnClickTab(CharacterInfo info)
        {
            foreach (var team in listOfTeamPosition)
            {
                team.SetAvailable(true);
            }
            _currentInfo = info;
        } 
        private void OnAddTeamClick(CharacterTeamTab info)
        {
            foreach (var team in listOfTeamPosition)
            {
                team.SetAvailable(false);
            }
            
            info.SetInfo(_currentInfo);

            GameInstance.GameService.SetCharacterPosition(info.Position,_currentInfo, 
                result =>
            { 
               GameInstance.PlayerCtrl.UpdatePlayerCharacter(result.player);
            }); 
            
            OnAddTeamTab?.Invoke(_currentInfo);
        }
    }
}
