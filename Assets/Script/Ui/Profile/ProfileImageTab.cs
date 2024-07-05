using Script.Database.ProfileData;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Ui.Profile
{
    public class ProfileImageTab : BaseTab<ProfileDataSO>
    {        
        [SerializeField] private Image image; 
        protected override void InitUi()
        {
            image.sprite = info.profileIcon;
        }
        protected override void OnClick()
        { 
        }
        protected override void Disable()
        { 
        }
    }
}
