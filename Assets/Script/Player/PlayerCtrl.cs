using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Script.Player
{
    public class PlayerCtrl : SerializedMonoBehaviour
    {
       [OdinSerialize,ReadOnly] public PlayerInfo playerInfo { get; private set; }
       
       public void SetPlayerInfo(PlayerInfo playerInfo)
       {
           this.playerInfo = playerInfo;
       }
    }
}
