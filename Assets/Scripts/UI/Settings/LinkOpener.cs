using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class LinkOpener : MonoBehaviour
    {
        private bool _linkIsOpening;
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OpenLink);
        }

        private void OpenLink()
        {
            if (_linkIsOpening) return;
            _linkIsOpening = true;
            Invoke(nameof(ResetLinkOpening),1f);
            Application.OpenURL("https://rocinantegames.com/index.php/privacy-policy");
        }


        private void ResetLinkOpening()
        {
            _linkIsOpening = false;
        }
    }
}
