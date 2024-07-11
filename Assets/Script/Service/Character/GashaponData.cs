using System;
using System.Collections.Generic;
using Script.Database.Shop;
using Script.Enum;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.Service.Character
{ 
    public class GashaponData
    {
        public List<Gashapon> gashaponList = new List<Gashapon>();

        public Dictionary<GameEnum.ETier, int> gashaponTierRateDefault = new Dictionary<GameEnum.ETier, int>();

        public GashaponData()
        {
            gashaponTierRateDefault.Add(GameEnum.ETier.COMMON, 80);
            gashaponTierRateDefault.Add(GameEnum.ETier.RARE, 20);
            gashaponTierRateDefault.Add(GameEnum.ETier.EPIC, 5); 
            gashaponTierRateDefault.Add(GameEnum.ETier.LEGEND, 0); 
            
            var gashapon1 = new Gashapon(1)
                { gashaName = "Starter Character" };
            gashapon1.AddCharacter(8, GameEnum.ETier.COMMON);
            gashapon1.AddCharacter(7, GameEnum.ETier.COMMON);
            gashapon1.AddCharacter(6, GameEnum.ETier.RARE); 
            gashapon1.AddCharacter(5, GameEnum.ETier.RARE); 
            gashapon1.AddCharacter(4, GameEnum.ETier.EPIC); 
            gashapon1.AddCharacter(3, GameEnum.ETier.EPIC);  
            gashapon1.gashaponTierRate = gashaponTierRateDefault;
            gashaponList.Add(gashapon1);
            
            var gashapon2 = new Gashapon(2,GameEnum.ECurrency.GEM)
                { gashaName = "Rate Up! Legend" };
            gashapon2.AddCharacter(8, GameEnum.ETier.COMMON);
            gashapon2.AddCharacter(7, GameEnum.ETier.COMMON);
            gashapon2.AddCharacter(6, GameEnum.ETier.RARE); 
            gashapon2.AddCharacter(5, GameEnum.ETier.RARE); 
            gashapon2.AddCharacter(4, GameEnum.ETier.EPIC); 
            gashapon2.AddCharacter(3, GameEnum.ETier.EPIC);  
            gashapon2.AddCharacter(2, GameEnum.ETier.LEGEND);  
            gashapon2.AddCharacter(1, GameEnum.ETier.LEGEND);  
            
            gashapon2.gashaponTierRate.Add(GameEnum.ETier.COMMON, 80);
            gashapon2.gashaponTierRate.Add(GameEnum.ETier.RARE, 20);
            gashapon2.gashaponTierRate.Add(GameEnum.ETier.EPIC, 5);
            gashapon2.gashaponTierRate.Add(GameEnum.ETier.LEGEND, 3);
            
            gashaponList.Add(gashapon2);
        }
        
        public Gashapon GetGashapon(int gashaId)
        { 
            return gashaponList.Find(x => x.gashaId == gashaId);
        }

        public int GetRandomGashapon(int gashaId)
        {
            var tier = RandomTier((int)DateTime.Now.Ticks,gashaId);
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

        GameEnum.ETier RandomTier(int seed,int gashaId)
        {
            int totalWeight = 0;
            foreach (var tier in gashaponTierRateDefault)
            {
                totalWeight += tier.Value;
            }
            Random.InitState(seed); 
            int randomValue = Random.Range(0, totalWeight);

            var tierRate = GetGashapon(gashaId).gashaponTierRate;
            if(randomValue < tierRate[GameEnum.ETier.LEGEND])
                return GameEnum.ETier.LEGEND;
            if(randomValue < tierRate[GameEnum.ETier.EPIC] +tierRate[GameEnum.ETier.LEGEND])
                return GameEnum.ETier.EPIC;
            if(randomValue < tierRate[GameEnum.ETier.RARE] +tierRate[GameEnum.ETier.EPIC]
                                                           +tierRate[GameEnum.ETier.LEGEND])
                return GameEnum.ETier.RARE;
            return GameEnum.ETier.COMMON;
        }
    }
}
