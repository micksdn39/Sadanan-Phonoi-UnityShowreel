using System;
using Script.Database.Shop;
using Script.DialogBox;
using Script.Game;
using Script.Language;
using Script.Service;
using TMPro;
using UnityEngine;

namespace Script.Shop
{
    public class CurrencyShopTab : BaseTab<CurrencyShopSO>
    {
        [SerializeField] private TextMeshProUGUI currencyDescText;
        [SerializeField] private TextMeshProUGUI priceText;
        protected override void InitUi()
        { 
            currencyDescText.text = info.currencyDesc;
            priceText.text = info.CurrencyPurchase.amount.ToString();
        }

        protected override void OnClick()
        { 
            Dialog.BasicMessageYesNo(
                GameInstance.LanguageManager.GetText(GameText.TITLE_VIRTUAL_CURRENCY_PURCHASE),Callback);
            
            void Callback(DialogResult result)
            {
                if (result == DialogResult.Yes)
                { 
                   GameInstance.GameService.PurchaseVirtualCurrency(
                       info,finish =>
                       {
                           if(finish.Success)
                               GameInstance.PlayerCtrl.UpdatePlayerCurrency(finish.player);
                           else
                           {
                               Dialog.BasicMessageOK(finish.error,null);
                           }
                       });
                } 
            } 
        }

        protected override void Disable()
        { 
        }
    }
}
