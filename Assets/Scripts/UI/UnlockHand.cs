using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockHand : MonoBehaviour
{
    private RectTransform _rect;
    private float _startY;
    private Coroutine _handClick;
    void OnEnable()
    {
        _rect ??= transform.GetComponent<RectTransform>();

        Invoke(nameof(StartMovement), .25f);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(StartMovement));
        if(_handClick != null)
        {
            StopCoroutine(_handClick);
        }
    }

    private void StartMovement()
    {
        _startY = _rect.localPosition.y;
        _handClick = StartCoroutine(HandClickRoutine());
    }

    IEnumerator HandClickRoutine()
    {
        
        var currentPosition = _rect.localPosition;
        var currentRotation = _rect.localEulerAngles;

        var counter = 0f;
        while (counter <= .2f)
        {
            var t = Easings.QuadEaseOut(counter, 0f, 1f, .2f);
            currentPosition.y = Mathf.Lerp(_startY, _startY + 20f, t);
            _rect.localPosition = currentPosition;


            currentRotation.z = Mathf.Lerp(0f, 30f, t);
            _rect.localEulerAngles = currentRotation;
            counter += Time.deltaTime;
            yield return null;
        }

        counter = 0f;
        while (counter <= .75f)
        {
            var t = Easings.QuadEaseOut(counter, 0f, 1f, .75f);
            currentPosition.y = Mathf.Lerp(_startY + 20f, _startY , t);
            _rect.localPosition = currentPosition;


            currentRotation.z = Mathf.Lerp(30f, 0f, t);
            _rect.localEulerAngles = currentRotation;
            counter += Time.deltaTime;
            yield return null;
        }

        StartMovement();
    }


}
