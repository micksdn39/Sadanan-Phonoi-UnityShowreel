using Script.Database.Character;
using Script.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Character
{
    public class CharacterInventoryTab : BaseTab<CharacterSO>
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
            foreach (var characterPosition in GameInstance.PlayerCtrl.playerInfo.characterPosition)
            {
                if(GameInstance.GameDatabase.GetCharacter(characterPosition.characterId).Guid == info.Guid)
                {
                    MarkAsSelected(true);
                    return;
                }
            }
            MarkAsSelected(false);
        }
        public void OnAddTeamClick(CharacterSO info)
        {
            if(info.Guid == this.info.Guid)
            {
                MarkAsSelected(true);
            }else
            {
                CheckPosition();
            }
        }
        protected override void InitUi()
        {
            characterIcon.sprite = info.characterIcon;
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
