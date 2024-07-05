using System;
using TMPro;
using UnityEngine;

namespace Script.DialogBox
{
    public class BasicMessageBoxInfo
    {
        public string message { get; private set; }
        public ButtonEntryInfo[] list { get; private set; }  
        public BasicMessageBoxInfo(string message, ButtonEntryInfo buttonEntryInfo)
        {
            this.message = message;
            this.list = new ButtonEntryInfo[] { buttonEntryInfo };
        }
        public BasicMessageBoxInfo(string message, ButtonEntryInfo[] list)
        {
            this.message = message;
            this.list = list;
        }
    }
    public class BasicMessageBox : BaseDialogBox
    {
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] private Transform rootButtons;
        [SerializeField] private DialogButton prefebBtnYes;
        [SerializeField] private DialogButton prefebBtnNo;
 
        public static BasicMessageBox Show(BasicMessageBoxInfo messageInfo, Action<DialogResult> callback)
        {
            BaseDialogBox box = Create("Dialog/(prefeb) BasicMessageBox");
            ((BasicMessageBox)box)._Show(messageInfo.message , callback,messageInfo.list);
            return (box as BasicMessageBox);
        }

        public static void Close()
        {
          //  box.ForceClose(true);
        }
        private void _Show(string message, Action<DialogResult> callback,ButtonEntryInfo[] list)
        {
            this.Init(callback);
            messageText.text = message; 
            prefebBtnYes.gameObject.SetActive(false);
            prefebBtnNo.gameObject.SetActive(false);
            ProduceButtons(list);
        }
        private void ProduceButtons(ButtonEntryInfo[] list)
        {
            foreach(ButtonEntryInfo i in list)
            {
                DialogButton targetPrefeb = null;
                if(	i.type == DialogResult.OK || i.type == DialogResult.Yes)
                {
                    targetPrefeb = prefebBtnYes;
                }
                else
                {
                    targetPrefeb = prefebBtnNo;
                }

                DialogButton btn = Instantiate(targetPrefeb , rootButtons) as DialogButton;
                btn.gameObject.SetActive(true);
                btn.transform.localScale = Vector3.one;
                btn.OnClick += OnButtonClick_DialogButton;
                btn.Init(i.text , i.type);
            }
        }
    }
}
