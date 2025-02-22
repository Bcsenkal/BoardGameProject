using System.Collections;
using System.Collections.Generic;
//using GameAnalyticsSDK;
using UnityEngine;
using UnityEngine.UI;
using Managers;

public class SettingsOption : MonoBehaviour
{

    [SerializeField] private Color onColor;
    [SerializeField] private Color offColor;

    [SerializeField] private bool sound;
    [SerializeField] private bool music;
    [SerializeField] private bool vibration;

    private Button _thisButton;
    private bool _isOff;
    private Image _buttonImage;
    private RectTransform _handle;
    private Vector3 _handleCurrentPosition;
    private bool _canPress;
    void Start()
    {
        _thisButton = GetComponent<Button>();
        _thisButton.onClick.AddListener(OnClick);

        _buttonImage = _thisButton.GetComponent<Image>();
        _handle = transform.GetChild(2).GetComponent<RectTransform>();
        _handleCurrentPosition = _handle.localPosition;
        if (sound)
        {
            _isOff = PlayerPrefs.GetInt("SoundSetting", 0) == 1;
            if (_isOff)
            {
                AudioManager.Instance.MuteSound(true);
                //PlayerPrefs.SetInt("SoundSetting", 1);
                _buttonImage.color = offColor;
                _handleCurrentPosition.x = -60f;
            }
            else
            {
                _buttonImage.color = onColor;
                _handleCurrentPosition.x = 60f;
            }
        }else if (vibration)
        {
            _isOff = PlayerPrefs.GetInt("VibrationSetting", 0) == 1;
            if (_isOff)
            {
                _buttonImage.color = offColor;
                _handleCurrentPosition.x = -60f;
            }
            else
            {
                _buttonImage.color = onColor;
                _handleCurrentPosition.x = 60f;
            }
        }else if (music)
        {
            _isOff = PlayerPrefs.GetInt("MusicSetting", 0) == 1;
            if (_isOff)
            {
                _buttonImage.color = offColor;
                AudioManager.Instance.MuteMusic(true);
                _handleCurrentPosition.x = -60f;
            }
            else
            {
                _buttonImage.color = onColor;
                _handleCurrentPosition.x = 60f;
            }
        }

        
        _handle.localPosition = _handleCurrentPosition;
        _canPress = true;
    }

    
    


    private void OnClick()
    {
        if (!_canPress) return;
        _canPress = false;
        AudioManager.Instance.PlayButtonClick();

        if (sound)
        {
            _isOff = !_isOff;
            if (_isOff)
            {
                
                //GameAnalytics.NewDesignEvent("Mute_Sound");
                _buttonImage.color = offColor;
                AudioManager.Instance.MuteSound(true);
                PlayerPrefs.SetInt("SoundSetting", 1);
                StartCoroutine(HandleMovementRoutine(true));
            }
            else
            {
                
                _buttonImage.color = onColor;
                AudioManager.Instance.MuteSound(false);
                PlayerPrefs.SetInt("SoundSetting", 0);
                StartCoroutine(HandleMovementRoutine(false));
            }
        }else if (music)
        {
            _isOff = !_isOff;
            if (_isOff)
            {
                
                //GameAnalytics.NewDesignEvent("Mute_Music");
                _buttonImage.color = offColor;
                AudioManager.Instance.MuteMusic(true);
                PlayerPrefs.SetInt("MusicSetting", 1);
                StartCoroutine(HandleMovementRoutine(true));
            }
            else
            {
                
                _buttonImage.color = onColor;
                AudioManager.Instance.MuteMusic(false);
                PlayerPrefs.SetInt("MusicSetting", 0);
                StartCoroutine(HandleMovementRoutine(false));
            }
        }
        else if (vibration)
        {
            _isOff = !_isOff;
            if (_isOff)
            {
                
                //GameAnalytics.NewDesignEvent("TurnOff_Vibration");
                _buttonImage.color = offColor;
                PlayerPrefs.SetInt("VibrationSetting", 1);
                StartCoroutine(HandleMovementRoutine(true));
            }
            else
            {
                
                _buttonImage.color = onColor;
                PlayerPrefs.SetInt("VibrationSetting", 0);
                StartCoroutine(HandleMovementRoutine(false));
            }
            
        }
    }


    IEnumerator HandleMovementRoutine(bool isOff)
    {
        _handleCurrentPosition = _handle.localPosition;
        var counter = 0f;
        while (counter <= .35f)
        {
            var t = Easings.QuadEaseOut(counter, 0f, 1f, .35f);
            if (isOff)
            {
                _handleCurrentPosition.x = Mathf.Lerp(60f, -60f, t);
                _handle.localPosition = _handleCurrentPosition;
            }
            else
            {
                _handleCurrentPosition.x = Mathf.Lerp(-60f, 60f, t);
                _handle.localPosition = _handleCurrentPosition;
            }
            counter += Time.deltaTime;
            yield return null;
        }

        if (isOff)
        {
            _handleCurrentPosition.x = -60f;
            
        }
        else
        {
            _handleCurrentPosition.x = 60f;
            
        }

        _handle.localPosition = _handleCurrentPosition;
        _canPress = true;
    }

}
