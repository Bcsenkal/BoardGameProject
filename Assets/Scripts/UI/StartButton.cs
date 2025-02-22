using System;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities
{
    public class StartButton : MonoBehaviour
    {
	    private readonly Vector3 _scaleFactor = new Vector3(1.1f, 1.1f, 1.1f);
	    private readonly Vector3 _defaultScale = new Vector3(1f, 1f, 1f);
	    private bool _isGameStarted = false;

		
		private void Start()
		{
			
			GetComponent<Button>().onClick.AddListener(StartGame);
		}
		
		private void StartGame()
		{
			EventManager.Instance.OnONLevelStart();
			GetComponent<Button>().onClick.RemoveListener(StartGame);
			
			AudioManager.Instance.PlayButtonClick();
			gameObject.SetActive(false);
		}
		
		
		
    }
}