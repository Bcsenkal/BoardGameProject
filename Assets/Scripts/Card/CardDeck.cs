using UnityEngine;
using System.Collections.Generic;

public class CardDeck : MonoBehaviour
{
    [SerializeField]private Card cardPrefab;
    [SerializeField]private Sprite[] cardSprites;
    [SerializeField]private Sprite cardBackSprite;
    private List<Card> availableCards = new List<Card>();
    void Start()
    {
        CreateDeck();
        Managers.EventManager.Instance.OnDealCardsToPlayer += DealCardsToPlayer;
    }

    private void CreateDeck()
    {
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 13; j++)
            {
                var card = Instantiate(cardPrefab, transform.position, Quaternion.identity,transform).GetComponent<Card>();
                card.CacheComponents();
                card.SetCardInfo((CardType)i, j + 1, cardSprites[i * 13 + j], cardBackSprite);
                availableCards.Add(card);
            }
        }
        XearUtils.Shuffle(availableCards);
    }

    private void DealCardsToPlayer(PlayerHand playerHand)
    {
        var dealingList = new List<Card>(4);
        for(int i = 0; i < 4; i++)
        {
            var randomCard = availableCards[Random.Range(0, availableCards.Count - 1)];
            dealingList.Add(randomCard);
            availableCards.Remove(randomCard);
        }

        _ =playerHand.OnDealCards(dealingList);
    }


}
