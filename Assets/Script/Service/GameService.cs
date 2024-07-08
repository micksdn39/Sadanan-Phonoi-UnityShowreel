using System;
using Script.Database.Shop;
using Script.Enum;
using Script.Player;
using Script.Save;
using UnityEngine;

namespace Script.Service
{
    public class GameService : MonoBehaviour
    { 
        public event Action OnServiceStart;
        public event Action OnServiceFinish;

        private PlayerInfo playerData;

        private void SavePlayerData()
        {
            ClientSave.Save(SaveKey.PlayerInfo,playerData);
        }
        public void RegisterAnonymous(PlayerInfo player)
        {
            ClientSave.Save(SaveKey.PlayerInfo,player);
        } 
        public void LoginAnonymous(Action<PlayerInfoResult> onFinish)
        {
            playerData = ClientSave.Load<PlayerInfo>(SaveKey.PlayerInfo);
            onFinish(new PlayerInfoResult { player = playerData.Clone() });   
            
            SavePlayerData();
        }  
        public void PurchaseVirtualCurrency(CurrencyShopSO currencyShop,Action<PlayerInfoResult> onFinish)
        {
            if (currencyShop.CurrencyPurchase.amount != 0)
            { 
                switch (currencyShop.CurrencyPurchase.currencyType)
                {
                    case GameEnum.ECurrency.GOLD:
                        if (playerData.currencyInfo.gold < currencyShop.CurrencyPurchase.amount)
                        {
                            onFinish(new PlayerInfoResult{ error = GameServiceErrorCode.PlAYER_CURRENCY_NOT_ENOUGH });
                            return;
                        }
                        playerData.currencyInfo.SetGold(playerData.currencyInfo.gold - currencyShop.CurrencyPurchase.amount);
                        break;
                    case GameEnum.ECurrency.GEM:
                        if (playerData.currencyInfo.gem < currencyShop.CurrencyPurchase.amount)
                        {
                            onFinish(new PlayerInfoResult{ error = GameServiceErrorCode.PlAYER_CURRENCY_NOT_ENOUGH });
                            return;
                        }
                        playerData.currencyInfo.SetGem(playerData.currencyInfo.gem - currencyShop.CurrencyPurchase.amount);
                        break; 
                }
            }
            switch (currencyShop.CurrencyReward.currencyType)
            {
                case GameEnum.ECurrency.GOLD:
                    playerData.currencyInfo.SetGold(playerData.currencyInfo.gold + currencyShop.CurrencyReward.amount);
                    break;
                case GameEnum.ECurrency.GEM:
                    playerData.currencyInfo.SetGem(playerData.currencyInfo.gem + currencyShop.CurrencyReward.amount);
                    break;
            } 
            onFinish(new PlayerInfoResult { player = playerData.Clone() }); 
            
            SavePlayerData();
        }
    }
}
