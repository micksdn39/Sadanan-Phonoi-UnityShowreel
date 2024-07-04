using System.Collections.Generic;
using UnityEngine;

namespace Script.Save
{
    public static class ClientSave
    {
        public static void Delete(string key)
        {
            PlayerPrefs.DeleteKey(key);
            Debug.Log("Deleted key: " + key); 
        }

        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Deleted all keys"); 
        }
        public static string Load (string key,string defaultValue = null)
        {
            Debug.Log("Loaded key: " + key);
            return string.IsNullOrEmpty(defaultValue) ? 
                PlayerPrefs.GetString(key) : PlayerPrefs.GetString(key, defaultValue);
        }
        public static T Load<T>(string key, T defaultValue = default)
        {
            var data = PlayerPrefs.GetString(key);
            
            Debug.Log("Loaded key: " + key); 
            return !string.IsNullOrEmpty(data) ? 
                JsonUtility.FromJson<T>(data) : defaultValue;
        }

        public static List<T> Load<T>(params string[] keys)
        { 
            var values = new List<T>(); 
            foreach (var key in keys)
            {
                values.Add(Load<T>(key));
            } 
            Debug.Log("Loaded keys: " + string.Join(", ", keys));
            return values;
        } 
        public static void Save(string key, object value)
        {
            if (value is string s)
            {
                PlayerPrefs.SetString(key, s);
            }
            else
            {
                var data = JsonUtility.ToJson(value);
                PlayerPrefs.SetString(key, data);
            }
            PlayerPrefs.Save();
            Debug.Log("Saved key: " + key); 
        } 
        public static void Save(params (string key, object value)[] values)
        {
            foreach (var (key, value) in values)
            {
                Save(key, value);
            }
        }
    }
}