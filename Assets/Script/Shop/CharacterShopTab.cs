using Script.Service.Character;
using TMPro;
using UnityEngine; 

namespace Script.Shop
{
    public class CharacterShopTab : BaseTab<Gashapon>
    {
        [SerializeField] private TextMeshProUGUI gashaponName;
        protected override void InitUi()
        { 
            gashaponName.text = info.gashaName;
        }

        protected override void OnClick()
        { 
            
        }

        protected override void Disable()
        { 
        }
    }
}
