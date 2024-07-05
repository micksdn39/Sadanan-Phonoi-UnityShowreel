using Script.Save; 
using UnityEngine;
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
            SceneManager.LoadScene(loadSceneName); 
        }  
        public void OnButtonClick_Exit()
        {
            isOpenRegister = false;
            tabToStartGameObject.SetActive(true);
        }
    }
}
