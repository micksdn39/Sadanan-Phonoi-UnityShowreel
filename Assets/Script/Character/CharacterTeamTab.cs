using System; 
using Script.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Character
{
    public class CharacterTeamTab : MonoBehaviour
    {
        [SerializeField] private int positionNumber;
        [Space]
        [SerializeField] private Image characterIcon;
        [SerializeField] private Image availableIcon; 
        public event Action<CharacterTeamTab> OnButtonClickEvent;
        
        private bool _isAvailable; 
        private CharacterInfo _info;
        public void SetInfo(CharacterInfo info)
        {
            _info = info;
            var chSO = GameInstance.GameDatabase.GetCharacter(_info.characterId);
            characterIcon.sprite = chSO.characterIcon; 
            
            SetAvailable(false);
        }  
        public int Position => positionNumber;

        public void SetAvailable(bool active)
        {
            _isAvailable = active;
            availableIcon.gameObject.SetActive(active);
        }
        public void OnButtonClick()
        {
            if(!_isAvailable)return;
            OnButtonClickEvent?.Invoke(this);
        }
    }
}
