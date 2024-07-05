using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Script.Player
{
    public class PlayerCtrl : SerializedMonoBehaviour
    {
       [OdinSerialize,ReadOnly] private PlayerInfo playerInfo;
       
       public void SetPlayerInfo(PlayerInfo playerInfo)
       {
           this.playerInfo = playerInfo;
       }
    }
}
