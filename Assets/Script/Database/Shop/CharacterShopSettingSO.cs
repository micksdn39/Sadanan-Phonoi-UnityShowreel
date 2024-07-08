using Sirenix.OdinInspector;
using UnityEngine;

namespace Script.Database.Shop
{
    [CreateAssetMenu(fileName = "CharacterShopSettingSO", menuName = "ScriptableObjects/CharacterShopSettingSO")]
    public class CharacterShopSettingSO : SerializedScriptableObject
    {
       [SerializeField] public int gashaponId { get; private set; }
       [SerializeField] public string gashaponDesc { get; private set; }
       [PreviewField(100, ObjectFieldAlignment.Left), SerializeField]
       public Sprite gashaponIcon { get; private set; }
       [PreviewField(100, ObjectFieldAlignment.Left), SerializeField]
       public Sprite gashaponBanner { get; private set; }
    }
}
