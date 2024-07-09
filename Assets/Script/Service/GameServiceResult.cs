using System.Collections.Generic;
using Script.Player;
using Script.Service.Character;
using UnityEngine;

namespace Script.Service
{
    public class GameServiceErrorCode
    {
        private const string ERROR_PREFIX = "ERROR_";
        public const string UNKNOW = ERROR_PREFIX + "UNKNOW";
        public const string NETWORK = ERROR_PREFIX + "NETWORK";
        public const string PlAYER_CURRENCY_NOT_ENOUGH = ERROR_PREFIX + "PLAYER_CURRENCY_NOT_ENOUGH";
        public const string ACCOUNT_NOT_EXIST = ERROR_PREFIX + "ACCOUNT_NOT_EXIST";
        public const string ACCOUNT_EXIST = ERROR_PREFIX + "ACCOUNT_EXIST";
    }
    [System.Serializable]
    public class GameServiceResult
    {
        public string error;
        public bool Success { get { return string.IsNullOrEmpty(error); } }
    }
    [System.Serializable]
    public class PlayerInfoResult : GameServiceResult
    {
        public PlayerInfo player;
    }
    [System.Serializable]
    public class RegisterResult : GameServiceResult
    { 
    }
    [System.Serializable]
    public class CharacterResult : GameServiceResult
    {
        public int characterId;
    }
    [System.Serializable]
    public class CharacterListResult : GameServiceResult
    {
        public List<int> characterListId;
    }
    [System.Serializable]
    public class GashaponResult : GameServiceResult
    {
        public List<Gashapon> gashaponList;
    }
}
