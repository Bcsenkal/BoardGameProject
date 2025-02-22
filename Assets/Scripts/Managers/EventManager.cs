
using UnityEngine;

namespace Managers
{
    public sealed class EventManager : Singleton<EventManager>
    {
        
#region Level Status

        //DEFINE
        public event System.Action<bool> ONLevelEnd;
        public event System.Action ONLevelStart;
        
        //FUNCS
        public void OnONLevelStart(){
            ONLevelStart?.Invoke();
        }
        public void OnONLevelEnd(bool isSuccess)
        {
            ONLevelEnd?.Invoke(isSuccess);
        }
        
        public event System.Action ONLevelReload;

        public void OnONLevelReload()
        {
            ONLevelReload?.Invoke();
        }
        
        public event System.Action ONLevelComplete;

        public void OnONLevelComplete()
        {
            ONLevelComplete?.Invoke();
        }
        
        public event System.Action ONRevive;
        public void OnONRevive()
        {
            ONRevive?.Invoke();
        }
        
        public event System.Action ONPrivacyPolicyAccepted;

        public void OnONPrivacyPolicyAccepted()
        {
            ONPrivacyPolicyAccepted?.Invoke();
        }
        
        public event System.Action ONResumeGamePlay;
        public void OnONResumeGamePlay(){
            ONResumeGamePlay?.Invoke();
        }

#endregion
        //*********************************************************************
        #region VFX
        //DEFINE
        public event System.Action<Vector3> ONPlayParticleHere;
        
        //FUNCS
        public void OnONPlayParticleHere(Vector3 position)
        {
            ONPlayParticleHere?.Invoke(position);
        }
        #endregion
        //*********************************************************************
        #region Settings

        public event System.Action ONSettingsButtonPressed;

        public void OnONSettingsButtonPressed()
        {
            ONSettingsButtonPressed?.Invoke();
        }

        public event System.Action<bool> ONSettingsPanelOpened;

        public void OnONSettingsPanelOpened(bool isOpen)
        {
            ONSettingsPanelOpened?.Invoke(isOpen);
        }
        
        public event System.Action ONReloadButtonPressed;

        public void OnONReloadButtonPressed()
        {
            ONReloadButtonPressed?.Invoke();
        }
                
        

        #endregion
        //*********************************************************************
        #region Resource

        public event System.Action<int,bool> OnSetCurrentCoin;
            
        public void ONOnSetCurrentCoin(int amount,bool isIncrement)
        {
            OnSetCurrentCoin?.Invoke(amount,isIncrement);
        }

        public event System.Action ONPlayerMakeAMove;

        public void OnONPlayerMakeAMove()
        {
            ONPlayerMakeAMove?.Invoke();
        }

        public event System.Action<int> ONAddMoves;

        public void OnONAddMoves(int amount)
        {
            ONAddMoves?.Invoke(amount);
        }

        public event System.Action ONRefillMoves;

        public void OnONRefillMoves()
        {
            ONRefillMoves?.Invoke();
        }

        public event System.Action ONRanOutOfMoves;

        public void OnONRanOutOfMoves()
        {
            ONRanOutOfMoves?.Invoke();
        }

        #endregion
        //*********************************************************************
        #region Heart System
        public event System.Action ONHeartTimerCompleted;

        public void OnONHeartTimerCompleted()
        {
            ONHeartTimerCompleted?.Invoke();
        }
            
        public event System.Action ONHeartDecrease;
        public void OnONHeartDecrease()
        {
            ONHeartDecrease?.Invoke();
        }

        public event System.Action ONRanOutOfHeart;
            
        public void OnONRanOutOfHeart()
        {
            ONRanOutOfHeart?.Invoke();
        }

        public event System.Action ONHeartsRefilled;

        public void OnONHeartsRefilled()
        {
            ONHeartsRefilled?.Invoke();
        }
        #endregion








        //remove listeners from all of the events here
        public void NextLevelReset()
        {
            ONLevelStart= null;
            ONLevelEnd = null;
            ONLevelComplete = null;
            ONLevelReload = null;
            ONRevive = null;
            ONResumeGamePlay = null;
            ONPrivacyPolicyAccepted = null;
            
            //VFX
            ONPlayParticleHere = null;
            
            
            //Settings
            ONSettingsButtonPressed = null;
            ONSettingsPanelOpened = null;
            ONReloadButtonPressed = null;
            
            
            //Resource
            OnSetCurrentCoin = null;
            ONAddMoves = null;
            ONRefillMoves = null;
            ONRanOutOfMoves = null;
            ONPlayerMakeAMove = null;
            
            //Hearts
            ONHeartTimerCompleted = null;
            ONHeartDecrease = null;
            ONRanOutOfHeart = null;
            ONHeartsRefilled = null;
        }


        private void OnApplicationQuit() {
            NextLevelReset();
        }
    }
}
