using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using AssetKits.ParticleImage;
using Cysharp.Threading.Tasks;
using LitMotion;

//using Firebase.Unity;

namespace UI
{
    public class SuccessScreenController : MonoBehaviour
    {
       
        private RectTransform _background;
        private RectTransform _levelCompleteRect;
        private GameObject _coins;
        private TextMeshProUGUI _coinAmountText;
        
        private RectTransform _continueButton;
        private CanvasGroup _canvasGroup;
        
        private ParticleImage _highlightLeft;
        private ParticleImage _highlightRight;
        private ParticleImage _shine;
        private bool _hasCalled;
        private int _rewardAmount;
        
        
        //if there will be a money earned animation at the end uncomment lines for Money earned
        //[SerializeField] private TextMeshProUGUI earnedMoneyText;
        //private RectTransform _moneyEarned;

        
        private List<RectTransform> _rectList;
        void Start()
        {
            PopulateObjects();
            EventManager.Instance.ONLevelEnd += OnLevelEnd;
            //EventManager.Instance.ONLevelSuccessReward += ONLevelSuccessReward;
        }

        


        private void PopulateObjects()
        {
            _background = transform.GetChild(0).GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _levelCompleteRect = transform.GetChild(3).GetComponent<RectTransform>();
            _shine = transform.GetChild(2).GetComponent<ParticleImage>();
            _highlightLeft = transform.GetChild(5).GetComponent<ParticleImage>();
            _highlightRight = transform.GetChild(6).GetComponent<ParticleImage>();
            _continueButton = transform.GetChild(4).GetComponent<RectTransform>();
            _coins = transform.GetChild(1).gameObject;
            _coinAmountText = _coins.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            _continueButton.GetComponent<Button>().onClick.AddListener(OpenNextLevel);
            _rectList = new List<RectTransform>()
            {
                _background,
                _levelCompleteRect,
                _continueButton,
            };
            
            
        }





        
        //Level End Event Callback
        private void OnLevelEnd(bool isSuccess)
        {
            if (!isSuccess) return;
            EventManager.Instance.ONLevelEnd -= OnLevelEnd;
            _rewardAmount = ResourceManager.Instance.GetRewardAmount();
            _coinAmountText.text = _rewardAmount.ToString();
            Invoke(nameof(ShowSuccessScreen), 1f);
        }


       
        private void ShowSuccessScreen()
        {
            //Invoke(nameof(ShowTotalAmount),1f);
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            AudioManager.Instance.PlayWinMusic();
            ShowSuccessTask();
        }
        
        async UniTask ShowSuccessTask()
        {
            //Turn on canvas alpha
            
            await LMotion.Create(0f, 1f, .25f)
                .WithEase(Ease.OutQuad)
                .Bind(x => _canvasGroup.alpha = x);
            
            _canvasGroup.alpha = 1f;
            ResumeEndScreen();
            
        }
        
        private void ResumeEndScreen()
        {
            ResumeTask();
        }

        async UniTask ResumeTask()
        {
            var counter = 0f;
            while (counter<=.35f)
            {
                var t = Easings.easeOutBack(0f,1f,counter/.35f);
                var s = Mathf.LerpUnclamped(0f, 1f,t);
                _levelCompleteRect.localScale = new Vector3(s, s, s);
                counter += Time.deltaTime;
                await UniTask.Yield();
            }
            
            _highlightLeft.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(0.25f), ignoreTimeScale: false);
            _highlightRight.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), ignoreTimeScale: false);
            
            counter = 0f;
            while (counter<=.25f)
            {
                var t = Easings.easeOutBack(0f,1f,counter/.25f);
                var s = Mathf.LerpUnclamped(0f, 1f,t);
                _continueButton.localScale = new Vector3(s, s, s);
                counter += Time.deltaTime;
                await UniTask.Yield();
            }
        }

        

        private void OpenNextLevel()
        {
            _continueButton.GetComponent<Button>().onClick.RemoveListener(OpenNextLevel);
            GameManager.Instance.OpenNextLevel();
        }
    }
}
