using UnityEngine;

namespace UI
{
    public class MouseFollower : MonoBehaviour
    {
        private RectTransform _rect;
        private Vector3 _buttonDownScale = new Vector3(.9f, .9f, .9f);
        private Vector3 _buttonUpScale = new Vector3(1f, 1f, 1f);
        void Start()
        {
            _rect = GetComponent<RectTransform>();
        }

        
        void Update()
        {
            _rect.position = Input.mousePosition;

            if (Input.GetMouseButtonDown(0))
            {
                _rect.localScale = _buttonDownScale;
            } else if (Input.GetMouseButtonUp(0))
            {
                _rect.localScale = _buttonUpScale;
            }
        }
    }
}
