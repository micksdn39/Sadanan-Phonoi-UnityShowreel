using Script.Database.Character;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Shop
{
    public class GashaponTab : BaseTab<CharacterSO>
    {
        [SerializeField] private Image characterImage;
        protected override void InitUi()
        { 
            characterImage.sprite = info.characterIcon;
        }

        protected override void OnClick()
        { 
        }

        protected override void Disable()
        { 
        }
    }
}