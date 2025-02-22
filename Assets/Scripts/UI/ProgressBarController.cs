using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ProgressBarController : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        private Slider _progressbar;
        private Coroutine _barRoutine;
        void Start()
        {
            CacheComponents();
        }


        private void CacheComponents()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _progressbar = transform.GetChild(0).GetComponent<Slider>();
        }


        //subscribe to a progression event and pass on the new progression amount (between 0-1) to change the progressbar fill amount
        private void ONProgressValueChange(float newValue)
        {
            if (_barRoutine != null)
            {
                StopCoroutine(_barRoutine);
            }

            newValue = Mathf.Clamp(newValue, 0f, 1f);
            _barRoutine = StartCoroutine(ProgressRoutine(newValue));
        }
        
        IEnumerator ProgressRoutine(float value)
        {
            var counter = 0f;
            var current = _progressbar.value;
            while (counter<=.3f)
            {
                var t = Easings.QuadEaseOut(counter, 0f, 1f, .3f);
                _progressbar.value = Mathf.Lerp(current, value, t);
                counter += Time.deltaTime;
                yield return null;
            }

            _progressbar.value = value;
        }


        //to show and hide the progress bar by changing canvas group alpha value
        private void ShowProgressBar(bool isActive)
        {
            StartCoroutine(AlphaRoutine(isActive));
        }
        
        IEnumerator AlphaRoutine(bool isActive)
        {
            var counter = 0f;
            while (counter<=.4f)
            {
                if (isActive)
                {
                    _canvasGroup.alpha = Easings.QuadEaseOut(counter, 0f, 1f, .4f);
                }
                else
                {
                    _canvasGroup.alpha = 1 - Easings.QuadEaseOut(counter, 0f, 1f, .4f);
                }
                
                counter += Time.deltaTime;
                yield return null;
            }

        }
        
    }
}
