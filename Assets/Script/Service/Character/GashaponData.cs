using System;
using System.Collections.Generic;
using Script.Database.Shop;
using Script.Enum;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.Service.Character
{
    [System.Serializable]
    public class GashaponData
    {
        public List<Gashapon> gashaponList = new List<Gashapon>();

        public Dictionary<GameEnum.ETier, int> gashaponTierRateDefault = new Dictionary<GameEnum.ETier, int>();

        public GashaponData()
        {
            gashaponTierRateDefault.Add(GameEnum.ETier.COMMON, 50);
            gashaponTierRateDefault.Add(GameEnum.ETier.RARE, 30);
            gashaponTierRateDefault.Add(GameEnum.ETier.EPIC, 15);
            gashaponTierRateDefault.Add(GameEnum.ETier.LEGEND, 5);
            
            var gashapon1 = new Gashapon(1)
                { gashaName = "Summer Gashapon" };
            gashapon1.AddCharacter(1, GameEnum.ETier.COMMON);
            gashapon1.AddCharacter(2, GameEnum.ETier.RARE);
            gashapon1.AddCharacter(5, GameEnum.ETier.EPIC);
            gashapon1.AddCharacter(9, GameEnum.ETier.LEGEND);
            gashapon1.gashaponTierRate = gashaponTierRateDefault;
            gashaponList.Add(gashapon1);
            
            var gashapon2 = new Gashapon(2,GameEnum.ECurrency.GEM)
                { gashaName = "Rate Up ! Legend x2" };
            gashapon2.AddCharacter(1, GameEnum.ETier.COMMON);
            gashapon2.AddCharacter(2, GameEnum.ETier.RARE);
            gashapon2.AddCharacter(5, GameEnum.ETier.EPIC);
            gashapon2.AddCharacter(9, GameEnum.ETier.LEGEND);
            
            gashapon2.gashaponTierRate.Add(GameEnum.ETier.COMMON, 50);
            gashapon2.gashaponTierRate.Add(GameEnum.ETier.RARE, 30);
            gashapon2.gashaponTierRate.Add(GameEnum.ETier.EPIC, 15);
            gashapon2.gashaponTierRate.Add(GameEnum.ETier.LEGEND, 10);
            
            gashaponList.Add(gashapon2);
        }
        
        public Gashapon GetGashapon(int gashaId)
        { 
            return gashaponList.Find(x => x.gashaId == gashaId);
        }

        public int GetRandomGashapon(int gashaId)
        {
            var tier = RandomTier((int)DateTime.Now.Ticks);
            Debug.Log("You got " + tier);
            var gashapon = GetGashapon(gashaId);
            var characterForRandom = gashapon.GetCharacterByTier(tier);
         
            return characterForRandom[Random.Range(0, characterForRandom.Count)].characterId;
        }
        public List<int> GetCharacterList(int gashaId)
        { 
            var characterList = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                characterList.Add(GetRandomGashapon(gashaId));
            }
            return characterList;
        } 
        public CurrencyOptions GetGashaponPrice(int gashaId)
        {
            return gashaponList.Find(x => x.gashaId == gashaId).gashaPrice;
        }

        GameEnum.ETier RandomTier(int seed)
        {
            int totalWeight = 0;
            foreach (var tier in gashaponTierRateDefault)
            {
                totalWeight += tier.Value;
            }
            Random.InitState(seed); 
            int randomValue = Random.Range(0, totalWeight);
            
            if(randomValue <= gashaponTierRateDefault[GameEnum.ETier.LEGEND])
                return GameEnum.ETier.LEGEND;
            if(randomValue <= gashaponTierRateDefault[GameEnum.ETier.EPIC])
                return GameEnum.ETier.EPIC;
            if(randomValue <= gashaponTierRateDefault[GameEnum.ETier.RARE])
                return GameEnum.ETier.RARE;
            return GameEnum.ETier.COMMON;
        }
    }

    public class Gashapon : BaseInfo
    {
        public string gashaName;
        public int gashaId;
        public List<CharacterGashapon> gashaponCharacter;
        public CurrencyOptions gashaPrice;
        public Dictionary<GameEnum.ETier, int> gashaponTierRate = new Dictionary<GameEnum.ETier, int>();
        public Gashapon(int gashaId,GameEnum.ECurrency currencyType = GameEnum.ECurrency.GOLD)
        {
            this.gashaId = gashaId;
            gashaponCharacter = new List<CharacterGashapon>();
            gashaPrice = new CurrencyOptions()
            {
                currencyType = currencyType,
                amount = 100
            };
        }
        public Gashapon AddCharacter(int characterId,GameEnum.ETier tier)
        {
            gashaponCharacter.Add(
                new CharacterGashapon
                {
                    characterId = characterId,
                    tier = tier
                });
            return this;
        }

        public List<CharacterGashapon> GetCharacterByTier(GameEnum.ETier tier)
        {
            return gashaponCharacter.FindAll(x => x.tier == tier);
        }
    }
    public class CharacterGashapon
    {
        public int characterId;
        public GameEnum.ETier tier ;
    }
}
