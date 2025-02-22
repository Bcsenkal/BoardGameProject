using Managers;
using UnityEngine;

namespace UI
{
    public class DialoguePanel : MonoBehaviour
    {
        private GameObject _notEnoughGold;
        private GameObject _pathIsClosed;
        private GameObject _stackIsEmpty;
        private Vector3 _startPosition;
        void Start()
        {
            _notEnoughGold = transform.GetChild(0).gameObject;
            _pathIsClosed = transform.GetChild(1).gameObject;
            _startPosition = _pathIsClosed.transform.localPosition;
            _notEnoughGold.SetActive(false);
            _pathIsClosed.SetActive(false);
            //EventManager.Instance.ONNotEnoughCoin += ShowNotEnoughCoin;
            
        }
        

        private void ShowNotEnoughCoin()
        {
            _notEnoughGold.transform.localPosition = _startPosition;
            _notEnoughGold.SetActive(true);
        }
        
    }
}
