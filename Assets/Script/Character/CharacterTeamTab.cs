using System;
using Script.Database.Character;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Character
{
    public class CharacterTeamTab : MonoBehaviour
    {
        [SerializeField] private int positionNumber;
        [SerializeField] private Image characterIcon;
        [SerializeField] private Image availableIcon;

        public event Action<CharacterTeamTab> OnButtonClickEvent;
        private bool isAvailable;

        private CharacterSO info;
        public void SetInfo(CharacterSO characterSo)
        {
            info = characterSo;
            characterIcon.sprite = info.characterIcon;
        }  
        public int Position
        {
            get
            {
                return positionNumber;
            }
        }

        public void SetAvailable(bool active)
        {
            isAvailable = active;
            availableIcon.gameObject.SetActive(active);
        }
        public void OnButtonClick()
        {
            if(!isAvailable)return;
            OnButtonClickEvent?.Invoke(this);
        }
    }
}
