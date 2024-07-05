using UnityEngine;

namespace Script.Player
{
    [System.Serializable]
    public class CurrencyInfo
    {
        [SerializeField] public float gold ;
        [SerializeField] public float gem ;

        public CurrencyInfo()
        {
            gold = 100;
            gem = 0;
        }

        public void SetGold(float gold)
        {
            this.gold = gold;
        }
        public void SetGem(float gem)
        {
            this.gem = gem;
        }
    }
}
