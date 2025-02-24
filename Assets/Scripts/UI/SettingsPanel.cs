using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using TMPro;

public class SettingsPanel : MonoBehaviour
{
    private Button closeButton;
    private Button newGameButton;
    private Button leaveLobbyButton;
    private Transform panel;
    private int currentBet;

    void Start()
    {
        panel = transform.GetChild(0);
        newGameButton = panel.GetChild(0).GetComponent<Button>();
        leaveLobbyButton = panel.GetChild(1).GetComponent<Button>();
        closeButton = panel.GetChild(2).GetComponent<Button>();
        Managers.EventManager.Instance.OnShowSettingsPanel += ShowPanel;
        Managers.EventManager.Instance.OnCreateTable += SetBetInfo;
    }

    private void SetBetInfo(TableCreationInfo tableCreationInfo)
    {
        currentBet = tableCreationInfo.currentBet;
    }

    private void ShowPanel()
    {
        transform.localScale = Vector3.one;
        _ =ShowPanelRoutine();
    }

    private async UniTask ShowPanelRoutine()
    {
        await LMotion.Create(Vector3.zero, Vector3.one, 0.25f).BindToLocalScale(panel);
        closeButton.onClick.AddListener(HidePanel);
        leaveLobbyButton.onClick.AddListener(LeaveLobby);
        newGameButton.onClick.AddListener(RestartGame);
    }

    private void HidePanel()
    {
        _ = HidePanelRoutine(true, false);
    }

    private async UniTask HidePanelRoutine(bool isCloseButton, bool isRestart)
    {
        closeButton.onClick.RemoveListener(HidePanel);
        leaveLobbyButton.onClick.RemoveListener(LeaveLobby);
        newGameButton.onClick.RemoveListener(RestartGame);
        await LMotion.Create(Vector3.one, Vector3.zero, 0.25f).BindToLocalScale(panel);
        transform.localScale = Vector3.zero;
        if(isCloseButton)
        {
            Managers.EventManager.Instance.ONOnHideSettingsPanel();
            return;
        } 
        if(isRestart)
        {
            Managers.EventManager.Instance.ONOnLeaveGame(true);
            Managers.EventManager.Instance.ONOnHideSettingsPanel();
            return;
        }
        Managers.EventManager.Instance.ONOnLeaveGame(false);
    }

    private void LeaveLobby()
    {
        _ = HidePanelRoutine(false, false);
    }

    private void RestartGame()
    {
        if(!ResourceManager.Instance.HasEnoughCoin(currentBet))
        {
            Managers.EventManager.Instance.ONOnNotEnoughMoney();
            return;
        }
        ResourceManager.Instance.AddLoseCount();
        ResourceManager.Instance.SpendCoin(currentBet);
        _ = HidePanelRoutine(false, true);
    }
}
