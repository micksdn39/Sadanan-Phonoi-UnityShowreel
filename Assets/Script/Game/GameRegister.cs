using System;
using System.Collections.Generic;
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
    public class GameRegister : SerializedMonoBehaviour
    { 
        [SerializeField] private Dictionary<ERegisterRoot,GameObject> registerRoot;  
        [Space]
        [SerializeField] private TMP_InputField inputFieldName;
        [SerializeField] private ProfileImageCtrl profileImageCtrl;
        
        private string _name;
        private int _profileId;  
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
        public void Init(Action callback)
        {
            OnRegisterSuccess = null;
            OnRegisterSuccess += callback;
            
            SetActiveRoot(ERegisterRoot.REGISTER);
            inputFieldName.onValueChanged.AddListener(input => 
                _name = input);
            profileImageCtrl.OnRegisterProfileSelected += OnRegisterProfileSelected;
            
            void OnRegisterProfileSelected(int profileId)
            {
                this._profileId = profileId;
            } 
        }

        #region Button Click 
        public void OnButtonClick_GuestLogin()
        { 
            SetActiveRoot(ERegisterRoot.ENTER_NAME);
        }
        public void OnButtonClick_ConfirmEnterName()
        {
            string nameConfirm = GameInstance.LanguageManager.GetText(GameText.TITLE_NAME_CONFIRM) + _name;
            
            Dialog.BasicMessageYesNo(nameConfirm,Callback);
            
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
                    .SetPlayerName(_name)
                    .SetProfileId(_profileId),
                result =>
                {  
                    result.BasicMessageErrorService(success:() =>
                    {
                        
                        string registerSuccess = GameInstance.LanguageManager.GetText(GameText.TITLE_REGISTER_SUCCESS);
                        Dialog.BasicMessageOK(registerSuccess, Callback);
            
                        void Callback(DialogResult result)
                        {
                            if (result == DialogResult.OK)
                            { 
                                OnRegisterSuccess?.Invoke();
                                SetActiveRoot(ERegisterRoot.NONE);
                            }
                        } 
                    },
                        error: () => SetActiveRoot(ERegisterRoot.ENTER_NAME));
                }); 
        }
        #endregion
        
        private void SetActiveRoot(ERegisterRoot root)
        { 
            foreach (var r in registerRoot) 
                r.Value.SetActive(r.Key == root);  
        }
    }
}
