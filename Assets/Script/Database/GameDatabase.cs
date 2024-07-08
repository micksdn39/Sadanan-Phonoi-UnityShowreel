using System.Collections.Generic;
using Script.Database.Character;
using Script.Database.ProfileData;
using Script.Database.Shop;
using Script.Enum;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Script.Database
{
    [CreateAssetMenu(fileName = "GameDatabase", menuName = "ScriptableObjects/GameDatabase")]
    public class GameDatabase : ScriptableObject
    {
        [Title("ProfileDatabase")]
        public List<ProfileDataSO> listOfProfileData;
        
        [Title("CurrencyShopDatabase")]
        public List<CurrencyShopSO> listOfCurrencyShop;
        
        [Title("CharacterDatabase")]
        public List<CharacterSO> listOfCharacter;
        
        [Title("CharacterShopSettingsDatabase")]
        public List<CharacterShopSettingSO> listOfCharacterShopSettings;

        public ProfileDataSO GetProfileData(int id)
        {
            return listOfProfileData.Find(x => x.profileId == id);
        }
        public List<CurrencyShopSO> GetCurrencyShop(GameEnum.ECurrency currencyType)
        {
            return listOfCurrencyShop.FindAll(x => x.CurrencyReward.currencyType == currencyType);
        } 
        public CharacterSO GetCharacter(int id)
        {
            return listOfCharacter.Find(x => x.characterId == id);
        } 
        public CharacterShopSettingSO GetCharacterShopSetting(int id)
        {
            return listOfCharacterShopSettings.Find(x => x.gashaponId == id);
        }

    }
}
