using Script.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Character
{
    public class CharacterInventoryTab : BaseTab<CharacterInfo>
    {
        [SerializeField] public Image characterIcon;
        [SerializeField] public Image selectedIcon;

        void MarkAsSelected(bool selected)
        {
            selectedIcon.gameObject.SetActive(selected);
            isLockClick = selected;
        }

        void CheckPosition()
        {
            if (!GameInstance.PlayerCtrl.playerInfo.IsPositionAvailable(info.characterId))
            { 
                MarkAsSelected(true);
                return;
            } 
            MarkAsSelected(false);
        }
        public void OnAddTeamClick(CharacterInfo info)
        {
            if(info.characterId == this.info.characterId)
            {
                MarkAsSelected(true);
            }else
            {
                CheckPosition();
            }
        }
        protected override void InitUi()
        {
            var characterSprite = GameInstance.GameDatabase.GetCharacter(info.characterId).characterIcon;
            characterIcon.sprite = characterSprite;
            selectedIcon.gameObject.SetActive(false);

            CheckPosition();
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
