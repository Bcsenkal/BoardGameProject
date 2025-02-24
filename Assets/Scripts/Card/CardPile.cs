using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using DG.Tweening;
public class CardPile : MonoBehaviour
{
    private List<Card> cardsOnPile = new List<Card>();
    private int cardCount;
    private bool initialCardsDrawn;
    private PlayerHand lastPlayer;

    void Start()
    {
        Managers.EventManager.Instance.OnCardDealingComplete += RequestInitialPile;
        Managers.EventManager.Instance.OnPlayCard += GetCardFromHand;
        Managers.EventManager.Instance.OnSendRemainingCardsToPlayer += SendRemainingCardsToPlayer;
        Managers.EventManager.Instance.OnLeaveGame += LeaveGame;
    }

    private void RequestInitialPile()
    {
        if(initialCardsDrawn) return;
        initialCardsDrawn = true;
        Managers.EventManager.Instance.ONOnRequestInitialPile(this);
    }

    public IEnumerator DrawCardsFromDeck(List<Card> cards)
    {
        for(int i = 0; i < cards.Count; i++)
        {
            cards[i].transform.localPosition = cards[i].transform.localPosition.WithReplace(z : (cardCount + 1) * -0.02f);
            if(i == cards.Count - 1)
            {
                cards[i].SetVisible(true);
            }
            cardsOnPile.Add(cards[i]);
            cardCount++;
            cards[i].transform.DOMove(new Vector3(transform.position.x,transform.position.y,cardCount * -0.02f),0.25f);
            cards[i].transform.DOLocalRotate(new Vector3(0,0,Random.Range(-10f,10f)),0.25f,RotateMode.Fast);
            yield return new WaitForSeconds(0.2f);
        }
        Managers.EventManager.Instance.ONOnReadyToPlay();
        Managers.EventManager.Instance.ONOnSkipTurn(cardsOnPile);
    }

    public void GetCardFromHand(Card card,PlayerHand player)
    {
        StartCoroutine(GetCard(card,player));
    }

    IEnumerator GetCard(Card card,PlayerHand player)
    {
        card.transform.localPosition = card.transform.localPosition.WithReplace(z : (cardCount + 1) * -0.02f);
        card.SetVisible(true);
        cardsOnPile.Add(card);
        cardCount++;
        card.transform.DOMove(new Vector3(transform.position.x,transform.position.y,cardCount * -0.02f),0.25f);
        card.transform.DOLocalRotate(new Vector3(0,0,Random.Range(-10f,10f)),0.25f,RotateMode.Fast);
        yield return new WaitForSeconds(0.3f);
        yield return CheckConditions(player);
        Managers.EventManager.Instance.ONOnSkipTurn(cardsOnPile);
    }

    IEnumerator CheckConditions(PlayerHand player)
    {
        var isMatch = false;
        var isCut = false;
        if(cardsOnPile.Count < 2)
        {
            yield return new WaitForSeconds(0.2f);
        }
        else
        {
            if(cardsOnPile[^1].GetCardNumber() == cardsOnPile[^2].GetCardNumber())
            {
                isCut = true;
                if(cardsOnPile.Count < 3)
                {
                    isMatch = true;
                }
            }
            else if(cardsOnPile[^1].GetCardNumber() == 11)
            {
                isCut = true;
            }
            yield return new WaitForSeconds(0.2f);
        }
        if(isCut)
        {
            yield return SendCardsToPlayer(player,isMatch);
        }
    }

    IEnumerator SendCardsToPlayer(PlayerHand player,bool isMatch)
    {
        lastPlayer = player;
        var point = isMatch ? 10 : 0;
        var sendingCards = new List<Card>(cardsOnPile);
        cardCount = 0;
        cardsOnPile.Clear();
        yield return player.GetCardsFromPile(sendingCards,point);
    }

    private void SendRemainingCardsToPlayer()
    {
        if(cardsOnPile.Count == 0)
        {
            GameOver();
            return;
        }
        StartCoroutine(SendCardsToPlayer(lastPlayer,false));
        Invoke(nameof(GameOver),1f);
    }

    private void GameOver()
    {
        Managers.EventManager.Instance.ONOnGameOver();
    }

    private void LeaveGame(bool isRestart)
    {
        StopAllCoroutines();
        initialCardsDrawn = false;
        cardCount = 0;
        cardsOnPile.Clear();
        lastPlayer = null;
    }
}
