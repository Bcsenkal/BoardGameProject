
using UnityEngine;

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

        #region Player
        
        public event System.Action OnShowPlayerInfoPanel;

        public void ONOnShowPlayerInfoPanel()
        {
            OnShowPlayerInfoPanel?.Invoke();
        }
        #endregion

        #region Table Creation

        public event System.Action<TableInfo> OnShowCreateTablePanel;
        public event System.Action<TableCreationInfo> OnCreateTable;

        public void ONOnShowCreateTable(TableInfo tableCreationInfo)
        {
            OnShowCreateTablePanel?.Invoke(tableCreationInfo);
        }

        public void ONOnCreateTable(TableCreationInfo tableCreationInfo)
        {
            OnCreateTable?.Invoke(tableCreationInfo);
        }
        #endregion

        #region Card Dealing

        public event System.Action<PlayerHand> OnDealCardsToPlayer;

        public void ONOnDealCardsToPlayer(PlayerHand playerHand)
        {
            OnDealCardsToPlayer?.Invoke(playerHand);
        }
        #endregion
        








        //remove listeners from all of the events here
        public void NextLevelReset()
        {
            //Resource
            OnSetCurrentCoin = null;
            OnNotEnoughMoney = null;

            //Player
            OnShowPlayerInfoPanel = null;

            //Table Creation
            OnShowCreateTablePanel = null;
            
        }


        private void OnApplicationQuit() {
            NextLevelReset();
        }
    }
}
