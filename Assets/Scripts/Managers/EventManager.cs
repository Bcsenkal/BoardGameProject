
using UnityEngine;
using System.Collections.Generic;
namespace Managers
{
    public sealed class EventManager : Singleton<EventManager>
    {
        //*********************************************************************
        #region Resource

        public event System.Action<int,bool> OnSetCurrentCoin;
        public event System.Action OnNotEnoughMoney;
            
        public void ONOnSetCurrentCoin(int amount,bool isIncrement)
        {
            OnSetCurrentCoin?.Invoke(amount,isIncrement);
        }

        public void ONOnNotEnoughMoney()
        {
            OnNotEnoughMoney?.Invoke();
        }

        #endregion

        #region Gameplay

        public event System.Action<List<Card>> OnSkipTurn;
        public event System.Action<Card,PlayerHand> OnPlayCard;
        public event System.Action OnSendRemainingCardsToPlayer;
        public event System.Action OnGameOver;
        public event System.Action OnShowSettingsPanel;
        public event System.Action OnHideSettingsPanel;
        public event System.Action<bool> OnLeaveGame;
        public event System.Action<bool> OnShowEndScreen;

        public void ONOnSkipTurn(List<Card> cards)
        {
            OnSkipTurn?.Invoke(cards);
        }

        public void ONOnPlayCard(Card card,PlayerHand hand)
        {
            OnPlayCard?.Invoke(card,hand);
        }

        public void ONOnSendRemainingCardsToPlayer()
        {
            OnSendRemainingCardsToPlayer?.Invoke();
        }

        public void ONOnGameOver()
        {
            OnGameOver?.Invoke();
        }

        public void ONOnShowSettingsPanel()
        {
            OnShowSettingsPanel?.Invoke();
        }

        public void ONOnHideSettingsPanel()
        {
            OnHideSettingsPanel?.Invoke();
        }

        public void ONOnLeaveGame(bool isRestart)
        {
            OnLeaveGame?.Invoke(isRestart);
        }

        public void ONOnShowEndScreen(bool isWin)
        {
            OnShowEndScreen?.Invoke(isWin);
        }

        #endregion

        #region Player
        
        public event System.Action OnShowPlayerInfoPanel;
        public event System.Action<Vector2> OnClick;
        public event System.Action<Card> OnCardClick;
        

        public void ONOnShowPlayerInfoPanel()
        {
            OnShowPlayerInfoPanel?.Invoke();
        }

        public void ONOnClick(Vector2 pos)
        {
            OnClick?.Invoke(pos);
        }

        public void ONOnCardClick(Card card)
        {
            OnCardClick?.Invoke(card);
        }

        
        #endregion

        #region Table Creation

        public event System.Action<TableInfo> OnShowCreateTablePanel;
        public event System.Action<TableCreationInfo> OnCreateTable;
        public event System.Action OnShowTableScreen;

        public void ONOnShowCreateTable(TableInfo tableCreationInfo)
        {
            OnShowCreateTablePanel?.Invoke(tableCreationInfo);
        }

        public void ONOnCreateTable(TableCreationInfo tableCreationInfo)
        {
            OnCreateTable?.Invoke(tableCreationInfo);
        }

        public void ONOnShowTableScreen()
        {
            OnShowTableScreen?.Invoke();
        }
        #endregion

        #region Card Dealing

        public event System.Action<PlayerHand> OnDealCardsToPlayer;
        public event System.Action OnCardDealingComplete;
        public event System.Action<CardPile> OnRequestInitialPile;
        public event System.Action OnReadyToPlay;
        public event System.Action OnOutOfCards;

        public void ONOnDealCardsToPlayer(PlayerHand playerHand)
        {
            OnDealCardsToPlayer?.Invoke(playerHand);
        }

        public void ONOnCardDealingComplete()
        {
            OnCardDealingComplete?.Invoke();
        }

        public void ONOnRequestInitialPile(CardPile cardPile)
        {
            OnRequestInitialPile?.Invoke(cardPile);
        }

        public void ONOnReadyToPlay()
        {
            OnReadyToPlay?.Invoke();
        }

        public void ONOnOutOfCards()
        {
            OnOutOfCards?.Invoke();
        }
        #endregion
        








        //remove listeners from all of the events here
        public void NextLevelReset()
        {
            //Resource
            OnSetCurrentCoin = null;
            OnNotEnoughMoney = null;



            //Gameplay
            OnSkipTurn = null;
            OnPlayCard = null;
            OnSendRemainingCardsToPlayer = null;
            OnGameOver = null;
            OnShowSettingsPanel = null;
            OnHideSettingsPanel = null;
            OnLeaveGame = null;

            //Card Dealing
            OnDealCardsToPlayer = null;
            OnCardDealingComplete = null;
            OnRequestInitialPile = null;
            OnReadyToPlay = null;
            OnOutOfCards = null;


            //Player
            OnShowPlayerInfoPanel = null;
            OnClick = null;
            OnCardClick = null;


            //Table Creation
            OnShowCreateTablePanel = null;
            OnCreateTable = null;
            OnShowTableScreen = null;
            
            
        }


        private void OnApplicationQuit() {
            NextLevelReset();
        }
    }
}
