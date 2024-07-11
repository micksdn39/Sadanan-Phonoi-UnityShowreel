using UnityEditor;
using UnityEngine;

namespace Script.Character
{
    [System.Serializable]
    public class CharacterInfo : BaseInfo
    {
        [SerializeField, HideInInspector] private string guid;
        public string Guid => guid;
        [SerializeField] public int characterId;
        [SerializeField] public int level;
        [SerializeField] public int exp;
        [SerializeField] public int statusPoint;
        [SerializeField] public int position;

        CharacterInfo(){ }
        public CharacterInfo Clone()
        {
            return new CharacterInfo()
            {
                characterId = characterId,
                level = level,
                exp = exp,
                statusPoint = statusPoint,
                position = position
            };
        } 
        public CharacterInfo(int id)
        { 
            guid = System.Guid.NewGuid().ToString();
            characterId = id;
            level = Random.Range(1, 10);
            exp = Random.Range(0, 100);
            statusPoint = (exp * level)*2;
            position = 0;
        }
        public void SetCharacterPosition(int pos)
        {
            position = pos;
        }
    }
}
