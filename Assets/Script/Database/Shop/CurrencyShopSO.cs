using Script.Enum;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Script.Database.Shop
{
    [CreateAssetMenu(fileName = "CurrencyShop", menuName = "ScriptableObjects/CurrencyShop")]
    public class CurrencyShopSO : SerializedScriptableObject ,BaseInfo
    {
        [field: TextArea,SerializeField] public string currencyDesc { get; private set; } 
        [SerializeField] public CurrencyOptions CurrencyReward { get; private set; }
        [SerializeField] public CurrencyOptions CurrencyPurchase { get; private set; }
        [PreviewField(100, ObjectFieldAlignment.Left), SerializeField]
        public Sprite currencyIcon { get; private set; }
    }
    [System.Serializable]
    public class CurrencyOptions
    {
        [SerializeField] public GameEnum.ECurrency currencyType { get; set; }
        [SerializeField] public float amount { get; set; }
    }
    
}
