using System;
using Script.Database.Character;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Character
{
    public class CharacterInventoryTab : BaseTab<CharacterSO>
    {
        [SerializeField] public Image characterIcon;
        [SerializeField] public Image selectedIcon;
        protected override void InitUi()
        {
            characterIcon.sprite = info.characterIcon;
            selectedIcon.gameObject.SetActive(false);
        }

        protected override void OnClick()
        {
            selectedIcon.gameObject.SetActive(true);
        }

        protected override void Disable()
        {
        }
    }
}
