using System.Collections.Generic;
using Unity.VisualScripting;

namespace Script.Language
{
    public static class GameText
    {
        private const string TEXT_PREFIX = "TEXT_";  
        public const string TITLE_NOW_LOADING = TEXT_PREFIX + "TITLE_NOW_LOADING";
        public const string TITLE_TAB_START = TEXT_PREFIX + "TITLE_TAB_START";
        public const string TITLE_INFO_DIALOG = TEXT_PREFIX + "TITLE_INFO_DIALOG";
        public const string TITLE_ERROR_DIALOG = TEXT_PREFIX + "TITLE_ERROR_DIALOG";
        public const string TITLE_PROFILE_NAME_DIALOG = TEXT_PREFIX + "TITLE_PROFILE_NAME_DIALOG";
        public const string CONTENT_PROFILE_NAME_DIALOG = TEXT_PREFIX + "CONTENT_PROFILE_NAME_DIALOG";
        public const string PLACE_HOLDER_PROFILE_NAME = TEXT_PREFIX + "PLACE_HOLDER_PROFILE_NAME";
        public const string TITLE_CURRENCY_GOLD = TEXT_PREFIX + "TITLE_CURRENCY_GOLD";
        public const string TITLE_CURRENCY_GEM = TEXT_PREFIX + "TITLE_CURRENCY_GEM";
        public const string TITLE_LOGIN = TEXT_PREFIX + "TITLE_LOGIN";
        public const string TITLE_LOGIN_SUCCESS = TEXT_PREFIX + "TITLE_LOGIN_SUCCESS";
        public const string TITLE_LOGOUT = TEXT_PREFIX + "TITLE_LOGOUT";
        public const string TITLE_ACCOUNT = TEXT_PREFIX + "TITLE_ACCOUNT";
        public const string TITLE_GUEST_LOGIN = TEXT_PREFIX + "TITLE_GUEST_LOGIN"; 
        public const string TITLE_GUEST_REGISTER = TEXT_PREFIX + "TITLE_GUEST_REGISTER";
        public const string TITLE_REGISTER = TEXT_PREFIX + "TITLE_REGISTER"; 
        public const string TITLE_REGISTER_SUCCESS = TEXT_PREFIX + "TITLE_REGISTER_SUCCESS";
        public const string TITLE_CONFIRM = TEXT_PREFIX + "TITLE_CONFIRM"; 
        public const string TITLE_PROFILE = TEXT_PREFIX + "TITLE_PROFILE";
        public const string TITLE_SELECTED_PROFILE = TEXT_PREFIX + "TITLE_SELECTED_PROFILE";
        public const string TITLE_SELECTED_TEXT = TEXT_PREFIX + "TITLE_SELECTED_TEXT";
        public const string TITLE_NOTICE = TEXT_PREFIX + "TITLE_NOTICE";
        public const string TITLE_ADVENTURE = TEXT_PREFIX + "TITLE_ADVENTURE";
        public const string TITLE_NAME_CONFIRM = TEXT_PREFIX + "TITLE_NAME_CONFIRM";
        public const string TITLE_VIRTUAL_CURRENCY_PURCHASE = TEXT_PREFIX + "TITLE_VIRTUAL_CURRENCY_PURCHASE";
        public const string TITLE_WELLCOME = TEXT_PREFIX + "TITLE_WELCOME";
        public const string TITLE_SIGN_UP = TEXT_PREFIX + "TITLE_SIGN_UP";
        
        public const string TITLE_BUTTON_OK = TEXT_PREFIX + "TITLE_BUTTON_OK";
        public const string TITLE_BUTTON_YES = TEXT_PREFIX + "TITLE_BUTTON_YES";
        public const string TITLE_BUTTON_CANCEL = TEXT_PREFIX + "TITLE_BUTTON_CANCEL";
    }
    
      public static class DefaultLocale
    {
        private enum ELanguage
        {
            ENG = 0,
            TH = 1
        }
        public static Dictionary<string, string> GetLanguage(string key)
        {
            return Languages[GetCurrentLanguageKey(key)];
        } 
        public static bool IsContainsKey(string key)
        {
            return Languages.ContainsKey(GetCurrentLanguageKey(key));
        }
        private static Dictionary<ELanguage, Dictionary<string, string>> Languages
            = new Dictionary<ELanguage, Dictionary<string, string>>();

        static DefaultLocale()
        {
            Dictionary<string, string> engTexts = new Dictionary<string, string>();
            engTexts.Add(GameText.TITLE_NOW_LOADING, "Now Loading");
            engTexts.Add(GameText.TITLE_TAB_START, "TAB TO START");
            engTexts.Add(GameText.TITLE_INFO_DIALOG, "Info");
            engTexts.Add(GameText.TITLE_ERROR_DIALOG, "Error");
            engTexts.Add(GameText.TITLE_PROFILE_NAME_DIALOG, "Name");
            engTexts.Add(GameText.CONTENT_PROFILE_NAME_DIALOG, "Enter your name");
            engTexts.Add(GameText.PLACE_HOLDER_PROFILE_NAME, "Enter your name...");
            engTexts.Add(GameText.TITLE_CURRENCY_GOLD, "Gold(s)");
            engTexts.Add(GameText.TITLE_CURRENCY_GEM, "Gem(s)"); 
            engTexts.Add(GameText.TITLE_LOGIN, "Login");
            engTexts.Add(GameText.TITLE_LOGIN_SUCCESS, "Login Success");
            engTexts.Add(GameText.TITLE_LOGOUT, "Logout");
            engTexts.Add(GameText.TITLE_ACCOUNT, "Account");
            engTexts.Add(GameText.TITLE_GUEST_LOGIN, "Guest Login");
            engTexts.Add(GameText.TITLE_GUEST_REGISTER, "Guest Register");
            engTexts.Add(GameText.TITLE_REGISTER, "Register");
            engTexts.Add(GameText.TITLE_SIGN_UP, "Sign Up");
            engTexts.Add(GameText.TITLE_CONFIRM, "Confirm");
            engTexts.Add(GameText.TITLE_PROFILE, "Profile");
            engTexts.Add(GameText.TITLE_SELECTED_PROFILE, "Selected Profile");
            engTexts.Add(GameText.TITLE_SELECTED_TEXT, "Select");
            engTexts.Add(GameText.TITLE_NOTICE, "Notice");
            engTexts.Add(GameText.TITLE_REGISTER_SUCCESS, "Register Success");
            engTexts.Add(GameText.TITLE_BUTTON_OK, "OK");
            engTexts.Add(GameText.TITLE_BUTTON_YES, "Yes");
            engTexts.Add(GameText.TITLE_BUTTON_CANCEL, "Cancel");
            engTexts.Add(GameText.TITLE_ADVENTURE, "Adventure");
            engTexts.Add(GameText.TITLE_NAME_CONFIRM, "Confirm Name : ");
            engTexts.Add(GameText.TITLE_VIRTUAL_CURRENCY_PURCHASE, "Virtual Currency Purchase");
            engTexts.Add(GameText.TITLE_WELLCOME, "Welcome :");
            Languages.Add(ELanguage.ENG, engTexts);

            Dictionary<string, string> thTexts = new Dictionary<string, string>();
            thTexts.Add(GameText.TITLE_NOW_LOADING, "กำลังโหลด");
            thTexts.Add(GameText.TITLE_TAB_START, "แตะหน้าจอเพื่อเริ่ม");
            thTexts.Add(GameText.TITLE_INFO_DIALOG, "ข้อมูล");
            thTexts.Add(GameText.TITLE_ERROR_DIALOG, "ผิดพลาด");
            thTexts.Add(GameText.TITLE_PROFILE_NAME_DIALOG, "ชื่อ");
            thTexts.Add(GameText.CONTENT_PROFILE_NAME_DIALOG, "กรุณาใส่ชื่อของท่าน");
            thTexts.Add(GameText.PLACE_HOLDER_PROFILE_NAME, "กรุณาใส่ชื่อของท่าน...");
            thTexts.Add(GameText.TITLE_CURRENCY_GOLD, "ทอง");
            thTexts.Add(GameText.TITLE_CURRENCY_GEM, "อัญมณี"); 
            thTexts.Add(GameText.TITLE_LOGIN, "เข้าสู่ระบบ");
            thTexts.Add(GameText.TITLE_LOGIN_SUCCESS, "เข้าสู่ระบบสำเร็จ");
            thTexts.Add(GameText.TITLE_LOGOUT, "ออกจากระบบ");
            thTexts.Add(GameText.TITLE_ACCOUNT, "บัญชี");
            thTexts.Add(GameText.TITLE_GUEST_LOGIN, "เข้าสู่ระบบบัญชี Guest");
            thTexts.Add(GameText.TITLE_GUEST_REGISTER, "ลงทะเบียนบัญชี Guest");
            thTexts.Add(GameText.TITLE_REGISTER, "ลงทะเบียน");
            thTexts.Add(GameText.TITLE_SIGN_UP, "ลงทะเบียน");
            thTexts.Add(GameText.TITLE_CONFIRM, "ยืนยัน");
            thTexts.Add(GameText.TITLE_PROFILE, "โปรไฟล์");
            thTexts.Add(GameText.TITLE_SELECTED_PROFILE, "เลือกโปรไฟล์ของท่าน");
            thTexts.Add(GameText.TITLE_SELECTED_TEXT, "เลือก");
            thTexts.Add(GameText.TITLE_NOTICE, "ประกาศ");
            thTexts.Add(GameText.TITLE_REGISTER_SUCCESS, "ลงทะเบียนสำเร็จ");
            thTexts.Add(GameText.TITLE_BUTTON_OK, "ตกลง");
            thTexts.Add(GameText.TITLE_BUTTON_YES, "ใช่");
            thTexts.Add(GameText.TITLE_BUTTON_CANCEL, "ยกเลิก");
            thTexts.Add(GameText.TITLE_ADVENTURE, "ผจญภัย");
            thTexts.Add(GameText.TITLE_NAME_CONFIRM, "ยืนยันชื่อ : ");
            thTexts.Add(GameText.TITLE_VIRTUAL_CURRENCY_PURCHASE,"ระบบจำลองซื้อไอเทม");
            thTexts.Add(GameText.TITLE_WELLCOME, "ยินดีต้อนรับ :");
            Languages.Add(ELanguage.TH, thTexts);

        } 
        private static ELanguage GetCurrentLanguageKey(string key)
        {
            if(string.IsNullOrEmpty(key)) return ELanguage.ENG;
            key = key.ToUpper();
            return key switch
            {
                "ENG" => ELanguage.ENG,
                "TH" => ELanguage.TH,
                _ => ELanguage.ENG
            };
        } 
    } 
}
