using System;
using UnityEngine;

namespace Script.DialogBox
{
	public enum DialogResult
	{
		Yes,
		No,
		OK,
		Cancel,
		Abort,
		Retry,
		Ignore,
	}

	public class ButtonEntryInfo
	{
		public string text { get; private set; }
		public DialogResult type { get; private set; }

		public ButtonEntryInfo(string text, DialogResult type)
		{
			this.text = text;
			this.type = type;
		}
	}

	public abstract class BaseDialogBox : MonoBehaviour 
	{
	   protected static BaseDialogBox Create(string prefebPath)
	    {
		    GameObject prefeb = Resources.Load<GameObject>(prefebPath);
		    GameObject obj = Instantiate(prefeb) as GameObject;
		    obj.SetActive(true);
		    DontDestroyOnLoad(obj);
		    BaseDialogBox dialogBox = obj.GetComponentInChildren<BaseDialogBox>();
		    return dialogBox;
	    }
	    [SerializeField] protected GameObject rootCanvas = null; 
		private DialogResult dialogResult = DialogResult.OK;
		private Action<DialogResult> callback = null;
 
		protected void OnButtonClick_DialogButton(DialogResult clickedButton)
		{
			this.dialogResult = clickedButton; 
			ForceClose(true);
		} 
        protected void Init(Action<DialogResult> callback)
        {
	        this.callback = callback;
        }

        public void ForceClose(bool alsoInvokeCallback)
		{
			if(alsoInvokeCallback && callback != null)
				callback(dialogResult);
			
			Destroy(rootCanvas);
		}   
		
    }
}
