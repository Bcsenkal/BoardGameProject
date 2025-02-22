using System.Collections;
using System.Collections.Generic;
using LitMotion;
using LitMotion.Extensions;
//using Firebase.Unity;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FailScreenController : MonoBehaviour
    {
        
       
        private CanvasGroup _canvasGroup;
        private RectTransform _background;
        private RectTransform _failPanel;
        private Button _tryAgainButton;
        private GameObject _tryAgainButtonCavity;
        private Button _reviveButton;

        private bool _isReloadPressed;
        private int _revivePrice;
        private TextMeshProUGUI _revivePriceLabel;
        private TextMeshProUGUI _panelText;
        
        private GameObject _coinPrice;
        private GameObject _adPrice;
        private bool _isAdClicked;
        private bool _hasEnoughGold;
        private bool _alreadyFailed;
        private bool _levelEnded;
        void Start()
        {
            PopulateObjects();
            EventManager.Instance.ONLevelEnd += ONLevelEnd;
             
        }

        private void ONNpcGotShot(Vector3 arg1, bool isAlien)
        {
            if (isAlien) return;
            _panelText.text = "HUMAN DOWN!";
        }

        private void PopulateObjects()
        {
            //_revivePrice = FirebaseManager.GetRevivePrice();
            _canvasGroup = GetComponent<CanvasGroup>();
            _background = transform.GetChild(0).GetComponent<RectTransform>();
            _failPanel = transform.GetChild(1).GetComponent<RectTransform>();
            _panelText = _failPanel.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
            _panelText.text = "Target Has Escaped!\nYou Failed!";
            
            _reviveButton = _failPanel.transform.GetChild(4).GetComponent<Button>();
            _coinPrice = _reviveButton.transform.GetChild(1).gameObject;
            _adPrice = _reviveButton.transform.GetChild(2).gameObject;
            _tryAgainButton = _failPanel.transform.GetChild(5).GetComponent<Button>();
            _tryAgainButtonCavity = _failPanel.transform.GetChild(1).gameObject;
            _revivePriceLabel = _coinPrice.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _revivePriceLabel.text = _revivePrice.ToString();
            
            _reviveButton.onClick.AddListener(RevivePlayer);
            _tryAgainButton.onClick.AddListener(ReloadLevel);
            
        }

        private void RevivePlayer()
        {
            if (_hasEnoughGold)
            {
                _alreadyFailed = false;
                ResourceManager.Instance.SpendCoin(_revivePrice);
                ResourceManager.Instance.AddMoveCount(20);
                EventManager.Instance.OnONRevive();
                CloseIt();
                AudioManager.Instance.PlayMusic(true);
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
            _alreadyFailed = false;
            //EventManager.Instance.ONRewardedAdShown -= ONRewardedAdShown;
            //EventManager.Instance.OnONRevive();
            //CardManager.Instance.ONLevelStart();
            CloseIt();
            AudioManager.Instance.PlayMusic(true);
        }

        private void CloseIt()
        {
            //_closeButtonRect.localScale = new Vector3(.8f, .8f, .8f);
        }

        private void ONLevelEnd(bool isSuccess)
        {
            if (_levelEnded) return;
            _levelEnded = true;
            if (isSuccess) return;
            if (_alreadyFailed) return;
            _alreadyFailed = true;
            //_hasEnoughGold = ResourceManager.Instance.HasEnoughGold(_revivePrice);
            _coinPrice.SetActive(_hasEnoughGold);
            _adPrice.SetActive(!_hasEnoughGold);
            ShowFailScreen();
        }
       
        private void ShowFailScreen()
        {
            AudioManager.Instance.PlayFailMusic();
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
            var value = 0f;
            LMotion.Create(0f, 1f, .35f)
                .WithEase(Ease.OutQuad)
                .Bind(x => _canvasGroup.alpha = x);

            LMotion.Create(Vector3.zero, Vector3.one, .35f)
                .WithEase(Ease.OutBack)
                .WithDelay(.5f)
                .BindToLocalScale(_failPanel);
            
            Invoke(nameof(ShowTryAgainButton),1.5f);
        }

        private void ShowTryAgainButton()
        {
            _tryAgainButtonCavity.SetActive(true);
            _tryAgainButton.gameObject.SetActive(true);
        }
        

        private void ReloadLevel()
        {
            _tryAgainButton.onClick.RemoveListener(ReloadLevel);
            _tryAgainButtonCavity.SetActive(false);
            _tryAgainButton.gameObject.SetActive(false);
            _reviveButton.onClick.RemoveListener(RevivePlayer);
            //EventManager.Instance.OnONLevelReload();
            GameManager.Instance.ReloadLevel();
        }
        
        
    }
}
