using UnityEngine;
using System;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;

[Serializable]
public class TableCreationInfo
{
    public GameMode tableType;
    public int currentBet;
    public int playerCount;

    public TableCreationInfo(GameMode tableType, int currentBet, int playerCount)
    {
        this.tableType = tableType;
        this.currentBet = currentBet;
        this.playerCount = playerCount;
    }
}
public class CreateTablePanel : MonoBehaviour
{
    private TableInfo tableInfo;
    private Button createTableButton;
    private Button closeButton;
    private BetSlider betSlider;
    private PlayerSelection playerSelection;
    private Transform panel;
    void Start()
    {
        panel = transform.GetChild(0);
        createTableButton = panel.GetChild(1).GetComponent<Button>();
        betSlider = panel.GetChild(3).GetComponent<BetSlider>();
        playerSelection = panel.GetChild(4).GetComponent<PlayerSelection>();
        closeButton = panel.GetChild(5).GetComponent<Button>();
        Managers.EventManager.Instance.OnShowCreateTablePanel += ShowPanel;
        transform.localScale = Vector3.zero;
    }

    private void ShowPanel(TableInfo info)
    {
        tableInfo = info;
        betSlider.SetBetRange(info);
        transform.localScale = Vector3.one;
        ShowPanelRoutine();
    }

    private async UniTask ShowPanelRoutine()
    {
        await LMotion.Create(Vector3.zero, Vector3.one, 0.25f).BindToLocalScale(panel);
        createTableButton.onClick.AddListener(CreateTable);
        closeButton.onClick.AddListener(HidePanel);
    }

    private void HidePanel()
    {
        createTableButton.onClick.RemoveListener(CreateTable);
        closeButton.onClick.RemoveListener(HidePanel);
        HidePanelRoutine();
    }

    private async UniTask HidePanelRoutine()
    {
        await LMotion.Create(Vector3.one, Vector3.zero, 0.25f).BindToLocalScale(panel);
        transform.localScale = Vector3.zero;
    }

    private void CreateTable()
    {
        if(ResourceManager.Instance.GetCurrentCoin() < betSlider.GetCurrentBet()) return;
        TableCreationInfo tableCreationInfo = new(tableInfo.tableType, betSlider.GetCurrentBet(), playerSelection.GetPlayerCount());
        createTableButton.onClick.RemoveListener(CreateTable);
        closeButton.onClick.RemoveListener(HidePanel);
        Debug.Log("Table Created with " + tableCreationInfo.tableType + " " + tableCreationInfo.currentBet + " " + tableCreationInfo.playerCount);
        transform.localScale = Vector3.zero;
    }
}
