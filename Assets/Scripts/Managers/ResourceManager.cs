using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public struct PlayerStats
{
    public string playerName;
    public int winCount;
    public int loseCount;
    public int currentCoin;
}
public class ResourceManager : Singleton<ResourceManager>
{
    private int currentCoin;
    private int winCount;
    private int loseCount;
    private string playerName;

    protected override void Awake() 
    {
        base.Awake();
        LoadResources();
    }

    private void LoadResources()
    {
        var savedCoin = ES3.Load("TotalCoin",1000);
        currentCoin = savedCoin <= 0 ? 1000 : savedCoin;
        winCount = ES3.Load("WinCount",0);
        loseCount = ES3.Load("LoseCount",0);
        playerName = ES3.LoadString("PlayerName","Player");
    }

    #region Stats

    public void AddWinCount()
    {
        winCount += 1;
        ES3.Save("WinCount",winCount);
    }

    public void AddLoseCount()
    {
        loseCount += 1;
        ES3.Save("LoseCount",loseCount);
    }
    
    public void SetPlayerName(string name)
    {
        playerName = name;
        ES3.Save("PlayerName",name);
    }

    public PlayerStats GetPlayerStats()
    {
        var stats = new PlayerStats();
        stats.playerName = playerName;
        stats.winCount = winCount;
        stats.loseCount = loseCount;
        stats.currentCoin = currentCoin;
        return stats;
    }

    

    #endregion

    #region Coin
    
    
    public void SpendCoin(int amount)
    {
        currentCoin -= amount;
        if (currentCoin < 0)
        {
            currentCoin = 0;
        }
        ES3.Save("TotalCoin",currentCoin);
        EventManager.Instance.ONOnSetCurrentCoin(amount,false);
    }

    public void AddCoin(int amount)
    {
        currentCoin += amount;
        ES3.Save("TotalCoin",currentCoin);
        EventManager.Instance.ONOnSetCurrentCoin(amount,true);
    }

    public int GetCurrentCoin()
    {
        return currentCoin;
    }

    public bool HasEnoughCoin(int price)
    {
        return price <= currentCoin;
    }

    #endregion
}
