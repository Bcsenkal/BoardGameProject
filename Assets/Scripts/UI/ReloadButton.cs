using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ReloadButton : MonoBehaviour
    {
        private Button _thisButton;
        void Start()
        {
            _thisButton = GetComponent<Button>();
            _thisButton.onClick.AddListener(OpenIt);
            
        }
        private void OpenIt()
        {
            EventManager.Instance.OnONReloadButtonPressed();
        }
    }
}
