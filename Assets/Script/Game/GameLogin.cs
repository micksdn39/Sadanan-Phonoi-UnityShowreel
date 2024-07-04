using Script.Save;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Game
{
    public class GameLogin : MonoBehaviour
    { 
        [SerializeField] private string loadSceneName;
        [Space] 
        [SerializeField] private GameObject panelRegister; 
        [SerializeField] private TMP_InputField inputFieldName;
        private void Update()
        {
            OnCheckGetInputMouse(); 
        }
        private void OnCheckGetInputMouse()
        {
            if (Input.GetMouseButtonDown(0))
            {
                string currentPlayerId = ClientSave.Load(SaveKey.PlayerInfo);
                if (string.IsNullOrEmpty(currentPlayerId))
                { 
                    panelRegister.SetActive(true);
                    return;
                } 
                LoadNewScene();

            }
        }
         
        private void LoadNewScene()
        {
            SceneManager.LoadScene(loadSceneName); 
        }

        public void OnButtonClick_GuestLogin()
        {
            
        }

    }
}
