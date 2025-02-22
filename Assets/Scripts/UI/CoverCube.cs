using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

namespace UI
{
    public class CoverCube : MonoBehaviour
    {
        
        void Start()
        {
            var rect = GetComponent<RectTransform>();
            var startPosition = rect.localPosition;


            LMotion.Create(startPosition, Vector3.zero, .5f)
                .WithEase(Ease.OutQuad)
                .BindToLocalPosition(rect);
        }

        
    }
}
