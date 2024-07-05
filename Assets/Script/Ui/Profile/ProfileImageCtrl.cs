using System; 
using Script.Database.ProfileData;
using Script.Game;
using UnityEngine;

namespace Script.Ui.Profile
{
    public class ProfileImageCtrl : BaseCtrl<ProfileImageTab,ProfileDataSO>
    { 
        [SerializeField] private GameObject root;

        private void Start()
        {
            RefreshUi();
        } 
        protected override void InitTabCallback()
        { 
        }
        protected override void InitInfo(Action Success = null, Action Failed = null)
        {
            listOfInfo = GameInstance.GameDatabase.listOfProfileData;
            if(listOfInfo != null)
            {
                Success?.Invoke(); 
            }
            else
                Failed?.Invoke(); 
        }
        protected override void Disable()
        { 
        }
        protected override void OnClickTab(ProfileDataSO info)
        { 
            OnRegisterProfileSelected?.Invoke(info.profileId);
        } 
        
        public delegate void RegisterProfileSelectedDelegate(int profileId); 
        public event RegisterProfileSelectedDelegate OnRegisterProfileSelected;
    }
}
