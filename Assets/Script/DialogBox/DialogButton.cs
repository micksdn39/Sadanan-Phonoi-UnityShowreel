using System;
using TMPro;
using UnityEngine;

namespace Script.DialogBox
{
    public class DialogButton : MonoBehaviour
    {
        
        [SerializeField] private DialogResult buttonType = DialogResult.Yes; 
        [SerializeField] private TMP_Text text = null;
        public Action<DialogResult> OnClick {get; set;}

        public void Init(string btnText , DialogResult buttonType)
        {
            this.buttonType = buttonType;
            if(text != null)
                text.text = btnText; 
        }

        public void OnButtonClicked()
        { 
            OnClick?.Invoke(buttonType);
        }
        
    }
}
