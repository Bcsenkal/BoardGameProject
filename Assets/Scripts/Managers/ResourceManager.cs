using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    private int currentCoin;
    private int winCount;
    private int loseCount;

    protected override void Awake() 
    {
        base.Awake();
        LoadResources();
    }

    private void LoadResources()
    {
        var startingCoin = 1000;
        currentCoin = ES3.Load("TotalCoin",startingCoin);
        winCount = ES3.Load("WinCount",0);
        loseCount = ES3.Load("LoseCount",0);
    }

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
