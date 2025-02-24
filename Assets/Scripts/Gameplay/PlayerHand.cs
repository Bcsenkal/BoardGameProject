using UnityEngine;
using UnityEngine.Splines;
using System.Collections.Generic;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using System.Linq;
using System.Collections;

public class PlayerHand : MonoBehaviour
{
    public bool IsPlaying{get; set;}
    [SerializeField]private bool isBot;
    [SerializeField]private GameObject cardPrefab;
    private SplineContainer splineContainer;
    private IngamePlayerInfo ingamePlayerInfo;
    private int maxSize = 6;

    private List<Card> cards = new List<Card>();
    private List<Card> orderedCards = new List<Card>();

    private Transform incomingCardTarget;
    private int totalPoints;

    public void CacheComponents()
    {
        splineContainer = transform.GetChild(0).GetComponent<SplineContainer>();
        incomingCardTarget = transform.GetChild(1);
        ingamePlayerInfo = transform.GetChild(2).GetComponent<IngamePlayerInfo>();
        ingamePlayerInfo.CacheComponents();
        ingamePlayerInfo.IsBot = isBot;
        Managers.EventManager.Instance.OnLeaveGame += LeaveGame;
    }

    public IEnumerator OnDealCards(List<Card> dealingCards)
    {
        for(int i = 0; i < dealingCards.Count; i++)
        {
            dealingCards[i].transform.localPosition = dealingCards[i].transform.localPosition + Vector3.back * 0.2f;
            dealingCards[i].SetVisible(!isBot);
            cards.Add(dealingCards[i]);
            UpdateCardPositions();
            yield return new WaitForSeconds(0.2f);
        }
        orderedCards = cards.OrderByDescending(x => x.GetPoint()).ToList();
    }

    private void UpdateCardPositions()
    {
        if(cards.Count < 1) return;
        float cardSpacing = 1f/ maxSize;

        float firstCardPosition = 0.5f - (cards.Count - 1) * cardSpacing * 0.5f;
        var spline = splineContainer.Spline;

        for(int i = 0; i < cards.Count; i++)
        {
            float pos = firstCardPosition + i * cardSpacing;
            float depth = i * -0.05f;
            Vector3 splinePos = spline.EvaluatePosition(pos);
            Vector3 forward = spline.EvaluateTangent(pos);
            Vector3 up = spline.EvaluateUpVector(pos);
            Quaternion rot = Quaternion.LookRotation(up, Vector3.Cross(up, forward).normalized);
            var cardPos = new Vector3(splinePos.x, splinePos.y, depth);
            cards[i].transform.DOMove(cardPos,0.25f);
            cards[i].transform.DORotateQuaternion(rot,0.25f);
        }
    }

    public IEnumerator GetCardsFromPile(List<Card> incomingCards, int point)
    {
        totalPoints += point;
        for(int i = 0; i< incomingCards.Count; i++)
        {
            incomingCards[i].transform.DOMove(incomingCardTarget.position,0.25f);
            incomingCards[i].transform.DOScale(Vector3.zero,0.3f);
            totalPoints += incomingCards[i].GetPoint();
        }
        yield return new WaitForSeconds(0.3f);
        ingamePlayerInfo.UpdateScore(totalPoints);
    }

    public bool CanPlay()
    {
        return !isBot && IsPlaying;
    }

    public void DisablePlaying(Card card)
    {
        cards.Remove(card);
        orderedCards.Remove(card);
        UpdateCardPositions();
        IsPlaying = false;
    }

    public int GetHandCount()
    {
        return cards.Count;
    }

    public int GetPoint()
    {
        return totalPoints;
    }

    public IEnumerator PlayTurn(List<Card> cardsOnPile)
    {
        IsPlaying = true;
        if(!isBot) yield break;
        yield return new WaitForSeconds(0.5f);
        var playingCard = GetPlayingCard(cardsOnPile);
        cards.Remove(playingCard);
        orderedCards.Remove(playingCard);
        UpdateCardPositions();
        Managers.EventManager.Instance.ONOnPlayCard(playingCard,this);
    }

    private Card GetPlayingCard(List<Card> cardsOnPile)
    {
        Card card = null;
        var cardCount = cardsOnPile.Count;
        if(cardCount == 0 || cardsOnPile == null)
        {
            card = orderedCards[^1];
        }
        else if(cardCount == 1)
        {
            var match = cards.Find(x => x.GetCardNumber() == cardsOnPile[^1].GetCardNumber());
            if(match != null)
            {
                card = match;
            }
            else
            {
                var playables = cards.FindAll(x => x.GetCardNumber() != 11);
                if(playables.Count < 1)
                {
                    card = orderedCards[^1];
                }
                else
                {
                    card = playables[Random.Range(0, playables.Count - 1)];
                }
            }
        }
        else
        {
            var match = cards.Find(x => x.GetCardNumber() == cardsOnPile[^1].GetCardNumber());
            if(match != null)
            {
                card = match;
            }
            else
            {
                var jack = cards.Find(x => x.GetCardNumber() == 11);
                if(jack != null)
                {
                    if(cardCount > 5)
                    {
                        card = jack;
                    }
                    else
                    {
                        var point = cardsOnPile.Find(x => x.GetPoint() > 0);
                        if(point != null)
                        {
                            card = jack;
                        }
                        else
                        {
                            card = orderedCards[^1];
                        }
                    }
                }
                
                else
                {
                    card = orderedCards[^1];
                }
            }
        }
        
        return card;
    }
    private void LeaveGame(bool isRestart)
    {
        StopAllCoroutines();
        cards.Clear();
        orderedCards.Clear();
        UpdateCardPositions();
        IsPlaying = false;
        totalPoints = 0;
        ingamePlayerInfo.UpdateScore(totalPoints);
    }
}
