using System;
using System.Collections.Generic;
using Script.Database.Shop;
using Script.Enum;
using Script.Game;
using UnityEngine;

namespace Script.Shop
{
    public class CurrencyShop : MonoBehaviour
    {
        [SerializeField] private CurrencyShopCtrl currencyShopPrefab; 
        [SerializeField] private Transform parent;
        private void Start()
        { 
            if(!GameInstance.IsInitialized) return;
            
            currencyShopPrefab.gameObject.SetActive(false);
            Init();
        }

        private void Init()
        {
            var goldShop = GameInstance.GameDatabase.GetCurrencyShop(GameEnum.ECurrency.GOLD);
            
            var gemShop = GameInstance.GameDatabase.GetCurrencyShop(GameEnum.ECurrency.GEM);
            
            CurrencyShopCtrl goldShopCtrl = Instantiate(currencyShopPrefab,parent);  
            CurrencyShopCtrl gemShopCtrl = Instantiate(currencyShopPrefab,parent);
            
            ActiveShop("Gold",goldShopCtrl, goldShop);
            ActiveShop("Gem",gemShopCtrl, gemShop);


            void ActiveShop(string header,CurrencyShopCtrl shopCtrl,List<CurrencyShopSO> listOfInfo)
            {
                shopCtrl.SetInfo(header,listOfInfo);
                shopCtrl.RefreshUi();
                shopCtrl.gameObject.SetActive(true);
            }
            
        }
    }
}
