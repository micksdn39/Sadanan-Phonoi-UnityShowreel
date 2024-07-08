using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Player
{
    [System.Serializable]
    public class PlayerInfo
    {
        [SerializeField] public string playerName;
        [SerializeField] public int profileId ;
        [SerializeField] public CurrencyInfo currencyInfo ; 
        [SerializeField] public List<CharacterInfo> characterInfo ; 

        public delegate void OnPlayerProfileEvent(PlayerInfo playerInfo); 
        [HideInInspector] public OnPlayerProfileEvent OnPlayerProfileChanged;
        public PlayerInfo()
        {
            playerName = "";
            profileId = 0;
            currencyInfo = new CurrencyInfo();
            characterInfo = new List<CharacterInfo>();
        }
        public void AddCharacter(int characterId)
        {
            characterInfo.Add(new CharacterInfo(characterId));
        }
        public PlayerInfo Clone()
        {
            return new PlayerInfo
            {
                playerName = playerName,
                profileId = profileId,
                currencyInfo = currencyInfo,
                characterInfo = characterInfo
            };
        }
        public PlayerInfo SetPlayerName(string name)
        {
            playerName = name;
            OnPlayerProfileChanged?.Invoke(this);
            return this;
        }
        public PlayerInfo SetProfileId(int id)
        {
            profileId = id;
            OnPlayerProfileChanged?.Invoke(this);
            return this; 
        }
    }
}
