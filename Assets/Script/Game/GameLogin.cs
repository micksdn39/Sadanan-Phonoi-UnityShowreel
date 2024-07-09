using System;
using System.Collections.Generic;
using Script.DialogBox;
using Script.Language;
using Script.Save;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Script.Game
{
    public class GameLogin : MonoBehaviour
    { 
        [SerializeField] private GameObject panelLogin;
        [SerializeField] private GameObject panelLoginOrRegister;
        [Space]
        [SerializeField] private string loadSceneName; 
        [SerializeField] private GameObject tabToStartGameObject;  
        [SerializeField] private GameRegister gameRegister;
        [SerializeField] private TextMeshProUGUI accountNameText;
        [SerializeField] private TMP_InputField inputName;
        
        private bool isOpenRegister = false;
        private string name;

        private enum ELoginPanel
        {
            NONE,
            LOGIN,
            LOGIN_OR_REGISTER
        }
        private void Start()
        {
            inputName.onValueChanged.AddListener(OnInputNameChanged);

            void OnInputNameChanged(string name)
            {
                this.name = name;
            } 
            ActivePanel(ELoginPanel.NONE);
        } 
        private void Update()
        {
            if (isOpenRegister) return;
            OnCheckGetInputMouse(); 
        }
        private void OnCheckGetInputMouse()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(IsOverUi(Input.mousePosition)) return;
                
                string currentPlayerId = ClientSave.Load(SaveKey.CurrentAccount);
                if (string.IsNullOrEmpty(currentPlayerId))
                {
                    ActivePanel(ELoginPanel.LOGIN_OR_REGISTER);
                    return;
                }  
                AccountLogin(); 
            }
        }

        private void AccountLogin()
        {
            GameInstance.GameService.LoginAnonymous(result =>
            {
                result.BasicMessageErrorService(success: () =>
                { 
                    Dialog.BasicMessageOK(GameInstance.LanguageManager.GetText(GameText.TITLE_LOGIN_SUCCESS),
                        callback =>
                    {
                        GameInstance.PlayerCtrl.SetPlayerInfo(result.player);
                        ActivePanel(ELoginPanel.LOGIN);

                        accountNameText.text = GameInstance.LanguageManager.GetText(GameText.TITLE_WELLCOME) + 
                                               GameInstance.PlayerCtrl.playerInfo.playerName;
                    });
                    
                });
            }); 
        }
        private void LoadNewScene()
        {
            SceneManager.LoadScene(loadSceneName); 
        }  
        public void OnButtonClick_LoginName()
        {
            GameInstance.GameService.LoginAnonymous(name, result =>
            {
                result.BasicMessageErrorService(success:()=>
                { 
                    AccountLogin();
                    ActivePanel(ELoginPanel.LOGIN);
                });
               
            });
        }
        public void OnButtonClick_SignUp()
        {
            gameRegister.Open();
            gameRegister.OnRegisterSuccess+=AccountLogin;
            isOpenRegister = true;
            ActiveTabToStart(false);
        }
        public void OnButtonClick_Login()
        {
            LoadNewScene();
        }
        public void OnButtonClick_Logout()
        {
           GameInstance.GameService.LogoutAnonymous();
           Dialog.BasicMessageOK(GameInstance.LanguageManager.GetText(GameText.TITLE_LOGOUT),
               result =>
               {
                   ActivePanel(ELoginPanel.LOGIN_OR_REGISTER);
               });
        }
        public void OnButtonClick_Exit()
        {
            isOpenRegister = false;
            ActiveTabToStart(true);
        }

        private void ActivePanel(ELoginPanel panel)
        {
            switch (panel)
            {
                case ELoginPanel.NONE:
                    panelLogin.SetActive(false);
                    panelLoginOrRegister.SetActive(false);
                    break;
                case ELoginPanel.LOGIN:
                    panelLogin.SetActive(true);
                    panelLoginOrRegister.SetActive(false);
                    break;
                case ELoginPanel.LOGIN_OR_REGISTER:
                    panelLogin.SetActive(false);
                    panelLoginOrRegister.SetActive(true);
                    break;
            }
        }
        private void ActiveTabToStart(bool active)
        {
            tabToStartGameObject.SetActive(active); 
        }
         
        #region Helper
        private PointerEventData _pointerEventData;
        private List<RaycastResult> _results;

        private bool IsOverUi(Vector2 pos)
        {
            _pointerEventData = new PointerEventData(EventSystem.current){position = pos};
            _results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(_pointerEventData, _results);
            return _results.Count > 0;
        } 
        #endregion
       
    }
}
