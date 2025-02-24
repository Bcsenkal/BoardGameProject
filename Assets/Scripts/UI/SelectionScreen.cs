using UnityEngine;

public class SelectionScreen : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        Managers.EventManager.Instance.OnCreateTable += HideScreen;
        Managers.EventManager.Instance.OnLeaveGame += ShowScreen;
    }

    private void HideScreen(TableCreationInfo tableCreationInfo)
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    private void ShowScreen(bool isRestart)
    {
        if(isRestart) return;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }
}
