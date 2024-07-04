using DG.Tweening;
using UnityEngine;

namespace Script.Ui
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupAlphaLoop : MonoBehaviour
    { 
        CanvasGroup canvasGroup;
        void Start()
        {
            canvasGroup = this.GetComponent<CanvasGroup>();
            canvasGroup.DOFade(0, 2).SetLoops(-1, LoopType.Yoyo);
        } 
    }
}
