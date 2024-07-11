using System.Collections.Generic;
using UnityEngine;
using CharacterInfo = Script.Character.CharacterInfo;

namespace Script.Player
{
    [System.Serializable]
    public class PlayerInfo
    {
        [SerializeField] public string playerName;
        [SerializeField] public int profileId;
        [SerializeField] public CurrencyInfo currencyInfo; 
        [SerializeField] public List<CharacterInfo> characterInfo;  
        public delegate void OnPlayerCharacterEvent(); 
        [HideInInspector] public OnPlayerCharacterEvent OnCharacterChanged;
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
                characterInfo = characterInfo, 
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
        public CharacterInfo GetCharacterByGuid(string characterGuid)
        {
            return characterInfo.Find(x => x.Guid == characterGuid);
        }
        public void SetCharacterPositionByGuid(CharacterInfo info,int position)
        {
            var character = GetCharacterByGuid(info.Guid);
            if (character != null)
            {
                character.SetCharacterPosition(position); 
            } 
        }
        public CharacterInfo GetCharacterByPosition(int position)
        {
            return characterInfo.Find(x => x.position == position);
        }
        public bool IsPositionAvailable(int characterId)
        {
            var result = characterInfo.FindAll(x => x.characterId == characterId);
            return result.Find(x => x.position != 0) == null;
        } 
        public List<CharacterInfo> GetCharacterAtPosition()
        {
            return characterInfo.FindAll(x => x.position != 0);
        }
    } 
}
