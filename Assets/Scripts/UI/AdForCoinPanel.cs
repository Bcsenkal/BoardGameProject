using System;
using System.Collections;

using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AdForCoinPanel : MonoBehaviour
    {
    
        private CanvasGroup _canvasGroup;
        private RectTransform _panel;
        private Button _WatchAdButton;
        private Button _closeButton;
        private RectTransform _closeButtonRect;
        
        private bool _isAdClicked;
        private int _coinCount;
        private int _adCoinReward;
        private bool _isOpening;
        private TextMeshProUGUI _rewardTextLabel;

        private void Start()
        {
            PopulateObjects();
            //EventManager.Instance.ONOpenAdCoinReward += ONOpenAdCoinReward;
        }

        private void ONOpenAdCoinReward()
        {
            if (_isOpening) return;
            _isOpening = true;
            StartCoroutine(AlphaRoutine(true));
            //_coinCount = ResourceManager.Instance.GetCurrentGold();
            _coinCount += _adCoinReward;
        }

        private void PopulateObjects()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _panel = transform.GetChild(1).GetComponent<RectTransform>();
            _closeButton = _panel.transform.GetChild(4).GetComponent<Button>();
            _closeButton.onClick.AddListener(CloseIt);
            _WatchAdButton = _panel.transform.GetChild(3).GetComponent<Button>();
            _WatchAdButton.onClick.AddListener(ShowAds);
            //_adCoinReward = FirebaseManager.GetAdCoinReward();
            _rewardTextLabel = _panel.transform.GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>();
            _rewardTextLabel.text = _adCoinReward.ToString();
        }
        
        private void ShowAds()
        {
            _isAdClicked = true;
            //EventManager.Instance.ONRewardedAdShown += ONRewardedAdShown;
            //MaxController.Instance.ShowRewardedAd();
        }

        private void ONRewardedAdShown()
        {
            if (!_isAdClicked) return;
            _isAdClicked = false;
            //EventManager.Instance.ONRewardedAdShown -= ONRewardedAdShown;
            //EventManager.Instance.ONOnSetCurrentCoin(_coinCount,true);
            //ResourceManager.Instance.AddCoin(_adCoinReward);
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
            while (counter <= .2f)
            {
                var t = Easings.QuadEaseOut(counter, 0f, 1f, .2f);
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
            while (counter <= .3f)
            {
                
                if (scaleUp)
                {
                    var t = Easings.easeOutBack(0f,1f,counter/.3f);
                    s = Mathf.LerpUnclamped(0f, .9f, t);
                    
                }
                else
                {
                    var t = Easings.easeInBack(0f,1f,counter/.3f);
                    s = Mathf.LerpUnclamped(.9f, 0f, t);
                }

                _panel.localScale = new Vector3(s, s, s);
                counter += Time.deltaTime;
                yield return null;
            }

            if (scaleUp)
            {
                _panel.localScale = new Vector3(.9f, .9f, .9f);
                yield return new WaitForSeconds(.25f);
                _closeButton.gameObject.SetActive(true);
                _isOpening = false;
            }
            else
            {
            
                StartCoroutine(AlphaRoutine(false));
                _panel.localScale = new Vector3(0f, 0f, 0f);
                _closeButton.gameObject.SetActive(false);
            }



        }

        
    }
}
