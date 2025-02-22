using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LeaveScreen : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        private RectTransform _failPanel;
        private Button _tryAgainButton;
        private Button _closeButton;
        private RectTransform _closeButtonRect;

        private bool _isReloadPressed;
        void Start()
        {
            PopulateObjects();
            EventManager.Instance.ONReloadButtonPressed += ONReloadButtonPressed;
        }

        private void ONReloadButtonPressed()
        {
            StartCoroutine(AlphaRoutine(true));
        }

        private void PopulateObjects()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _failPanel = transform.GetChild(1).GetComponent<RectTransform>();
            _closeButton = _failPanel.transform.GetChild(4).GetComponent<Button>();
            _closeButtonRect = _closeButton.GetComponent<RectTransform>();
            _closeButton.onClick.AddListener(CloseIt);
            
            _tryAgainButton = _failPanel.transform.GetChild(3).GetComponent<Button>();
            
            _tryAgainButton.onClick.AddListener(ReloadLevel);
            
        }
        
        private void CloseIt()
        {
            StartCoroutine(PanelScaleRoutine(false));
            _closeButtonRect.localScale = new Vector3(.8f, .8f, .8f);
        }
        IEnumerator AlphaRoutine(bool isOpening)
        {
        
            var counter = 0f;
            while (counter <= .25f)
            {
                var t = Easings.QuadEaseOut(counter, 0f, 1f, .25f);
                if (isOpening)
                {
                    _canvasGroup.alpha = t;
                }
                else
                {
                    _canvasGroup.alpha = 1 - t;
                }
                counter += Time.deltaTime;
                yield return null;
            }

            if (isOpening)
            {
                _canvasGroup.alpha = 1f;
                _canvasGroup.interactable = true;
                _canvasGroup.blocksRaycasts = true;
                StartCoroutine(PanelScaleRoutine(true));
            }
            else
            {
                _canvasGroup.alpha = 0f;
                _canvasGroup.interactable = false;
                _canvasGroup.blocksRaycasts = false;
            }

        }
        
        IEnumerator PanelScaleRoutine(bool scaleUp)
        {
            yield return new WaitForSeconds(.1f);
            var s = -1f;
            var counter = 0f;
            while (counter <= .35f)
            {
                
                if (scaleUp)
                {
                    var t = Easings.easeOutBack(0f,1f,counter/.35f);
                    s = Mathf.LerpUnclamped(0f, .9f, t);
                }
                else
                {
                    var t = Easings.easeInBack(0f,1f,counter/.35f);
                    s = Mathf.LerpUnclamped(.9f, 0f, t);
                }

                _failPanel.localScale = new Vector3(s, s, s);
                counter += Time.deltaTime;
                yield return null;
            }

            if (scaleUp)
            {
                _failPanel.localScale = new Vector3(.9f, .9f, .9f);
                yield return new WaitForSeconds(.5f);
                _closeButton.gameObject.SetActive(true);

            }
            else
            {
            
                StartCoroutine(AlphaRoutine(false));
                _failPanel.localScale = new Vector3(0f, 0f, 0f);
                _closeButton.gameObject.SetActive(false);
            }



        }
        

        private void ReloadLevel()
        {
            _tryAgainButton.onClick.RemoveListener(ReloadLevel);
            EventManager.Instance.OnONHeartDecrease();
            //EventManager.Instance.OnONLevelReload();
            GameManager.Instance.ReloadLevel();
        }
    }
}
