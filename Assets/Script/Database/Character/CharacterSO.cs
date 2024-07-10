using Script.Enum;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Script.Database.Character
{
    [CreateAssetMenu(fileName = "CharacterDatabase", menuName = "ScriptableObjects/CharacterDatabase")]
    public class CharacterSO : SerializedScriptableObject , BaseInfo
    {	
        [SerializeField, HideInInspector] private string guid;
        public string Guid => guid;
        [SerializeField] public int characterId { get; private set; }
        [field: TextArea,SerializeField] public string characterName { get; private set; }
        [SerializeField] public GameEnum.ETier tier { get; private set; }
        
        [PreviewField(100, ObjectFieldAlignment.Left), SerializeField]
        public Sprite characterIcon { get; private set; }
        [PreviewField(100, ObjectFieldAlignment.Left), SerializeField]
        public GameObject characterPrefab { get; private set; }
        
        void OnValidate()
        {
            var path = AssetDatabase.GetAssetPath(this);
            guid = AssetDatabase.AssetPathToGUID(path);
        }
        
        public GameObject InstantiatePrefab(Transform parent)
        { 
           return GameObject.Instantiate(characterPrefab, parent);
        }
    }
}
