using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;

public class PlayerInfoPanel : MonoBehaviour
{
    private Button closeButton;
    private Transform panel;
    void Start()
    {
        panel = transform.GetChild(0);
        closeButton = panel.GetChild(3).GetComponent<Button>();
        Managers.EventManager.Instance.OnShowPlayerInfoPanel += ShowPanel;

    }

    private void ShowPanel()
    {
        transform.localScale = Vector3.one;
        ShowPanelRoutine();
    }

    private async UniTask ShowPanelRoutine()
    {
        await LMotion.Create(Vector3.zero, Vector3.one, 0.25f).BindToLocalScale(panel);
        closeButton.onClick.AddListener(HidePanel);
    }

    private void HidePanel()
    {
        closeButton.onClick.RemoveListener(HidePanel);
        HidePanelRoutine();
    }

    private async UniTask HidePanelRoutine()
    {
        await LMotion.Create(Vector3.one, Vector3.zero, 0.25f).BindToLocalScale(panel);
        transform.localScale = Vector3.zero;
    }
}
