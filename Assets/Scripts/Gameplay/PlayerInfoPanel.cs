using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using TMPro;


public class PlayerInfoPanel : MonoBehaviour
{
    private Button closeButton;
    private Transform panel;
    private TextMeshProUGUI playerName;
    private TextMeshProUGUI playerCoin;
    private TextMeshProUGUI playerWinCount;
    private TextMeshProUGUI playerLoseCount;
    void Start()
    {
        panel = transform.GetChild(0);
        closeButton = panel.GetChild(3).GetComponent<Button>();
        playerName = panel.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
        playerCoin = panel.GetChild(1).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        playerWinCount = panel.GetChild(2).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        playerLoseCount = panel.GetChild(2).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        Managers.EventManager.Instance.OnShowPlayerInfoPanel += ShowPanel;

    }

    private void ShowPanel()
    {
        transform.localScale = Vector3.one;
        _ =ShowPanelRoutine();
    }

    private async UniTask ShowPanelRoutine()
    {
        SetPlayerStats();
        await LMotion.Create(Vector3.zero, Vector3.one, 0.25f).BindToLocalScale(panel);
        closeButton.onClick.AddListener(HidePanel);
    }

    private void HidePanel()
    {
        closeButton.onClick.RemoveListener(HidePanel);
        _ = HidePanelRoutine();
    }

    private async UniTask HidePanelRoutine()
    {
        await LMotion.Create(Vector3.one, Vector3.zero, 0.25f).BindToLocalScale(panel);
        transform.localScale = Vector3.zero;
    }

    private void SetPlayerStats()
    {
        var stats = ResourceManager.Instance.GetPlayerStats();
        playerName.text = stats.playerName;
        playerCoin.text = stats.currentCoin.ToString();
        playerWinCount.text = stats.winCount.ToString();
        playerLoseCount.text = stats.loseCount.ToString();
    }
}
