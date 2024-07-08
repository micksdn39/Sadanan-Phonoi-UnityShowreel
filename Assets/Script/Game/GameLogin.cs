using System.Collections.Generic;
using Script.Player;
using Script.Save; 
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Script.Game
{
    public class GameLogin : MonoBehaviour
    { 
        [SerializeField] private string loadSceneName; 
        [SerializeField] private GameObject tabToStartGameObject;  
        [SerializeField] private GameRegister gameRegister;
        
        private bool isOpenRegister = false; 
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
                
                string currentPlayerId = ClientSave.Load(SaveKey.PlayerInfo);
                if (string.IsNullOrEmpty(currentPlayerId))
                { 
                    gameRegister.Open();
                    gameRegister.OnRegisterSuccess+=LoadNewScene;
                    isOpenRegister = true;
                    tabToStartGameObject.SetActive(false);
                    return;
                }  
                LoadNewScene(); 
            }
        } 
        private void LoadNewScene()
        {
            GameInstance.GameService.LoginAnonymous(result =>
            {
                GameInstance.PlayerCtrl.SetPlayerInfo(result.player);
                
                SceneManager.LoadScene(loadSceneName); 
            }); 
        }  
        public void OnButtonClick_Exit()
        {
            isOpenRegister = false;
            tabToStartGameObject.SetActive(true);
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
    }
}
