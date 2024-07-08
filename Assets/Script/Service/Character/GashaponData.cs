using System;
using System.Collections.Generic;
using Script.Enum;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.Service.Character
{
    [System.Serializable]
    public class GashaponData
    {
        public List<Gashapon> gashaponList = new List<Gashapon>();

        public Dictionary<GameEnum.ETier, int> gashaponTierRate = new Dictionary<GameEnum.ETier, int>();

        public GashaponData()
        {
            var gashapon1 = new Gashapon(1)
                { gashaName = "Gashapon 1" };
            gashapon1.AddCharacter(1, GameEnum.ETier.COMMON);
            gashapon1.AddCharacter(2, GameEnum.ETier.RARE);
            gashapon1.AddCharacter(5, GameEnum.ETier.EPIC);
            gashapon1.AddCharacter(9, GameEnum.ETier.LEGEND);
            gashaponList.Add(gashapon1);
            
            var gashapon2 = new Gashapon(2)
                { gashaName = "Gashapon 2" };
            gashapon2.AddCharacter(1, GameEnum.ETier.COMMON);
            gashapon2.AddCharacter(2, GameEnum.ETier.RARE);
            gashapon2.AddCharacter(5, GameEnum.ETier.EPIC);
            gashapon2.AddCharacter(9, GameEnum.ETier.LEGEND);
            gashaponList.Add(gashapon2);
            
            gashaponTierRate.Add(GameEnum.ETier.COMMON, 50);
            gashaponTierRate.Add(GameEnum.ETier.RARE, 30);
            gashaponTierRate.Add(GameEnum.ETier.EPIC, 15);
            gashaponTierRate.Add(GameEnum.ETier.LEGEND, 5);
        }
        
        public Gashapon GetGashapon(int gashaId)
        { 
            Debug.Log(" gashaId: " + gashaId);
            return gashaponList.Find(x => x.gashaId == gashaId);
        }

        public int GetRandomGashapon(int gashaId)
        {
            var tier = RandomTier();
            
            var gashapon = GetGashapon(gashaId);
            var characterForRandom = gashapon.GetCharacterByTier(tier);
            Random.InitState((int)DateTime.Now.Ticks);
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

        GameEnum.ETier RandomTier()
        {
            int totalWeight = 0;
            foreach (var tier in gashaponTierRate)
            {
                totalWeight += tier.Value;
            }

            int randomValue = Random.Range(0, totalWeight);
           
            if(randomValue <= gashaponTierRate[GameEnum.ETier.LEGEND])
                return GameEnum.ETier.LEGEND;
            if(randomValue <= gashaponTierRate[GameEnum.ETier.EPIC])
                return GameEnum.ETier.EPIC;
            if(randomValue <= gashaponTierRate[GameEnum.ETier.RARE])
                return GameEnum.ETier.RARE;
            return GameEnum.ETier.COMMON;
        }
    }

    public class Gashapon : BaseInfo
    {
        public string gashaName;
        public int gashaId;
        public List<CharacterGashapon> gashaponCharacter;

        public Gashapon(int gashaId)
        {
            this.gashaId = gashaId;
            gashaponCharacter = new List<CharacterGashapon>();
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
