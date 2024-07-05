using Script.Database;
using Script.Service;
using UnityEngine;

namespace Script.Game
{
    public class GameInstance : MonoBehaviour
    {  
        public static GameInstance Singleton { get; private set; }
        
        public GameDatabase gameDatabase; 
        public static GameDatabase GameDatabase { get; private set; }
        
        public GameService gameService;
        public static GameService GameService { get; private set; }
        
        private void Awake()
        {
            if (Singleton != null)
            {
                Destroy(gameObject);
                return;
            }
            Singleton = this;
            DontDestroyOnLoad(gameObject); 
            
            GameDatabase = gameDatabase;
            GameService = gameService;
        }

    }
}
