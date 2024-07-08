using System.Collections.Generic;
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

        public ProfileDataSO GetProfileData(int id)
        {
            return listOfProfileData.Find(x => x.profileId == id);
        }
        public List<CurrencyShopSO> GetCurrencyShop(GameEnum.ECurrency currencyType)
        {
            return listOfCurrencyShop.FindAll(x => x.CurrencyReward.currencyType == currencyType);
        }

    }
}
