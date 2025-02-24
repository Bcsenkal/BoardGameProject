using UnityEngine;
using TMPro;

public class IngamePlayerInfo : MonoBehaviour
{
    public bool IsBot { get; set; }
    private TextMeshProUGUI nameText;
    private TextMeshProUGUI moneyText;
    private TextMeshProUGUI scoreText;
    public void CacheComponents()
    {
        nameText = transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        moneyText = transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        scoreText = transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();

        Managers.EventManager.Instance.OnCreateTable += OnCreateTable;
    }



    private void OnCreateTable(TableCreationInfo tableCreationInfo)
    {
        nameText.text = IsBot ?  "Bot" : ResourceManager.Instance.GetPlayerStats().playerName;
        var betRange = tableCreationInfo.maximumBet / tableCreationInfo.minimumBet;
        var randomMoney = XearUtils.ThousandFormat(Random.Range(2, betRange - 1) * tableCreationInfo.minimumBet);
        moneyText.text = IsBot ? randomMoney : XearUtils.ThousandFormat(ResourceManager.Instance.GetCurrentCoin());
        scoreText.text = "0";
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void UpdateMoney()
    {
        if(IsBot) return;
        moneyText.text = XearUtils.ThousandFormat(ResourceManager.Instance.GetCurrentCoin());
    }
}
