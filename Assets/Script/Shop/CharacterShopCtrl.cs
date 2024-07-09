using System;
using System.Collections.Generic;
using Script.Database.Character;
using Script.DialogBox;
using Script.Game;
using Script.Service.Character;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Shop
{ 
    public class CharacterShopCtrl : BaseCtrl<CharacterShopTab,Gashapon>
    { 
        [SerializeField] private Transform parent;
        [SerializeField] private Image gashaponBanner;
        
        [SerializeField] private TextMeshProUGUI priceOneText;
        [SerializeField] private TextMeshProUGUI priceTenText;
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
            
            var price = info.gashaPrice.amount;
            var currencyType = info.gashaPrice.currencyType;
            priceOneText.text = price +" "+currencyType ;
            priceTenText.text = (price*10)+" "+currencyType;
        }

        public void OnButtonClick_OneTicket()
        {
            GameInstance.GameService.GashaponRandom(currentGashaponId, result =>
            {
                result.BasicMessageErrorService(success:() =>
                {
                    List<CharacterSO> list = new List<CharacterSO>();
                    list.Add(GameInstance.GameDatabase.GetCharacter(result.characterId));
            
                    var gashapon = Instantiate(Resources.Load("Prefabs/(template) gashapon"),parent);
                    var gashaponScript = gashapon.GetComponentInChildren<GashaponCtrl>();
                    if(gashaponScript!=null)
                        gashaponScript.Init(list);
                }); 
            });
        }
        public void OnButtonClick_TenTicket()
        {
            GameInstance.GameService.GashaponRandomList(currentGashaponId, result =>
            {
                result.BasicMessageErrorService(success:() =>
                {
                    List<CharacterSO> list = new List<CharacterSO>();
                    foreach (var r in result.characterListId) 
                        list.Add(GameInstance.GameDatabase.GetCharacter(r)); 
                
                    var gashapon = Instantiate(Resources.Load("Prefabs/(template) gashapon"),parent);
                    var gashaponScript = gashapon.GetComponentInChildren<GashaponCtrl>();
                    if(gashaponScript!=null)
                        gashaponScript.Init(list);
                });
              
            });
        }
    }
}
