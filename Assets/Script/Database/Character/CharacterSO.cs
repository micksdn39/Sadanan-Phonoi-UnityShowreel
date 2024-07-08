using Script.Enum;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Script.Database.Character
{
    [CreateAssetMenu(fileName = "CharacterDatabase", menuName = "ScriptableObjects/CharacterDatabase")]
    public class CharacterSO : SerializedScriptableObject
    {
        [SerializeField] public int characterId { get; private set; }
        [field: TextArea,SerializeField] public string characterName { get; private set; }
        [SerializeField] public GameEnum.ETier tier { get; private set; }
        
        [PreviewField(100, ObjectFieldAlignment.Left), SerializeField]
        public Sprite characterIcon { get; private set; }
    }
}
