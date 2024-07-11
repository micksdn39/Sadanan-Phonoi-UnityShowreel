using System.Collections.Generic;
using Script.DialogBox;
using Script.Language;
using Script.Save;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Script.Game
{
    public class GameLogin : SerializedMonoBehaviour
    { 
        [SerializeField] private Dictionary<ELoginRoot,GameObject> menuRoot;   
        [Space]
        [SerializeField] private string loadSceneName; 
        [Space]
        [SerializeField] private GameRegister gameRegister;
        [SerializeField] private GameObject tabToStartGameObject;  
        [SerializeField] private TextMeshProUGUI accountNameText;
        [SerializeField] private TMP_InputField inputName;
        
        private bool _isOpenRegister = false;
        private string _name;

        private enum ELoginRoot
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
                this._name = name;
            } 
            ActivePanel(ELoginRoot.NONE);
        } 
        private void Update()
        {
            if (_isOpenRegister) return;
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
                    ActivePanel(ELoginRoot.LOGIN_OR_REGISTER);
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
                        ActivePanel(ELoginRoot.LOGIN);

                        accountNameText.text = GameInstance.LanguageManager.GetText(GameText.TITLE_WELLCOME) + 
                                               GameInstance.PlayerCtrl.playerInfo.playerName;
                    });
                    
                });
            }); 
        }

        #region ButtonClick 
        public void OnButtonClick_LoginName()
        {
            GameInstance.GameService.LoginAnonymous(_name, result =>
            {
                result.BasicMessageErrorService(success:()=>
                { 
                    AccountLogin();
                    ActivePanel(ELoginRoot.LOGIN);
                });
               
            });
        }
        public void OnButtonClick_SignUp()
        {
            gameRegister.Init(AccountLogin); 
            ActiveTabToStart(false);
        }
        public void OnButtonClick_Login()
        {
            LoadNewScene();
        }
        public void OnButtonClick_Logout()
        {
           GameInstance.GameService.LogoutAnonymous();
           Dialog.BasicMessageOK(GameInstance.LanguageManager.GetText(
                   GameText.TITLE_LOGOUT), result =>
               {
                   ActivePanel(ELoginRoot.LOGIN_OR_REGISTER);
               });
        }
        public void OnButtonClick_Exit()
        { 
            ActiveTabToStart(true);
        }
        #endregion

        private void ActivePanel(ELoginRoot root)
        { 
            foreach (var r in menuRoot) 
                r.Value.SetActive(r.Key == root); 
        }
        private void ActiveTabToStart(bool active)
        {
            tabToStartGameObject.SetActive(active); 
            _isOpenRegister = !active;
        }
         
        #region Helper 
        private void LoadNewScene()
        {
            SceneManager.LoadScene(loadSceneName); 
        }
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
