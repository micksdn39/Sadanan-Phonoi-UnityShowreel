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
        [SerializeField] private GameObject registerRoot; 
        [SerializeField] private GameObject enterNameRoot; 
        [SerializeField] private GameObject profileRoot;
        [Space]
        [SerializeField] private TMP_InputField inputFieldName;
        [SerializeField] private ProfileImageCtrl profileImageCtrl;
        
        private string name;
        private int profileId;  
        public event Action OnRegisterSuccess;

        enum ERegisterRoot
        {
            NONE = -1,
            REGISTER = 0,
            ENTER_NAME = 1,
            PROFILE = 2
        }
        [Button]
        public void ResetPlayer()
        {
            ClientSave.DeleteAll();
        }
        public void Open()
        {
            SetActiveRoot(ERegisterRoot.REGISTER);
            inputFieldName.onValueChanged.AddListener(input => 
                name = input);
            profileImageCtrl.OnRegisterProfileSelected += OnRegisterProfileSelected;
            
            void OnRegisterProfileSelected(int profileId)
            {
                this.profileId = profileId;
            } 
        }   
        public void OnButtonClick_GuestLogin()
        { 
            SetActiveRoot(ERegisterRoot.ENTER_NAME);
        }
        public void OnButtonClick_ConfirmEnterName()
        {
            string nameConfirm = GameInstance.LanguageManager.GetText(GameText.TITLE_NAME_CONFIRM) + name;
            string buttonYes = GameInstance.LanguageManager.GetText(GameText.TITLE_BUTTON_YES);
            string buttonCancel = GameInstance.LanguageManager.GetText(GameText.TITLE_BUTTON_CANCEL);
            
            BasicMessageBox.Show(
                new BasicMessageBoxInfo(nameConfirm, 
                   new ButtonEntryInfo[]
                   {
                       new ( buttonYes, DialogResult.Yes),
                       new ( buttonCancel, DialogResult.Cancel)
                   }), 
                Callback);
            
            void Callback(DialogResult result)
            {
                if (result == DialogResult.Yes)
                { 
                    SetActiveRoot(ERegisterRoot.PROFILE); 
                } 
            }
        }
        public void OnButtonClick_ConfirmRegister()
        {
            GameInstance.GameService.RegisterAnonymous(
                new PlayerInfo()
                    .SetPlayerName(name)
                    .SetProfileId(profileId));
              
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
                    SetActiveRoot(ERegisterRoot.NONE);
                }
            }
        }
        private void SetActiveRoot(ERegisterRoot root)
        {
            switch (root)
            {
                case ERegisterRoot.NONE:
                    registerRoot.SetActive(false);
                    enterNameRoot.SetActive(false);
                    profileRoot.SetActive(false);
                    break;
                case ERegisterRoot.REGISTER:
                    registerRoot.SetActive(true);
                    enterNameRoot.SetActive(false);
                    profileRoot.SetActive(false);
                    break;
                case ERegisterRoot.ENTER_NAME:
                    registerRoot.SetActive(false);
                    enterNameRoot.SetActive(true);
                    profileRoot.SetActive(false);
                    break;
                case ERegisterRoot.PROFILE:
                    registerRoot.SetActive(false);
                    enterNameRoot.SetActive(false);
                    profileRoot.SetActive(true);
                    break;
                
            }
        }
    }
}
