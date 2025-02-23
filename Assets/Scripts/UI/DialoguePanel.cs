using Managers;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using LitMotion;
using LitMotion.Extensions;
using System;

namespace UI
{
    public class Dialogue : MonoBehaviour
    {
        private Transform notEnoughMoney;
        private Vector3 _startPosition;
        private CancellationTokenSource cancellationToken = new CancellationTokenSource();
        private bool isShowing;
        void Start()
        {
            notEnoughMoney = transform.GetChild(0);
            EventManager.Instance.OnNotEnoughMoney += NotEnoughMoney;
            
        }
        

        private void NotEnoughMoney()
        {
            if (isShowing)
            {
                isShowing = false;
                cancellationToken.Cancel();
                cancellationToken.Dispose();
                cancellationToken = new CancellationTokenSource();
            }
            ShowNotEnoughMoney();
        }

        private async UniTask ShowNotEnoughMoney()
        {
            notEnoughMoney.gameObject.SetActive(true);
            isShowing = true;
            cancellationToken.Token.ThrowIfCancellationRequested();
            await LMotion.Create(Vector3.zero, Vector3.one*1.1f, .15f)
                .WithEase(Ease.OutQuad)
                .BindToLocalScale(notEnoughMoney).ToUniTask(cancellationToken.Token);
            await LMotion.Create(Vector3.one*1.1f, Vector3.one*.9f, .15f)
                .WithEase(Ease.OutQuad)
                .BindToLocalScale(notEnoughMoney).ToUniTask(cancellationToken.Token);
            await LMotion.Create(Vector3.one*.9f, Vector3.one, .1f)
                .WithEase(Ease.OutBack)
                .BindToLocalScale(notEnoughMoney).ToUniTask(cancellationToken.Token);
            await UniTask.Delay(TimeSpan.FromSeconds(1f), ignoreTimeScale: false,cancellationToken: cancellationToken.Token);
            await LMotion.Create(Vector3.one, Vector3.zero, .25f)
                .WithEase(Ease.InBack)
                .BindToLocalScale(notEnoughMoney).ToUniTask(cancellationToken.Token);
            isShowing = false;
            notEnoughMoney.gameObject.SetActive(false);
        }
        
    }
}
