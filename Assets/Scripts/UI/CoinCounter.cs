using System.Collections;
using AssetKits.ParticleImage;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CoinCounter : MonoBehaviour
    {
        [SerializeField] private bool isOnEndScreen;
        private TextMeshProUGUI _coinText;
        private int _currentCoin;
        private int _targetCoin;
        private ParticleImage _coinParticle;
        private RectTransform _coinParticleRect;
        private Button _adCoinButton;
        private bool _isAdClicked;
        private int _levelTotalReward;
        
        void Start()
        {
            _coinText = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
            _coinParticle = transform.GetChild(0).GetChild(1).GetComponent<ParticleImage>();
            _coinParticleRect = _coinParticle.GetComponent<RectTransform>();
            _currentCoin = ResourceManager.Instance.GetCurrentCoin();
            _coinText.text = _currentCoin.ToString();
            _adCoinButton = transform.GetChild(1).GetComponent<Button>();
            _adCoinButton.onClick.AddListener(ShowRewardedForCoin);
            //_moveCounter = transform.parent.GetChild(transform.GetSiblingIndex()+1).GetComponent<MoveCounter>();
            EventManager.Instance.OnSetCurrentCoin += OnSetCurrentCoin;
            EventManager.Instance.ONLevelComplete += OnLevelComplete;
        }

        private void OnLevelComplete()
        {
            //EventManager.Instance.OnONLevelSuccessReward(_levelTotalReward);
        }

        private void ShowRewardedForCoin()
        {
            //EventManager.Instance.OnONOpenAdCoinReward();
        }

    

        private void OnSetCurrentCoin(int amount,bool isIncrement)
        {
            if (!isOnEndScreen) return;
            if (isIncrement)
            {
                _targetCoin = _currentCoin + amount;
                //ResourceManager.Instance.AddCoin(amount);
                //_coinParticleRect.position = position;
                _levelTotalReward += amount;
                
            }
            else
            {
                _targetCoin = _currentCoin - amount;
            }
            StartCoroutine(SetGoldText(isIncrement));
        }

        IEnumerator SetGoldText(bool isIncrease)
        {
            if(isIncrease)
            {
                _coinParticle.Play();
                yield return new WaitForSeconds(1.5f);
            }
            var timer = 0f;
            while (timer < 0.25f)
            {
                timer += Time.deltaTime;
                _coinText.text = Mathf.RoundToInt(Mathf.Lerp(_currentCoin, _targetCoin, timer)).ToString();
                yield return null;
            }

            
            _currentCoin = _targetCoin;
            _coinText.text = _currentCoin.ToString();
        }

    }
}
