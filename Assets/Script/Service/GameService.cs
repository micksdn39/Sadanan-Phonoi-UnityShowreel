using System;
using Script.Database.Shop;
using Script.Enum;
using Script.Player;
using Script.Save;
using Script.Service.Character;
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
            ClientSave.Save(SaveKey.PlayerInfo+GetCurrentAccount(),playerData);
        } 
        private PlayerInfo GetPlayerData()
        { 
            return ClientSave.Load<PlayerInfo>(SaveKey.PlayerInfo+GetCurrentAccount());
        }
        private PlayerInfo GetPlayerData(string name)
        { 
            return ClientSave.Load<PlayerInfo>(SaveKey.PlayerInfo+name);
        }
        private string GetCurrentAccount()
        { 
            return ClientSave.Load(SaveKey.CurrentAccount,"");
        } 
        public void RegisterAnonymous(PlayerInfo player,Action<RegisterResult> onFinish)
        {
            if(GetPlayerData(player.playerName) != default)
            {
                onFinish(new RegisterResult() { error = GameServiceErrorCode.ACCOUNT_EXIST });
                return;
            }
            ClientSave.Save(SaveKey.CurrentAccount,player.playerName);
            ClientSave.Save(SaveKey.PlayerInfo+player.playerName,player);
            onFinish(new RegisterResult());
        } 
        public void LoginAnonymous(Action<PlayerInfoResult> onFinish)
        {
            if (GetCurrentAccount() == default)
            {
                onFinish(new PlayerInfoResult { error = GameServiceErrorCode.ACCOUNT_NOT_EXIST });
                return;
            } 
            playerData = GetPlayerData();
            onFinish(new PlayerInfoResult { player = playerData.Clone() });   
            
            SavePlayerData();
        }
        public void LoginAnonymous(string name,Action<PlayerInfoResult> onFinish)
        {
            if (GetPlayerData(name) == default)
            {
                onFinish(new PlayerInfoResult { error = GameServiceErrorCode.ACCOUNT_NOT_EXIST });
                return;
            } 
            ClientSave.Save(SaveKey.CurrentAccount,name);

            playerData = GetPlayerData();
            onFinish(new PlayerInfoResult { player = playerData.Clone() });   
            
            SavePlayerData();
        }

        public void LogoutAnonymous()
        {
            ClientSave.Delete(SaveKey.CurrentAccount);
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

        #region Gashapon 
        public void GetGashaponList(Action<GashaponResult> onFinish)
        {
            GashaponData gashaponData = new GashaponData();
            onFinish(new GashaponResult { gashaponList = gashaponData.gashaponList });
        }
        public void GetGashaponRandomList(int gashaId,Action<CharacterListResult> onFinish)
        {
            GashaponData gashaponData = new GashaponData();
            var characterList = gashaponData.GetCharacterList(gashaId);
            onFinish(new CharacterListResult { characterListId = characterList });
        } 
        public void GashaponRandom(int gashaId,Action<CharacterResult> onFinish)
        {
            GashaponData gashaponData = new GashaponData();
            var gashapon = gashaponData.GetRandomGashapon(gashaId);
            playerData.AddCharacter(gashapon); 
            onFinish(new CharacterResult { characterId = gashapon }); 
            
            SavePlayerData();
        } 
        #endregion
    }
}
