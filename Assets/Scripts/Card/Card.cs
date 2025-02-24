using UnityEngine;

public enum CardType
{
    Spades,
    Hearts,
    Diamonds,
    Clubs
}

public class Card : MonoBehaviour
{
    private CardType cardType;
    private int cardNumber;
    private Sprite cardBack;
    private Sprite cardSprite;
    private SpriteRenderer cardVisual;
    private PlayerHand player;
    private int cardPoint;
    public void CacheComponents()
    {
        cardVisual = transform.GetChild(0).GetComponent<SpriteRenderer>();
        Managers.EventManager.Instance.OnCardClick += PlayCardOnClick;
    }

    public void SetCardInfo(CardType type, int number, Sprite sprite,Sprite back,int point)
    {
        cardType = type;
        cardNumber = number;
        cardSprite = sprite;
        cardBack = back;
        cardVisual.sprite = cardBack;
        cardPoint = point;
    }

    public CardType GetCardType()
    {
        return cardType;
    }

    public int GetCardNumber()
    {
        return cardNumber;
    }

    public int GetPoint()
    {
        return cardPoint;
    }

    public void SetVisible(bool isVisible)
    {
        cardVisual.sprite = isVisible ? cardSprite : cardBack;
    }

    private void PlayCardOnClick(Card card)
    {
        if(card != this) return;
        if(!player.CanPlay()) return;
        PlayCard();
    }

    public void PlayCard()
    {
        Managers.EventManager.Instance.ONOnPlayCard(this,player);
        player.DisablePlaying(this);
        SetPlayer(null);
    }

    public void SetPlayer(PlayerHand p)
    {
        player = p;
    }

    
}
