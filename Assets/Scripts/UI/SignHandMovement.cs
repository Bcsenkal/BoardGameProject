using System;

using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using LoopType = LitMotion.LoopType;

namespace UI
{
    public class SignHandMovement : MonoBehaviour
    {
        private MotionHandle _motionHandle;
        
        private void OnEnable()
        {
            var rect = transform.GetComponent<RectTransform>();
            var targetPos = rect.localPosition;
            targetPos.y -= 24;
            
            _motionHandle = LMotion.Create(rect.localPosition, targetPos, .5f).WithLoops(10, LoopType.Yoyo)
                .BindToAnchoredPosition3D(rect);
        }

        private void OnDisable()
        {
            if (_motionHandle.IsActive())
            {
                
                _motionHandle.Cancel();
            }
        }
    }
}
