using Sirenix.OdinInspector;
using UnityEngine;

namespace Script.Database.ProfileData
{
    [CreateAssetMenu(fileName = "ProfileData", menuName = "ScriptableObjects/ProfileData")]
    public class ProfileDataSO : SerializedScriptableObject, BaseInfo
    {
        [SerializeField] public int profileId { get; private set; }
        [field: TextArea,SerializeField] public string profileName { get; private set; }

        [PreviewField(100, ObjectFieldAlignment.Left), SerializeField]
        public Sprite profileIcon { get; private set; }
    }
}
