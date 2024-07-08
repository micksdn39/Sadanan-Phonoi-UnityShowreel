using System; 
using UnityEngine;

[System.Serializable]
public class CharacterInfo
{ 
    [SerializeField] public int characterId { get; private set; }
    [SerializeField] public int level { get; private set; }
    [SerializeField] public int exp { get; private set; }
    [SerializeField] public int statusPoint { get; private set; } 

    CharacterInfo(){ }
    public CharacterInfo Clone()
    {
        return new CharacterInfo()
        {
            characterId = characterId,
            level = level,
            exp = exp,
            statusPoint = statusPoint
        };
    }

    public CharacterInfo(int id)
    {
        characterId = id;
        level = 0;
        exp = 0;
        statusPoint = 0; 
    }
}
