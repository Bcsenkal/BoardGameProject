using System.Collections;
using LitMotion;
using LitMotion.Extensions;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace UI
{
    public class MoveCounter : MonoBehaviour
    {
        private TextMeshProUGUI _moveCountLabel;
        private RectTransform _moveCountRect;
        private int _moveCount = 30;
        private ShineMovement _shineMovement;
        void Start()
        {
            CacheComponents();
        }

        private void CacheComponents()
        {
            _moveCountLabel = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _moveCountRect = _moveCountLabel.GetComponent<RectTransform>();
            _moveCount = 30; //ResourceManager.Instance.GetMoveCount();
            _shineMovement = GetComponentInChildren<ShineMovement>();
            EventManager.Instance.ONPlayerMakeAMove += ONPlayerMakeAMove;
            EventManager.Instance.ONLevelStart += ONLevelStart;
            EventManager.Instance.ONAddMoves += ONAddMoves;
            EventManager.Instance.ONLevelComplete += ONLevelComplete;
            
            _moveCountLabel.text = $"{_moveCount}";
            
        }

        private void ONLevelComplete()
        {
            ResourceManager.Instance.SetRewardAmount(_moveCount * 5);
            EventManager.Instance.OnONLevelEnd(true);
        }

        private void ONAddMoves(int amount)
        {
            _moveCount += amount;
            _moveCountLabel.text = $"{_moveCount}";
        }

        private void ONPlayerMakeAMove()
        {
            _moveCount -= 1;
            ResourceManager.Instance.DecreaseMoveCount();  
            _moveCountLabel.text = $"{_moveCount}";
            if (_moveCount != 0) return;
            EventManager.Instance.OnONLevelEnd(false);
        }

        public void SetRewardCoinCount(int amount)
        {
            _moveCountLabel.text = $"{_moveCount}";
            LMotion.Create(Vector3.one, Vector3.one*1.2f, .35f)
                .WithEase(Ease.OutQuad)
                .WithLoops(2,LoopType.Yoyo)
                .BindToLocalScale(_moveCountRect);
        }
        

        private void ONLevelStart()
        {
            _shineMovement.StartMovement();
        }

        
        
    }
}
