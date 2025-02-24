using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using TMPro;

public class EndPanel : MonoBehaviour
{
    private TextMeshProUGUI resultText;
    private TextMeshProUGUI moneyText;
    private Button newGameButton;
    private Button leaveLobbyButton;
    private Transform panel;
    private int currentBet;
    private int playerCount;

    void Start()
    {
        panel = transform.GetChild(0);
        resultText = panel.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        newGameButton = panel.GetChild(1).GetComponent<Button>();
        leaveLobbyButton = panel.GetChild(2).GetComponent<Button>();
        moneyText = panel.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();

        Managers.EventManager.Instance.OnCreateTable += SetBetInfo;
        Managers.EventManager.Instance.OnShowEndScreen += ShowPanel;
    }

    private void SetBetInfo(TableCreationInfo tableCreationInfo)
    {
        currentBet = tableCreationInfo.currentBet;
        playerCount = tableCreationInfo.playerCount;
    }

    private void ShowPanel(bool isWin)
    {
        resultText.text = isWin ? "YOU WIN!" : "YOU LOSE!";
        moneyText.text = isWin ? "+ " + XearUtils.ThousandFormat(currentBet * playerCount) : "- " + XearUtils.ThousandFormat(currentBet);
        if(isWin)
        {
            ResourceManager.Instance.AddCoin(currentBet * playerCount);
            ResourceManager.Instance.AddWinCount();
        }
        else
        {
            ResourceManager.Instance.AddLoseCount();
        }
        transform.localScale = Vector3.one;
        _ =ShowPanelRoutine();
    }

    private async UniTask ShowPanelRoutine()
    {
        await LMotion.Create(Vector3.zero, Vector3.one, 0.25f).BindToLocalScale(panel);
        leaveLobbyButton.onClick.AddListener(LeaveLobby);
        newGameButton.onClick.AddListener(RestartGame);
    }

    private async UniTask HidePanelRoutine(bool isRestart)
    {
        leaveLobbyButton.onClick.RemoveListener(LeaveLobby);
        newGameButton.onClick.RemoveListener(RestartGame);
        await LMotion.Create(Vector3.one, Vector3.zero, 0.25f).BindToLocalScale(panel);
        transform.localScale = Vector3.zero;
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
        _ = HidePanelRoutine(false);
    }

    private void RestartGame()
    {
        if(!ResourceManager.Instance.HasEnoughCoin(currentBet))
        {
            Managers.EventManager.Instance.ONOnNotEnoughMoney();
            return;
        }
        ResourceManager.Instance.SpendCoin(currentBet);
        ResourceManager.Instance.AddLoseCount();
        _ = HidePanelRoutine(true);
    }
}
