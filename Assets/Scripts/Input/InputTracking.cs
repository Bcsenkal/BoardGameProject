using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InputTracking : MonoBehaviour, IPointerClickHandler
{
    private Image raycastImage;
    private bool canGetInput;
    void Start()
    {
        raycastImage = GetComponent<Image>();
        Managers.EventManager.Instance.OnReadyToPlay += ReadyToPlay;
        Managers.EventManager.Instance.OnLeaveGame += OnLeaveGame;
    }
    public void OnPointerClick(PointerEventData eventData)
    {   
        if(!canGetInput) return;
        Managers.EventManager.Instance.ONOnClick(eventData.position);
    }

    private void ReadyToPlay()
    {
        raycastImage.raycastTarget = true;
        canGetInput = true;
    }

    private void OnLeaveGame(bool isRestart)
    {
        DisableInput();
    }

    private void DisableInput()
    {
        raycastImage.raycastTarget = false;
        canGetInput = false;
    }
}
