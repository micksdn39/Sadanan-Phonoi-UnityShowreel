using System.Collections.Generic;
using Script.Database.Shop;
using Script.Enum; 

namespace Script.Service.Character
{ 
    public class CharacterGashapon
    {
        public int characterId;
        public GameEnum.ETier tier ;
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
}
