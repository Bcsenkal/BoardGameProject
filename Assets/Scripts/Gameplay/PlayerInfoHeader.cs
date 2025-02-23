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
        SetPlayerStats();
    }

    private void ShowInfoPanel()
    {
        Managers.EventManager.Instance.ONOnShowPlayerInfoPanel();
    }

    private void SetPlayerStats()
    {
        var stats = ResourceManager.Instance.GetPlayerStats();
        playerName.text = stats.playerName;
        playerCoin.text = XearUtils.ThousandFormat(stats.currentCoin);
    }
}
