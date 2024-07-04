using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public interface BaseInfo
    { 
    }
    public abstract class BaseCtrl<TOne,TTwo> : MonoBehaviour where TOne : BaseTab<TTwo> where TTwo : BaseInfo
    {   
        [SerializeField,Header("Tab")] protected Transform content;
        [SerializeField,Header("Tab")] protected TOne tabPrefab;
        [SerializeField,Header("Tab")] protected List<TOne> listOfTabs;
        [Space]
        [SerializeField,Header("Info")] protected List<TTwo> listOfInfo; 
 
        private void InitTab()
        {    
            DisableTab();

            tabPrefab.gameObject.SetActive(false); 
            listOfTabs ??= new List<TOne>();
            int index = -1;
            foreach (var item in listOfInfo)
            {
                index++;
                if (listOfTabs.Count > index)
                {
                    listOfTabs[index].RefreshUi(item);
                    listOfTabs[index].gameObject.SetActive(true);
                    continue;
                } 
                var tab = Instantiate(tabPrefab, content);
                tab.RefreshUi(item);
                tab.OnDisableOtherTab += DisableTab;
                tab.OnClickTab += OnClickTab;
                tab.gameObject.SetActive(true); 
                listOfTabs.Add(tab);
            }

            InitTabCallback();
        }
        private void DisableTab()
        {
            if(listOfTabs==null) return;
            foreach (var tab in listOfTabs)
            {
                tab.DisableUi();
            }
        }
 
        public void RefreshUi()
        { 
            InitInfo(InitTab,InitFailed);
        }
        public void DisableUi()
        {
            if(listOfTabs==null) return;
            foreach (var tab in listOfTabs)
            {
               tab.gameObject.SetActive(false);
            } 
            Disable();
        } 
        private void InitFailed()
        {
            Debug.Log("Failed to init Info");
        }
        protected abstract void InitTabCallback();
        protected abstract void InitInfo(Action Success=null, Action Failed=null); 
        protected abstract void Disable();
        protected abstract void OnClickTab(TTwo info);
        
    }
}
