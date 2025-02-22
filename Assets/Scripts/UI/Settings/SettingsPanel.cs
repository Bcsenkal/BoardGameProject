using System.Collections;
using LitMotion;
using LitMotion.Extensions;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Settings
{
    public class SettingsPanel : MonoBehaviour
    {

        private Button _closeButton;
        private CanvasGroup _canvasGroup;
        private RectTransform _panelRect;
        private RectTransform _closeButtonRect;
        private Image _thisImage;
        
        private readonly Vector3 _activeScale = new Vector3(1f,1f,1f);
        private readonly Vector3 _deActiveScale = new Vector3(0f, 0f, 0f);

        private void Start()
        {
            CacheComponents();
            EventManager.Instance.ONSettingsButtonPressed += ONSettingsButtonPressed;
        }

        private void ONSettingsButtonPressed()
        {
            OpenPanel();
        }


        private void OpenPanel()
        {
            EventManager.Instance.OnONSettingsPanelOpened(true);
            AlphaRoutine(true);
        }

        private void CacheComponents()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _panelRect = transform.GetChild(1).GetComponent<RectTransform>();
        
            _closeButton = _panelRect.transform.GetChild(5).GetComponent<Button>();
            _closeButtonRect = _closeButton.GetComponent<RectTransform>();
            _closeButton.onClick.AddListener(CloseIt);
        
            
        }


        private void CloseIt()
        {
            EventManager.Instance.OnONSettingsPanelOpened(false);
            PanelScaleRoutine(false);
            _closeButtonRect.localScale = new Vector3(.8f, .8f, .8f);
            StartCoroutine(ButtonScaleRoutine(_closeButtonRect));
        
        }



        private void AlphaRoutine(bool isOpening)
        {

            if (isOpening)
            {
                LMotion.Create(0f, 1f, .35f)
                    .WithEase(Ease.OutQuad)
                    .Bind(x => _canvasGroup.alpha = x);
                
                PanelScaleRoutine(true);
            }
            else
            {
                LMotion.Create(1f, 0f, .35f)
                    .WithEase(Ease.OutQuad)
                    .Bind(x => _canvasGroup.alpha = x);
            }
            
            _canvasGroup.blocksRaycasts = isOpening;
            _canvasGroup.interactable = isOpening;

        }



        private void PanelScaleRoutine(bool scaleUp)
        {
            
            if (scaleUp)
            {
                LMotion.Create(_deActiveScale, _activeScale, .35f)
                    .WithEase(Ease.OutBack)
                    .WithDelay(.15f)
                    .BindToLocalScale(_panelRect);
                Invoke(nameof(ShowCloseButton),1.5f);
                

            }
            else
            {
            
                LMotion.Create(_activeScale, _deActiveScale, .35f)
                    .WithEase(Ease.OutBack)
                    .BindToLocalScale(_panelRect);
                AlphaRoutine(false);
                _closeButton.gameObject.SetActive(false);
            }
        }

        private void ShowCloseButton()
        {
            _closeButton.gameObject.SetActive(true);
        }


        IEnumerator ButtonScaleRoutine(RectTransform rect)
        {
            var counter = 0f;
            while (counter <= .35f)
            {
                var t = Easings.QuadEaseOut(counter, 0f, 1f, .35f);
                var s = Mathf.Lerp(.8f, 1f, t);
                rect.localScale = new Vector3(s, s, s);
                counter += Time.deltaTime;
                yield return null;
            }
            rect.localScale = new Vector3(1f, 1f, 1f);

        }
    }
}
