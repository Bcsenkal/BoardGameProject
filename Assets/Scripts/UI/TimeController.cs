using System;
using Managers;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TimeController : MonoBehaviour
    {
        private readonly int _levelTime = 20;
        private TextMeshProUGUI _timerLabel;
        private bool _canCountDown;
        private float _nextCheck;
        private bool _onCoolDown;
        
        private DateTime _unlockTime;
        private TimeSpan _remainingTime;
        private readonly TimeSpan _zeroTime = TimeSpan.FromSeconds(0.0);
        
        private void Awake()
        {
            _timerLabel = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            _timerLabel.text = _levelTime.ToString();
            EventManager.Instance.ONLevelEnd += ONLevelEnd;
            SetTimer();
        }

        private void ONLevelEnd(bool obj)
        {
            _canCountDown = false;
        }


        private void Update()
        {
            if (!_canCountDown) return;
            if (Time.time < _nextCheck) return;
            _nextCheck = Time.time + .5f;
            TimerCountDown();
        }

        private void SetTimer()
        {
            _unlockTime = DateTime.Now.AddSeconds(_levelTime);
            _remainingTime = DateTime.Now.Subtract(_unlockTime);
            _timerLabel.text = _remainingTime.ToString(@"ss");
            _canCountDown = true;
        }

        private void TimerCountDown()
        {
            
            _remainingTime = DateTime.Now.Subtract(_unlockTime);
            _timerLabel.text = _remainingTime.ToString(@"ss");
            
            var index = _remainingTime.CompareTo(_zeroTime);
            if (index < 0) return;
            _canCountDown = false;
            EventManager.Instance.ONLevelEnd -= ONLevelEnd;
            EventManager.Instance.OnONLevelEnd(false);
        }
    }
}
