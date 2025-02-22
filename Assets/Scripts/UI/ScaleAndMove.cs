using System;
using UnityEngine;

namespace Utilities
{
    public class ScaleAndMove : MonoBehaviour
    {
        private void OnEnable()
        {
            
            var targetY = transform.localPosition.y + 100f;
            
            Invoke(nameof(ClosePanel),2f);
        }

        private void ClosePanel()
        {
            transform.localScale=Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}
