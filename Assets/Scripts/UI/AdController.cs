using System;
using LitMotion;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class AdController : MonoBehaviour
    {
        [SerializeField] private bool dontShowAd;
        private CanvasGroup _canvasGroup;
        private int _adCondition;
        private int _sceneIndex;
        private bool _adIsShown;
        private bool _justOpened;


        private void Start()
        {
            Invoke(nameof(CheckAdCondition),.1f);
        }

        private void CheckAdCondition()
        {
            if (PlayerPrefs.HasKey("JustOpened"))
            {
                PlayerPrefs.DeleteKey("JustOpened");
                //EventManager.Instance.OnONResumeGamePlay();
                return;
            }
            
            _sceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (_sceneIndex < 5)
            {
                //EventManager.Instance.OnONResumeGamePlay();
                return;
            }
            Invoke(nameof(CacheComponents),.75f);
        }
        
        private void CacheComponents()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.blocksRaycasts = true;
            LMotion.Create(0f, 1f, .5f)
                .WithEase(Ease.OutQuad)
                .Bind(x => _canvasGroup.alpha = x);
            Invoke(nameof(ShowAd),1.5f);
        }
        
        private void ShowAd()
        {
            if (_adIsShown) return;
            _adIsShown = true;
            var adCount = PlayerPrefs.GetInt("AdCount", 0);
            adCount += 1;
            PlayerPrefs.SetInt("AdCount", adCount);
            _canvasGroup.alpha = 0f;
            _canvasGroup.blocksRaycasts = false;
            if (adCount == 5)
            {
                Debug.Log("Showing Rewarded Ad");
                adCount = 0;
                PlayerPrefs.SetInt("AdCount", adCount);
                //IronSourceController.Instance.ShowRewardedAd();
            }
            else
            {
                Debug.Log("Showing Interstitial Ad");
                //IronSourceController.Instance.ShowInterstitial();
            }
        }
    }
}
