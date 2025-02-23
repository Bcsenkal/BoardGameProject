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
    public void CacheComponents()
    {
        cardVisual = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void SetCardInfo(CardType cardType, int cardNumber, Sprite cardSprite,Sprite cardBack)
    {
        this.cardType = cardType;
        this.cardNumber = cardNumber;
        this.cardSprite = cardSprite;
        this.cardBack = cardBack;
        cardVisual.sprite = cardBack;
    }

    public void SetVisible(bool isBot)
    {
        if(isBot) return;
        cardVisual.sprite = cardSprite;
    }
}
