using System;
using System.Collections.Generic;
using Script.Database.Character;
using Script.Game;
using UnityEngine;

namespace Script.Character
{
    public class CharacterInventoryCtrl : BaseCtrl<CharacterInventoryTab, CharacterSO>
    {
        [SerializeField] private List<CharacterTeamTab> listOfTeamPosition;
        
        private CharacterSO currentInfo;
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
                GameInstance.PlayerCtrl.SetPlayerInfo(result.player);
                listOfInfo = new List<CharacterSO>();
                foreach (var character in result.player.characterInfo)
                {
                    listOfInfo.Add(GameInstance.GameDatabase.GetCharacter(character.characterId));
                } 
                Success?.Invoke();
            });

            foreach (var characterPosition in GameInstance.PlayerCtrl.playerInfo.characterPosition)
            {
                var chPosition = listOfTeamPosition.Find(x => x.Position == characterPosition.position);
                if(chPosition != null);
                {
                    chPosition.SetInfo(GameInstance.GameDatabase.GetCharacter(characterPosition.characterId));
                }
            }
        }

        protected override void InitTabCallback()
        {
        }

        protected override void Disable()
        {
        }

        protected override void OnClickTab(CharacterSO info)
        {
            foreach (var team in listOfTeamPosition)
            {
                team.SetAvailable(true);
            }
            currentInfo = info;
        }

        private void OnAddTeamClick(CharacterTeamTab info)
        {
            foreach (var team in listOfTeamPosition)
            {
                team.SetAvailable(false);
            }
            
            info.SetInfo(currentInfo);

            GameInstance.GameService.UpdatePlayerCharacterPosition(info.Position,currentInfo.characterId, 
                result =>
            {
                GameInstance.PlayerCtrl.SetPlayerInfo(result.player);
            }); 
        }
    }
}
