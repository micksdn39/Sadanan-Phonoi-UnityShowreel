using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Script.Player
{
    public class PlayerCtrl : SerializedMonoBehaviour
    {
       [OdinSerialize,ReadOnly] public PlayerInfo playerInfo { get; private set; }
       
       public void SetPlayerInfo(PlayerInfo info)
       {
           this.playerInfo = info.Clone();
       } 
       public void UpdatePlayerCurrency(PlayerInfo info)
       {
           playerInfo.currencyInfo = info.Clone().currencyInfo;
       }
       public void UpdatePlayerCharacter(PlayerInfo info)
       {
           playerInfo.characterInfo = info.Clone().characterInfo;
           playerInfo.characterPosition = info.Clone().characterPosition;
       }
    }
}
