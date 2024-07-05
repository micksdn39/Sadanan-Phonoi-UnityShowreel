using System;
using Script.DialogBox;
using Script.Language;
using Script.Player;
using Script.Save;
using Script.Ui.Profile;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Script.Game
{
    public class GameRegister : MonoBehaviour
    {
        [SerializeField] private GameObject root; 
        [SerializeField] private TMP_InputField inputFieldName;
        [SerializeField] private ProfileImageCtrl profileImageCtrl;
        
        private string name;
        private int profileId; 
        
        public event Action OnRegisterSuccess;
        
        [Button]
        public void ResetPlayer()
        {
            ClientSave.DeleteAll();
        } 
        
        public void Open()
        { 
            root.SetActive(true);
            inputFieldName.onValueChanged.AddListener(input => 
                name = input);
            profileImageCtrl.OnRegisterProfileSelected += OnRegisterProfileSelected;
        }  
        private void OnRegisterProfileSelected(int profileId)
        {
            this.profileId = profileId;
        } 
        public void OnButtonClick_ConfirmEnterName()
        {
            profileImageCtrl.RefreshUi();
        }
        public void OnButtonClick_ConfirmRegister()
        {
            GameInstance.GameService.PlayerRegister(new PlayerInfo
            {
                playerName = name,
                profileId = profileId 
            });
            profileImageCtrl.Active(false);
            
            string registerSuccess = GameInstance.LanguageManager.GetText(GameText.TITLE_REGISTER_SUCCESS);
            string buttonOk = GameInstance.LanguageManager.GetText(GameText.TITLE_BUTTON_OK);
            BasicMessageBox.Show(
                new BasicMessageBoxInfo(registerSuccess, 
                new ButtonEntryInfo(buttonOk, DialogResult.OK)), 
                Callback);
            
            void Callback(DialogResult result)
            {
                if (result == DialogResult.OK)
                {
                    OnRegisterSuccess?.Invoke();
                }
            }
        }
    }
}
