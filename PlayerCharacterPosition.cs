using UnityEngine;

[System.Serializable]
public class PlayerCharacterPosition
{
    [SerializeField] public int position;
    [SerializeField] public int characterId; 
      
    public void SetCharacterId(int id)
    {
        characterId = id;
    }  
} 
