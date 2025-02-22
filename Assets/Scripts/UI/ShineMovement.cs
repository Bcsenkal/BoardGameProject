using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ShineMovement : MonoBehaviour
{
    [SerializeField] private float moveDuration;
    private RectTransform _thisRect;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private Vector3 _currentPosition;
    private Coroutine _shineMovement;
    private Image _image;


    void Start()
    {
        _thisRect = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        _startPosition = _thisRect.localPosition;
        _endPosition = _startPosition;
        _endPosition.x += 350f;
        //StartMovement();
    }

    public void StopMovement()
    {
        
        _image.enabled = false;
        if (_shineMovement == null) return;
        StopCoroutine(_shineMovement);
    }

    public void StartMovement()
    {
        MoveRoutine();
    }

    async UniTask MoveRoutine()
    {
        var counter = 0f;
        _thisRect.localPosition = _startPosition;
        _image.enabled = true;
        _currentPosition = _startPosition;
        _image.enabled = true;
        while (counter <= moveDuration)
        {
            var t = Easings.QuadEaseOut(counter, 0f, 1f, moveDuration);
            _currentPosition.x = Mathf.Lerp(_startPosition.x, _endPosition.x, t);
            _thisRect.localPosition = _currentPosition;
            counter += Time.deltaTime;
            await UniTask.Yield();
        }

        
        await UniTask.Delay(TimeSpan.FromSeconds(3f), ignoreTimeScale: false);
        StartMovement();
    }

}
