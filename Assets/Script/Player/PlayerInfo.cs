using UnityEngine;

namespace Script.Player
{
    [System.Serializable]
    public class PlayerInfo
    {
        [SerializeField] public string playerName;
        [SerializeField] public int profileId ;
        [SerializeField] public CurrencyInfo currencyInfo ;

        public PlayerInfo()
        {
            playerName = "";
            profileId = 0;
            currencyInfo = new CurrencyInfo();
        }
        public PlayerInfo SetPlayerName(string name)
        {
            playerName = name;
            return this;
        }
        public PlayerInfo SetProfileId(int id)
        {
            profileId = id;
            return this; 
        }
    }
}
