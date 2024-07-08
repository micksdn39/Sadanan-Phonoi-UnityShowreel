using Script.Database.Shop;
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
        }

        protected override void Disable()
        { 
        }
    }
}
