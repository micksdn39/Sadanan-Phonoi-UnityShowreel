using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public abstract class BaseTab<T> : SerializedMonoBehaviour where T : BaseInfo
    { 
        [SerializeField] private Image iconSelected;
        [SerializeField,ReadOnly] protected T info { get; private set; }
        public event Action OnDisableOtherTab;
        public event Action<T> OnClickTab;
        
        public void RefreshUi(T bInfo)
        { 
            info = bInfo; 
            if(iconSelected!=null)
                iconSelected.gameObject.SetActive(false);
            InitUi();
        } 
        
        public void ButtonClick()
        { 
            OnDisableOtherTab?.Invoke();
            OnClickTab?.Invoke(info);
            
            if(iconSelected!=null)
                iconSelected.gameObject.SetActive(true);

            OnClick();
        }

        public void DisableUi()
        {
            if(iconSelected!=null)
                iconSelected.gameObject.SetActive(false);
            
            Disable();
        }
        protected abstract void InitUi();
        protected abstract void OnClick(); 
        protected abstract void Disable();
    }
}
