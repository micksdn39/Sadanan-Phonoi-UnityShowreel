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
         
        public void PlayerRegister(PlayerInfo player)
        {
            ClientSave.Save(SaveKey.PlayerInfo,player);
        } 
    }
}
