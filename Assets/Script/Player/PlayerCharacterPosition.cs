using Script.Game;
using UnityEngine;

namespace Script.Player
{
    [System.Serializable]
    public class PlayerCharacterPosition
    {
        [SerializeField] public int position;
        [SerializeField] public int characterId; 
        private GameObject _prefab; 
        
        public void InitPrefab()
        {
          
        }
        public void SetCharacterId(int id)
        {
            characterId = id;
        }  
    }
} 