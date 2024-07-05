using System;
using Script.Player;
using Script.Save;
using UnityEngine;

namespace Script.Service
{
    public class GameService : MonoBehaviour
    { 
        public event Action OnServiceStart;
        public event Action OnServiceFinish;
         
        public void RegisterAnonymous(PlayerInfo player)
        {
            ClientSave.Save(SaveKey.PlayerInfo,player);
        } 
        public PlayerInfo LoginAnonymous()
        {
            return  ClientSave.Load<PlayerInfo>(SaveKey.PlayerInfo);
        } 
    }
}
