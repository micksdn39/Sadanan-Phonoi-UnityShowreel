using System; 
using Script.Game;
using Script.Service.Character;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Shop
{ 
    public class CharacterShopCtrl : BaseCtrl<CharacterShopTab,Gashapon>
    { 
        [SerializeField] private Image gashaponBanner;
        
        private int currentGashaponId = 1;

        private void Start()
        {
            RefreshUi();
        }

        protected override void InitTabCallback()
        { 
            listOfTabs[0].ButtonClick();
        }
        protected override void InitInfo(Action Success = null, Action Failed = null)
        { 
            GameInstance.GameService.GetGashaponList(result =>
            {
                listOfInfo = result.gashaponList; 
            });
  
            Success?.Invoke();
        }
        protected override void Disable()
        {
        }
        protected override void OnClickTab(Gashapon info)
        { 
            currentGashaponId = info.gashaId;
            var gashapon = GameInstance.GameDatabase.GetCharacterShopSetting(currentGashaponId); 
            gashaponBanner.sprite = gashapon.gashaponBanner;
        }

        public void OnButtonClick_OneTicket()
        {
            GameInstance.GameService.GashaponRandom(currentGashaponId, result =>
            {
                var character = GameInstance.GameDatabase.GetCharacter(result.characterId);
                Debug.Log("character: " + character.characterName + 
                          " tier: " + character.tier + 
                          " characterId: " + result.characterId);
            });
        }
        public void OnButtonClick_TenTicket()
        {
            GameInstance.GameService.GetGashaponRandomList(currentGashaponId, result =>
            {
                foreach (var r in result.characterListId)
                {
                    var character = GameInstance.GameDatabase.GetCharacter(r);
                    Debug.Log("character: " + character.characterName + 
                              " tier: " + character.tier + 
                              " characterId: " + r);
                } 
            });
        }
    }
}
