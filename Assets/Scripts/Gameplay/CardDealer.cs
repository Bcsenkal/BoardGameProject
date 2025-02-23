using UnityEngine;
using System.Collections.Generic;

public class CardDealer : MonoBehaviour
{
    private PlayerHand[] players;
    private List<PlayerHand> playingPlayers = new List<PlayerHand>();
    void Awake()
    {
        CachePlayers();
    }

    void Start()
    {
        Managers.EventManager.Instance.OnCreateTable += SetPlayerCount;
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
        playingPlayers.Clear();
        playingPlayers.Add(players[0]);
        var playerCount = info.playerCount;
        for(int i = 1; i < playerCount; i++)
        {
            playingPlayers.Add(players[i]);
        }
        for(int i = 0; i < players.Length; i++)
        {
            players[i].gameObject.SetActive(playingPlayers.Contains(players[i]));
        }
    }
}
