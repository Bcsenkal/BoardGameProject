using UnityEngine;
using UnityEngine.Splines;
using System.Collections.Generic;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class PlayerHand : MonoBehaviour
{
    [SerializeField]private bool isBot;
    [SerializeField]private GameObject cardPrefab;
    private SplineContainer splineContainer;
    private int maxSize = 6;

    private List<Card> cards = new List<Card>();

    public void CacheComponents()
    {
        splineContainer = transform.GetChild(0).GetComponent<SplineContainer>();
    }

    [ContextMenu("Request Card")]
    private void RequestCard()
    {
        Managers.EventManager.Instance.ONOnDealCardsToPlayer(this);
    }

    public async Task OnDealCards(List<Card> dealingCards)
    {
        for(int i = 0; i < dealingCards.Count; i++)
        {
            dealingCards[i].transform.localPosition = dealingCards[i].transform.localPosition + Vector3.back * 0.2f;
            dealingCards[i].SetVisible(isBot);
            cards.Add(dealingCards[i]);
            UpdateCardPositions();
            await UniTask.Delay(200);
        }
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
}
