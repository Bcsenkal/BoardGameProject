using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerInfoHeader : MonoBehaviour
{
    private Button button;
    private TextMeshProUGUI playerName;
    private TextMeshProUGUI playerCoin;
    void Start()
    {
        playerName = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        playerCoin = transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        button = GetComponent<Button>();
        button.onClick.AddListener(ShowInfoPanel);
        Managers.EventManager.Instance.OnLeaveGame += SetPlayerStats;
        SetPlayerStats(false);
    }

    private void ShowInfoPanel()
    {
        Managers.EventManager.Instance.ONOnShowPlayerInfoPanel();
    }

    private void SetPlayerStats(bool isRestart)
    {
        var stats = ResourceManager.Instance.GetPlayerStats();
        playerName.text = stats.playerName;
        playerCoin.text = XearUtils.ThousandFormat(stats.currentCoin);
    }
}
