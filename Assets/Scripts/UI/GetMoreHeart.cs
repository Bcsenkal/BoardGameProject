using System;
using System.Collections;

using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GetMoreHeart : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        private RectTransform _failPanel;
        private Button _refillButton;
        private Button _closeButton;
        private RectTransform _closeButtonRect;

        private TextMeshProUGUI _heartTimerLabel;
        private DateTime _unlockTime;
        private TimeSpan _remainingTime;
        private readonly TimeSpan _zeroTime = TimeSpan.FromSeconds(0.0);
        private bool _canCountDown;
        private float _nextCheck;
        
        private int _heartCount;
        private TextMeshProUGUI _heartCountLabel;

        private bool _isReloadPressed;
        private GameObject _coinPrice;
        private GameObject _adPrice;
        private bool _isAdClicked;
        private bool _hasEnoughGold;
        private int _refillCost;
        private TextMeshProUGUI _refillCostLabel;
        void Start()
        {
            PopulateObjects();
            
        }

        private void ONRanOutOfHeart()
        {
            //_hasEnoughGold = ResourceManager.Instance.HasEnoughGold(_refillCost);
            _coinPrice.SetActive(_hasEnoughGold);
            _adPrice.SetActive(!_hasEnoughGold);
            //_unlockTime = ES3.Load("HeartTimer", DateTime.Now.AddMinutes(30));
            _canCountDown = true;
            _heartCount = 0;
            _heartCountLabel.text = _heartCount.ToString();
            StartCoroutine(AlphaRoutine(true));
        }

        private void Update()
        {
            if (!_canCountDown) return;
            if (Time.time < _nextCheck) return;
            _nextCheck = Time.time + .5f;
            TimerCountDown();
            CheckUnlockStatus();
        }

        

        private void PopulateObjects()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _failPanel = transform.GetChild(1).GetComponent<RectTransform>();
            _closeButton = _failPanel.transform.GetChild(4).GetComponent<Button>();
            _closeButtonRect = _closeButton.GetComponent<RectTransform>();
            _closeButton.onClick.AddListener(CloseIt);
            
            _refillButton = _failPanel.transform.GetChild(3).GetComponent<Button>();
            _coinPrice = _refillButton.transform.GetChild(1).gameObject;
            _adPrice = _refillButton.transform.GetChild(2).gameObject;
            
            _refillButton.onClick.AddListener(RefillHearts);
            _heartTimerLabel = _failPanel.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
            _heartCountLabel = _failPanel.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
            //_refillCost = FirebaseManager.GetRefillPrice();
            _refillCostLabel = _coinPrice.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _refillCostLabel.text = _refillCost.ToString();
        }

        private void TimerCountDown()
        {
            _remainingTime = DateTime.Now.Subtract(_unlockTime);
            _heartTimerLabel.text = _remainingTime.ToString(@"mm\:ss");
        }
        
        private void CheckUnlockStatus()
        {
            var index = _remainingTime.CompareTo(_zeroTime);
            if (index < 0) return;
            _canCountDown = false;
            _heartCount += 1;
            _heartCountLabel.text = _heartCount.ToString();
            //ES3.Save("HeartCount", _heartCount);
            _unlockTime = DateTime.Now.AddMinutes(30);
            //ES3.Save("HeartTimer", _unlockTime);
            _canCountDown = true;
        }

        private void RefillHearts()
        {
            
            if (_hasEnoughGold)
            {
                //ResourceManager.Instance.SpendCoin(_refillCost);
                CloseIt();
            }
            else
            {
                _isAdClicked = true;
                //EventManager.Instance.ONRewardedAdShown += ONRewardedAdShown;
                //MaxController.Instance.ShowRewardedAd();
            }
        }

        private void ONRewardedAdShown()
        {
            if (!_isAdClicked) return;
            _isAdClicked = false;
            //EventManager.Instance.ONRewardedAdShown -= ONRewardedAdShown;
            CloseIt();
        }

        private void CloseIt()
        {
            StartCoroutine(PanelScaleRoutine(false));
            //_closeButtonRect.localScale = new Vector3(.8f, .8f, .8f);
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
                _canCountDown = false;
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
                    _canCountDown = true;
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
            }
            else
            {
            
                StartCoroutine(AlphaRoutine(false));
                _failPanel.localScale = new Vector3(0f, 0f, 0f);
            }



        }
    }
}
