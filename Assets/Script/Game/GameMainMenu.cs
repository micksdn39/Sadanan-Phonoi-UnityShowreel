using System.Collections.Generic; 
using Script.DialogBox;
using Script.Language;
using Script.Ui.MainMenu;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using CharacterInfo = Script.Character.CharacterInfo;

namespace Script.Game
{
    public class GameMainMenu : SerializedMonoBehaviour
    {
        [SerializeField] private string loadSceneName;  
        [Space]
        [SerializeField] private PanelMainMenu panelMainMenu;
        [Space]
        [SerializeField] private Dictionary<int,Transform> dictionaryOfCharacterPosition; 
        [SerializeField] private Dictionary<int,GameObject> dictionaryOfCharacterPrefab = new Dictionary<int, GameObject>();
        private void Start()
        {
            if (!GameInstance.IsInitialized)
            {
                SceneManager.LoadScene(loadSceneName);
                return;
            }
            SubscribePlayerProfile();
            SubscribeCurrency();
            SubscribePlayerCharacterChange();

            UpdateCharacter();
        } 
        private void OnDestroy()
        {
            if(!GameInstance.IsInitialized) return;
            GameInstance.PlayerCtrl.playerInfo.currencyInfo.OnPlayerCurrencyChanged -= panelMainMenu.SetCurrency;
            GameInstance.PlayerCtrl.playerInfo.OnPlayerProfileChanged -= panelMainMenu.SetProfileInfo;
            GameInstance.PlayerCtrl.playerInfo.OnCharacterChanged -= UpdateCharacter;
        } 
        private void SubscribePlayerProfile()
        { 
            panelMainMenu.SetProfileInfo(GameInstance.PlayerCtrl.playerInfo);
            GameInstance.PlayerCtrl.playerInfo.OnPlayerProfileChanged += panelMainMenu.SetProfileInfo; 
        }
        private void SubscribeCurrency()
        {
            panelMainMenu.SetCurrency(GameInstance.PlayerCtrl.playerInfo.currencyInfo);
            GameInstance.PlayerCtrl.playerInfo.currencyInfo.OnPlayerCurrencyChanged += panelMainMenu.SetCurrency; 
        } 
        private void SubscribePlayerCharacterChange()
        { 
            GameInstance.PlayerCtrl.playerInfo.OnCharacterChanged += UpdateCharacter; 
        }

        private void UpdateCharacter()
        {
            var chList = GameInstance.PlayerCtrl.playerInfo.GetCharacterAtPosition();
            foreach (var ch in chList)
            { 
                InstantiatePrefab(ch);
            }
        }
        private void InstantiatePrefab(CharacterInfo info)
        { 
            var characterSO = GameInstance.GameDatabase.GetCharacter(info.characterId);
            var prefab = characterSO.InstantiatePrefab(dictionaryOfCharacterPosition[info.position]);
            if(dictionaryOfCharacterPrefab.ContainsKey(info.position))
                Destroy(dictionaryOfCharacterPrefab[info.position]);
            dictionaryOfCharacterPrefab[info.position] = prefab;
        }
        
        public void OnButtonClick_Logout()
        {
            GameInstance.GameService.LogoutAnonymous();
            Dialog.BasicMessageOK(GameInstance.LanguageManager.GetText(GameText.TITLE_LOGOUT),
                result =>
                {
                    SceneManager.LoadScene(loadSceneName);
                });
        } 
        public void OnButtonClick_Development()
        { 
            Dialog.BasicMessageOK(GameInstance.LanguageManager.GetText(GameText.TITLE_ON_DEVELOPMENT),
                result => { });
        }
    }
}
