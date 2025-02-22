using System.Collections;
using LitMotion;
using LitMotion.Extensions;
using Managers;
using TMPro;
using UnityEngine;


namespace UI
{
    public class LevelNumber : MonoBehaviour
    {
        private int _levelNumber;
        
        void Start()
        {
            _levelNumber = ES3.Load("CurrentLevelNumber", 1);
            transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _levelNumber < 10 ? $"0{_levelNumber}" : $"{_levelNumber}";
            EventManager.Instance.ONLevelEnd += OnLevelEnd;
            
        }

        

        private void OnLevelEnd(bool isSuccess)
        {
            if (!isSuccess) return;
            ES3.Save("CurrentLevelNumber", _levelNumber+1);
        }

        
    }
}
