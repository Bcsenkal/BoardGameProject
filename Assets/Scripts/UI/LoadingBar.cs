using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoadingBar : MonoBehaviour
    {

        private Image _loadingBar;

        private void Awake()
        {
            _loadingBar = GetComponent<Image>();
            _loadingBar.fillAmount = 0f;
            FillLoadingBar();
        }

        private async UniTask FillLoadingBar()
        {
            var counter = 0f;
            while (counter<= 2.9f)
            {
                _loadingBar.fillAmount = Mathf.Lerp(0f,1f,counter/2.9f);
                counter += Time.deltaTime;
                await UniTask.Yield();
            }
        }
    }
}
