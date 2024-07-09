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
        [SerializeField] public List<PlayerCharacterPosition> characterPosition;

        public delegate void OnPlayerProfileEvent(PlayerInfo playerInfo); 
        [HideInInspector] public OnPlayerProfileEvent OnPlayerProfileChanged;
        public PlayerInfo()
        {
            playerName = "";
            profileId = 0;
            currencyInfo = new CurrencyInfo();
            characterInfo = new List<CharacterInfo>();
            characterPosition = new List<PlayerCharacterPosition>();
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
                characterInfo = characterInfo,
                characterPosition = characterPosition
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
        public PlayerInfo SetCharacterPosition(List<PlayerCharacterPosition> characterPosition)
        {
            this.characterPosition = characterPosition;
            return this;
        }

        public PlayerCharacterPosition GetCharacterPosition(int position)
        {
            if(characterPosition.Find(x => x.position == position) == null)
            {
                var newCharacterPosition = new PlayerCharacterPosition { position = position };
                characterPosition.Add(newCharacterPosition);
                return newCharacterPosition;
            }
            return characterPosition.Find(x => x.position == position);
        }
    }

    [System.Serializable]
    public class PlayerCharacterPosition
    {
      [SerializeField] public int position;
      [SerializeField] public int characterId;
      
      public void SetCharacterId(int id)
      {
          characterId = id;
      }
    }
}
