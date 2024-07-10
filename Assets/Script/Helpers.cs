using UnityEngine;

namespace Script
{
    public class Helpers : MonoBehaviour
    { 
        public void DoDestroy(GameObject gameObject)
        {
            Debug.Log("Destroy "+gameObject.name);
            if(gameObject!=null)
                Destroy(gameObject);
        }
    }
}
