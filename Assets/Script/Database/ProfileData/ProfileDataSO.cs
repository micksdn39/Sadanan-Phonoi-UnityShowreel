using Sirenix.OdinInspector;
using UnityEngine;

namespace Script.Database.ProfileData
{
    [CreateAssetMenu(fileName = "ProfileData", menuName = "ScriptableObjects/ProfileData")]
    public class ProfileDataSO : ScriptableObject, BaseInfo
    {
        public int profileId;
        public string profileName;
        [PreviewField(100,ObjectFieldAlignment.Left)] public Sprite profileIcon;
    }
}
