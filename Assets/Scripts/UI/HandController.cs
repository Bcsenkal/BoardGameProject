using System;
using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UI
{
    public class HandController : MonoBehaviour
    {
        [SerializeField] private Sprite clickedSprite;
        [SerializeField] private float clickedScale;
        private Image _handImage;
        private Image _handShadowImage;
        private GameObject _circle;
        private GameObject _circle2;
        private RectTransform _handTransform;
        private bool _isClicking;
        void Start()
        {
            _circle = transform.GetChild(0).gameObject;
            _circle2 = transform.GetChild(1).gameObject;
            _handShadowImage = transform.GetChild(2).GetComponent<Image>();
            _handImage = _handShadowImage.transform.GetChild(0).GetComponent<Image>();
            _handTransform = GetComponent<RectTransform>();
        }

        private void OnMouseDown(Vector2 obj)
        {
            OnPointerClick();
        }

        private void ONGridCellClicked(bool arg1, bool arg2, int arg3, Vector3 arg4)
        {
            OnPointerClick();
        }

        private void ONPlayerSoldierSelected(int obj)
        {
            OnPointerClick();
        }

        private void Update()
        {
            var mousePos = Input.mousePosition;
            _handTransform.position = mousePos;
        }


        private void OnPointerClick()
        {
            if (_isClicking) return;
            StartCoroutine(HandClickRoutine(.1f));
        }
        
        IEnumerator HandClickRoutine(float duration)
        {
            _isClicking = true;
            var counter = 0f;
            while (counter<duration)
            {
                var t = QuadEaseOut(counter, 0f, 1f, duration);
                var s = Mathf.Lerp(1f, clickedScale, t);
                _handTransform.localScale = new Vector3(s, s, s);
                counter += Time.deltaTime;
                yield return null;
            }

            var sprite = _handImage.sprite;
            _handImage.sprite = clickedSprite;
            _handShadowImage.sprite = clickedSprite;
            _circle.SetActive(true);
            //_circle2.SetActive(true);
            //yield return new WaitForSeconds(.1f);
            
            counter = 0f;
            
            while (counter<duration)
            {
                var t = QuadEaseOut(counter, 0f, 1f, duration);
                var s = Mathf.Lerp(clickedScale, 1f, t);
                _handTransform.localScale = new Vector3(s, s, s);
                counter += Time.deltaTime;
                yield return null;
            }
            
            _handImage.sprite = sprite;
            _handShadowImage.sprite = sprite;
            _isClicking = false;
        }
        private float QuadEaseOut(float t, float b, float c, float d)
        {
            return -c * (t /= d) * (t - 2) + b;
        }
    }
}
