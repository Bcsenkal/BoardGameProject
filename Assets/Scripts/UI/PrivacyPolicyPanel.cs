using System;
using LitMotion;
using LitMotion.Extensions;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PrivacyPolicyPanel : MonoBehaviour
    {

        private Button _acceptButton;

        private void Awake()
        {
            //EventManager.Instance.ONRemoteConfigFetchComplete += ONRemoteConfigFetchComplete;
        }

        void Start()
        {
            _acceptButton = transform.GetChild(5).GetComponent<Button>();
            _acceptButton.onClick.AddListener(ClosePanel);
            Invoke(nameof(InitPolicyCheck),.5f);
            
        }

        private void ONRemoteConfigFetchComplete()
        {
            //EventManager.Instance.ONRemoteConfigFetchComplete -= ONRemoteConfigFetchComplete;
            Invoke(nameof(InitPolicyCheck),.05f);
        }

        private void InitPolicyCheck()
        {
            Invoke(nameof(QuickStartDelay),0.1f);
            /*if (PlayerPrefs.HasKey("PrivacyAccepted"))
            {
                //
                Invoke(nameof(QuickStartDelay),0.1f);
                return;
            }
            Invoke(nameof(ShowPanel),0.25f);*/
        }

        


        private void QuickStartDelay()
        {
            Debug.Log("Privacy Event Called");
            EventManager.Instance.OnONPrivacyPolicyAccepted();
            gameObject.SetActive(false);
        }

        

        private void ShowPanel()
        {
            //Debug.Log("Show panel");
            LMotion.Create(Vector3.zero, Vector3.one, .35f)
                .WithEase(Ease.OutBack)
                .BindToLocalScale(transform);
        }

        private void ClosePanel()
        {
            PlayerPrefs.SetInt("PrivacyAccepted",1);
            AudioManager.Instance.PlayButtonClick();
            //Vibration.VibratePop();
            _acceptButton.onClick.RemoveListener(ClosePanel);
            EventManager.Instance.OnONPrivacyPolicyAccepted();
            LMotion.Create(Vector3.one, Vector3.zero, .35f)
                .WithEase(Ease.InBack)
                .BindToLocalScale(transform);
        }
    }
}