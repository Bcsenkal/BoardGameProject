using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class CardDeck : MonoBehaviour
{
    [SerializeField]private Card cardPrefab;
    [SerializeField]private Sprite[] cardSprites;
    [SerializeField]private Sprite cardBackSprite;
    private List<Card> availableCards = new List<Card>();
    private List<Card> allCards = new List<Card>();
    void Start()
    {
        CreateDeck();
        Managers.EventManager.Instance.OnDealCardsToPlayer += DealCardsToPlayer;
        Managers.EventManager.Instance.OnRequestInitialPile += DealInitialPile;
        Managers.EventManager.Instance.OnLeaveGame += LeaveGame;
    }

    private void CreateDeck()
    {
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 13; j++)
            {
                var card = Instantiate(cardPrefab, transform.position, Quaternion.identity,transform).GetComponent<Card>();
                card.CacheComponents();
                card.SetCardInfo((CardType)i, j + 1, cardSprites[i * 13 + j], cardBackSprite,GetCardPoint((CardType)i,j+1));
                card.gameObject.name = ((CardType)i).ToString() + (j+1).ToString();
                availableCards.Add(card);
                allCards.Add(card);
            }
        }
        XearUtils.Shuffle(availableCards);
    }

    private void DealCardsToPlayer(PlayerHand playerHand)
    {
        var dealingList = new List<Card>();
        for(int i = 0; i < 4; i++)
        {
            var randomCard = availableCards[Random.Range(0, availableCards.Count - 1)];
            dealingList.Add(randomCard);
            dealingList[i].SetPlayer(playerHand);
            availableCards.Remove(randomCard);
        }
        StartCoroutine(playerHand.OnDealCards(dealingList));
        if(availableCards.Count == 0)
        {
            Managers.EventManager.Instance.ONOnOutOfCards();
        }
    }

    private void DealInitialPile(CardPile cardPile)
    {
        var dealingList = new List<Card>(4);
        for(int i = 0; i < 4; i++)
        {
            var randomCard = availableCards[Random.Range(0, availableCards.Count - 1)];
            dealingList.Add(randomCard);
            availableCards.Remove(randomCard);
        }
        StartCoroutine(cardPile.DrawCardsFromDeck(dealingList));
    }

    private int GetCardPoint(CardType type, int cardNumber)
    {
        if(type == CardType.Diamonds && cardNumber == 10)
        {
            return 3;
        }
        else if(type == CardType.Clubs && cardNumber == 2)
        {
            return 2;
        }
        else if(cardNumber == 1 || cardNumber == 11)
        {
            return 1;
        }
        return 0;
    }

    private void LeaveGame(bool isRestart)
    {
        StopAllCoroutines();
        availableCards = new List<Card>(allCards);
        XearUtils.Shuffle(availableCards);
        for(int i = 0; i < availableCards.Count; i++)
        {
            availableCards[i].transform.position = transform.position;
            availableCards[i].transform.rotation = Quaternion.identity;
            availableCards[i].transform.localScale = Vector3.one;
            availableCards[i].transform.DOPause();
            availableCards[i].SetVisible(false);
        }
    }

}
