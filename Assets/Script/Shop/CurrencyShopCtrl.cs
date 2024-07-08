using System;
using System.Collections.Generic;
using Script.Database.Shop;
using Script.Game;
using TMPro;
using UnityEngine;

namespace Script.Shop
{
    public class CurrencyShopCtrl : BaseCtrl<CurrencyShopTab, CurrencyShopSO>
    {
        [Space] 
        [SerializeField] private TextMeshProUGUI currencyHeaderText;

        string currencyHeader;
        public void SetInfo(string header,List<CurrencyShopSO> listOfInfo)
        {
            this.listOfInfo = listOfInfo;
            currencyHeader = header; 
        }
        protected override void InitTabCallback()
        {
            currencyHeaderText.text = currencyHeader;
        }

        protected override void InitInfo(Action Success = null, Action Failed = null)
        { 
            if(listOfInfo != null)
                Success?.Invoke();
            else
                Failed?.Invoke();
            
        }

        protected override void Disable()
        { 
        }

        protected override void OnClickTab(CurrencyShopSO info)
        { 
        }
    }
}
