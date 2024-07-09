using UnityEngine;

namespace Script.Player
{
    [System.Serializable]
    public class CurrencyInfo
    {
        [SerializeField] public float gold ;
        [SerializeField] public float gem ;
        public delegate void OnPlayerCurrencyEvent(CurrencyInfo currencyInfo); 
        [HideInInspector] public OnPlayerCurrencyEvent OnPlayerCurrencyChanged;
        public CurrencyInfo()
        {
            gold = 100;
            gem = 0;
        }

        public CurrencyInfo SetGold(float gold)
        {
            this.gold = gold;
            OnPlayerCurrencyChanged?.Invoke(this);
            return this;
        }
        public CurrencyInfo SetGem(float gem)
        {
            this.gem = gem;
            OnPlayerCurrencyChanged?.Invoke(this);
            return this;
        }
    }
}
