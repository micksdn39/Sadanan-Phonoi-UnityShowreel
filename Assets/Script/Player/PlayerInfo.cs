using System;
using UnityEngine;

namespace Script.Player
{
    [System.Serializable]
    public class PlayerInfo
    {
        [SerializeField] public string playerName;
        [SerializeField] public int profileId ;
        [SerializeField] public CurrencyInfo currencyInfo ; 
        public delegate void OnPlayerInfoChangedEvent(PlayerInfo playerInfo); 
        public OnPlayerInfoChangedEvent OnPlayerInfoChanged;
        public PlayerInfo()
        {
            playerName = "";
            profileId = 0;
            currencyInfo = new CurrencyInfo();
        }
        public PlayerInfo SetPlayerName(string name)
        {
            playerName = name;
            OnPlayerInfoChanged?.Invoke(this);
            return this;
        }
        public PlayerInfo SetProfileId(int id)
        {
            profileId = id;
            OnPlayerInfoChanged?.Invoke(this);
            return this; 
        }
    }
}
