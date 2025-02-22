using System.Collections;
//using Facebook.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_ANDROID
using UnityEngine.Android;
#endif


namespace Managers
{
    //********************************************************************************BEWARE Uncomment the lines only After installing the SDK

    
    public class LoadingManager : MonoBehaviour
    {
        
        void Awake ()
        {
            Application.targetFrameRate = 60;
            //InitFacebook();
            StartCoroutine(LoadingRoutine());
        }
/*
        private void InitFacebook()
        {
            if (!FB.IsInitialized) {
                // Initialize the Facebook SDK
                FB.Init(InitCallback);
            } else {
                // Already initialized, signal an app activation App Event
                FB.ActivateApp();
            }
        }
        

        private void InitCallback ()
        {
            if (FB.IsInitialized) {
                // Signal an app activation App Event
                FB.Mobile.SetAdvertiserTrackingEnabled(true);
                FB.ActivateApp();
                // Continue with Facebook SDK
                // ...
            } else {
                Debug.Log("Failed to Initialize the Facebook SDK");
            }
        }
        
*/
        private IEnumerator LoadingRoutine()
        {
            //TinySauce.SubscribeOnInitFinishedEvent( OnInitFinished );
            var index = PlayerPrefs.GetInt("LastLevel",1);
            yield return new WaitForSeconds(1f);
#if UNITY_ANDROID && !UNITY_EDITOR
            if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
            {
                Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
            }
#endif
            yield return new WaitForSeconds(2f);
            /*
            if (!PlayerPrefs.HasKey("FirstPlayStart"))
            {
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start,"GameFirstOpen");
                PlayerPrefs.SetInt("FirstPlayStart",1);
            }
            */
            SceneManager.LoadScene(index);
        }
        
        private void OnInitFinished(bool arg1, bool arg2)
        {
            
        }
        
    }
    
}
