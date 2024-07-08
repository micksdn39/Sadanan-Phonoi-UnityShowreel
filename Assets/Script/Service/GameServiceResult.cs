using Script.Player;
using UnityEngine;

namespace Script.Service
{
    public class GameServiceErrorCode
    {
        private const string ERROR_PREFIX = "ERROR_";
        public const string UNKNOW = ERROR_PREFIX + "UNKNOW";
        public const string NETWORK = ERROR_PREFIX + "NETWORK";
        public const string PlAYER_CURRENCY_NOT_ENOUGH = ERROR_PREFIX + "PLAYER_CURRENCY_NOT_ENOUGH";
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
    
}
