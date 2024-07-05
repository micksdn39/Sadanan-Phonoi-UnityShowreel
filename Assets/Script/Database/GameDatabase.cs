using System.Collections.Generic;
using Script.Database.ProfileData;
using UnityEngine;

namespace Script.Database
{
    [CreateAssetMenu(fileName = "GameDatabase", menuName = "ScriptableObjects/GameDatabase")]
    public class GameDatabase : ScriptableObject
    {
        [Header("ProfileDatabase")]
        public List<ProfileDataSO> listOfProfileData;

        public ProfileDataSO GetProfileData(int id)
        {
            return listOfProfileData.Find(x => x.profileId == id);
        }
        

    }
}
