using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public abstract class BaseTab<T> : SerializedMonoBehaviour where T : BaseInfo
    { 
        [SerializeField] private Image iconSelected;
        [SerializeField,ReadOnly] public T info { get; private set; }
        public event Action OnDisableOtherTab;
        public event Action<T> OnClickTab;

        protected bool isLockClick = false;
        public void RefreshUi(T bInfo)
        { 
            info = bInfo; 
            if(iconSelected!=null)
                iconSelected.gameObject.SetActive(false);
            InitUi();
        } 
        
        public void ButtonClick()
        { 
            if(isLockClick) return;
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
