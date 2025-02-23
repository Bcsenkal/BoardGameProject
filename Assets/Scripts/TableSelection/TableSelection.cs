using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using Lean.Gui;

public enum GameMode
{
    Newbie,
    Rookie,
    Noble
}

[Serializable]
public class TableInfo
{
    public GameMode tableType;
    public int minimumBet;
    public int maximumBet;
    
    public TableInfo(GameMode type, int minimumBet, int maximumBet)
    {
        this.tableType = type;
        this.minimumBet = minimumBet;
        this.maximumBet = maximumBet;
    }
}

public class TableSelection : MonoBehaviour
{

    [SerializeField]private TableInfo tableInfo;
    private TextMeshProUGUI tableName;
    private TextMeshProUGUI betRange;
    private Button createTableButton;
    private Button playNowButton;
    void Start()
    {
        tableName = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        tableName.text = tableInfo.tableType.ToString();
        betRange = transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
        betRange.text = XearUtils.ThousandFormat(tableInfo.minimumBet) + " - " + XearUtils.ThousandFormat(tableInfo.maximumBet);
        createTableButton = transform.GetChild(3).GetComponent<Button>();
        createTableButton.onClick.AddListener(ShowCreateTablePanel);
        playNowButton = transform.GetChild(2).GetComponent<Button>();
        playNowButton.onClick.AddListener(CreateTable);
    }

    void ShowCreateTablePanel()
    {
        if(ResourceManager.Instance.GetCurrentCoin() < tableInfo.minimumBet)
        {
            Managers.EventManager.Instance.ONOnNotEnoughMoney();
            return;
        }
        Managers.EventManager.Instance.ONOnShowCreateTable(tableInfo);
    }

    private void CreateTable()
    {
        Debug.Log(ResourceManager.Instance.GetCurrentCoin());
        if(ResourceManager.Instance.GetCurrentCoin() < tableInfo.minimumBet)
        {
            Managers.EventManager.Instance.ONOnNotEnoughMoney();
            return;
        }
        var tableCreationInfo = new TableCreationInfo(tableInfo.tableType, tableInfo.minimumBet, 2);
        Debug.Log("Table Created with " + tableCreationInfo.tableType + " " + tableCreationInfo.currentBet + " " + tableCreationInfo.playerCount);
    }
}
