using System; 
using UnityEngine;

[System.Serializable]
public class CharacterInfo
{
    [SerializeField] public int characterId;
    [SerializeField] public int level;
    [SerializeField] public int exp;
    [SerializeField] public int statusPoint;

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
