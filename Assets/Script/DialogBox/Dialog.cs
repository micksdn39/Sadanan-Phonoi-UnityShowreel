using System;
using Script.Game;
using Script.Language;
using Script.Service;

namespace Script.DialogBox
{
    public static class Dialog
    {
        public static void BasicMessageYesNo(string desc,Action<DialogResult> callback)
        {
            string buttonYes = GameInstance.LanguageManager.GetText(GameText.TITLE_BUTTON_YES);
            string buttonCancel = GameInstance.LanguageManager.GetText(GameText.TITLE_BUTTON_CANCEL);
            
            BasicMessageBox.Show(
                new BasicMessageBoxInfo(
                    desc, 
                    new ButtonEntryInfo[]
                    {
                        new ( buttonYes, DialogResult.Yes),
                        new ( buttonCancel, DialogResult.Cancel)
                    }), 
                    callback);
             
        }
        public static void BasicMessageOK(string desc,Action<DialogResult> callback)
        {
            string buttonOK = GameInstance.LanguageManager.GetText(GameText.TITLE_BUTTON_OK); 
            
            BasicMessageBox.Show(
                new BasicMessageBoxInfo(
                    desc,
                    new ButtonEntryInfo(buttonOK,DialogResult.OK)),
                    callback);
             
        }
        public static void BasicMessageErrorService(this GameServiceResult service,Action success=null,Action error=null)
        {
            if(service.Success)
            {
                success?.Invoke();
                return;
            }
            string buttonOK = GameInstance.LanguageManager.GetText(GameText.TITLE_BUTTON_OK);

            BasicMessageBox.Show(
                new BasicMessageBoxInfo(
                    service.error,
                    new ButtonEntryInfo(buttonOK, DialogResult.OK)),
                result =>
                {
                    error?.Invoke();
                });

        }
    }
}
