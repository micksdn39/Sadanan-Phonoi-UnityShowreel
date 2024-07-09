using System;
using System.Collections.Generic;
using Script.Database.Character; 
using UnityEngine; 

namespace Script.Shop
{
    public class GashaponCtrl : BaseCtrl<GashaponTab,CharacterSO>
    {
        [SerializeField] private GameObject root;
        public void OnTrigger()
        {
            Destroy(root);
        }
        protected override void InitTabCallback()
        { 
        }

        public void Init(List<CharacterSO> list)
        {
            listOfInfo = list;
            RefreshUi(); 
        }
        protected override void InitInfo(Action Success = null, Action Failed = null)
        { 
            if(listOfInfo!=null)
                Success?.Invoke();
        }

        protected override void Disable()
        { 
        }

        protected override void OnClickTab(CharacterSO info)
        { 
        }
    }
}
