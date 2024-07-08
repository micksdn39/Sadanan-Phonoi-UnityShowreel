using Script.Database;
using Script.Language;
using Script.Player;
using Script.Service;
using UnityEngine;

namespace Script.Game
{
    public class GameInstance : MonoBehaviour
    {
        public static bool IsInitialized { get; private set; } = false;
        public static GameInstance Singleton { get; private set; }
        
        public GameDatabase gameDatabase; 
        public static GameDatabase GameDatabase { get; private set; }
        
        public GameService gameService;
        public static GameService GameService { get; private set; }
        
        public LanguageManager languageManager;
        public static LanguageManager LanguageManager { get; private set; }

        public PlayerCtrl playerCtrl;
        public static PlayerCtrl PlayerCtrl { get; private set; }

        private void Awake()
        {
            if (Singleton != null)
            {
                Destroy(gameObject);
                return;
            }

            IsInitialized = true;
            Singleton = this;
            DontDestroyOnLoad(gameObject); 
            
            GameDatabase = gameDatabase;
            GameService = gameService;
            LanguageManager = languageManager;
            PlayerCtrl = playerCtrl;
        }

    }
}
