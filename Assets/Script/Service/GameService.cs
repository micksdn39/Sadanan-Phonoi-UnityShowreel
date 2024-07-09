using System;
using Script.Character;
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
        public void GetUpdatePlayerInfo(Action<PlayerInfoResult> onFinish)
        {
            onFinish(new PlayerInfoResult{ player = playerData.Clone() });
        }

        public void UpdatePlayerCharacterPosition(int position,int id,
            Action<PlayerInfoResult> onFinish)
        {
            playerData.GetCharacterPosition(position).SetCharacterId(id);
            
            onFinish(new PlayerInfoResult { player = playerData.Clone() });
            SavePlayerData();
        }

        #region Currency 
        public void PurchaseVirtualCurrency(CurrencyShopSO currencyShop,Action<PlayerInfoResult> onFinish)
        {
            bool isVerify = VerifyPlayerCurrency(currencyShop.CurrencyPurchase);
            if(!isVerify)
            {
                onFinish(new PlayerInfoResult { error = GameServiceErrorCode.PlAYER_CURRENCY_NOT_ENOUGH });
                return;
            } 
            IncreaseVirtualCurrency(currencyShop.CurrencyReward);
            onFinish(new PlayerInfoResult { player = playerData.Clone() }); 
            
            SavePlayerData();
        }

        bool VerifyPlayerCurrency(CurrencyOptions currencyOptions,int amount=1)
        {
            if (currencyOptions.amount != 0)
            { 
                switch (currencyOptions.currencyType)
                {
                    case GameEnum.ECurrency.GOLD:
                        if (playerData.currencyInfo.gold < (currencyOptions.amount * amount)) 
                            return false;  
                        break;
                    case GameEnum.ECurrency.GEM:
                        if (playerData.currencyInfo.gem < (currencyOptions.amount * amount)) 
                            return false;  
                        break; 
                }
                DecreaseVirtualCurrency(currencyOptions,amount);
            } 
            return true;
        }

        void DecreaseVirtualCurrency(CurrencyOptions currencyOptions,int amount=1)
        {
            switch (currencyOptions.currencyType)
            {
                case GameEnum.ECurrency.GOLD:
                    playerData.currencyInfo.SetGold(playerData.currencyInfo.gold - (currencyOptions.amount * amount));
                    break;
                case GameEnum.ECurrency.GEM:
                    playerData.currencyInfo.SetGem(playerData.currencyInfo.gem - (currencyOptions.amount*amount));
                    break;
            } 
        }
        void IncreaseVirtualCurrency(CurrencyOptions currencyOptions)
        {
            switch (currencyOptions.currencyType)
            {
                case GameEnum.ECurrency.GOLD:
                    playerData.currencyInfo.SetGold(playerData.currencyInfo.gold + currencyOptions.amount);
                    break;
                case GameEnum.ECurrency.GEM:
                    playerData.currencyInfo.SetGem(playerData.currencyInfo.gem + currencyOptions.amount);
                    break;
            } 
        }

        #endregion
        
        #region Gashapon 
        public void GetGashaponList(Action<GashaponResult> onFinish)
        {
            GashaponData gashaponData = new GashaponData();
            onFinish(new GashaponResult { gashaponList = gashaponData.gashaponList });
        }
        public void GashaponRandomList(int gashaId,Action<CharacterListResult> onFinish)
        {
            GashaponData gashaponData = new GashaponData();
            
            bool isVerify = VerifyPlayerCurrency(gashaponData.GetGashaponPrice(gashaId),10);
            if(!isVerify)
            {
                onFinish(new CharacterListResult { error = GameServiceErrorCode.PlAYER_CURRENCY_NOT_ENOUGH });
                return;
            } 
            
            var characterList = gashaponData.GetCharacterList(gashaId);
            foreach (var c in characterList)  
                playerData.AddCharacter(c); 
            onFinish(new CharacterListResult { characterListId = characterList });
            SavePlayerData();
        } 
        public void GashaponRandom(int gashaId,Action<CharacterResult> onFinish)
        {
            GashaponData gashaponData = new GashaponData();
            bool isVerify = VerifyPlayerCurrency(gashaponData.GetGashaponPrice(gashaId));
            if(!isVerify)
            {
                onFinish(new CharacterResult { error = GameServiceErrorCode.PlAYER_CURRENCY_NOT_ENOUGH });
                return;
            } 
            
            var gashapon = gashaponData.GetRandomGashapon(gashaId);
            playerData.AddCharacter(gashapon); 
            onFinish(new CharacterResult { characterId = gashapon });
            SavePlayerData();
        } 
        #endregion
    }
}
