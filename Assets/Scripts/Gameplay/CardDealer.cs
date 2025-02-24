using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
public class CardDealer : MonoBehaviour
{
    private PlayerHand[] players;
    private List<PlayerHand> playingPlayers = new List<PlayerHand>();
    private int currentPlayer;
    private TableCreationInfo tableInfo;
    private bool isOutOfCards;
    void Awake()
    {
        CachePlayers();
    }

    void Start()
    {
        Managers.EventManager.Instance.OnCreateTable += SetPlayerCount;
        Managers.EventManager.Instance.OnSkipTurn += SkipTurn;
        Managers.EventManager.Instance.OnOutOfCards += OutOfCards;
        Managers.EventManager.Instance.OnLeaveGame += LeaveGame;
        Managers.EventManager.Instance.OnGameOver += GameOver;
    }

    private void CachePlayers()
    {
        players = new PlayerHand[4];
        for (int i = 0; i < players.Length; i++)
        {
            players[i] = transform.GetChild(i).GetComponent<PlayerHand>();
            players[i].CacheComponents();
        }
    }

    private void SetPlayerCount(TableCreationInfo info)
    {
        tableInfo = info;
        playingPlayers.Clear();
        playingPlayers.Add(players[0]);
        var playerCount = info.playerCount;
        ActivatePlayers(playerCount);
        for(int i = 0; i < players.Length; i++)
        {
            players[i].gameObject.SetActive(playingPlayers.Contains(players[i]));
        }
        StartCoroutine(DealCards());
    }

    private void ActivatePlayers(int playerCount)
    {
        playingPlayers.Add(players[2]);
        if(playerCount < 3) return;
        playingPlayers.Insert(1,players[1]);
        playingPlayers.Add(players[3]);
    }

    private IEnumerator DealCards()
    {
        for(int i = 0; i < playingPlayers.Count; i++)
        {
            Managers.EventManager.Instance.ONOnDealCardsToPlayer(playingPlayers[i]);
            yield return new WaitForSeconds(0.5f);
        }
        currentPlayer = 0;
        Managers.EventManager.Instance.ONOnCardDealingComplete();
    }

    public void SkipTurn(List<Card> cardsOnPile)
    {
        currentPlayer = (currentPlayer + 1) % playingPlayers.Count;
        if(playingPlayers[currentPlayer].GetHandCount() == 0)
        {
            if(isOutOfCards)
            {
                Managers.EventManager.Instance.ONOnSendRemainingCardsToPlayer();
                return;
            }
            StartCoroutine(DealCardsAgain());
            return;
        }
        StartCoroutine(playingPlayers[currentPlayer].PlayTurn(cardsOnPile));
    }

    private IEnumerator DealCardsAgain()
    {
        yield return DealCards();
        StartCoroutine(playingPlayers[currentPlayer].PlayTurn(null));
    }

    private void OutOfCards()
    {
        isOutOfCards = true;
    }

    private void LeaveGame(bool isRestart)
    {
        StopAllCoroutines();
        isOutOfCards = false;
        currentPlayer = 0;
        if(!isRestart) return;
        Invoke(nameof(Restart),0.2f);
        
    }

    private void Restart()
    {
        SetPlayerCount(tableInfo);
    }

    private void GameOver()
    {
        int highestPoint = 0;
        for(int i = 0; i < players.Length; i++)
        {
            if(players[i].GetPoint() > highestPoint)
            {
                highestPoint = players[i].GetPoint();
            }
        }
        var isWin = highestPoint == players[0].GetPoint();
        Managers.EventManager.Instance.ONOnShowEndScreen(isWin);
    }
}
