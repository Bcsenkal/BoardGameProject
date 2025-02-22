using System.Collections;
using System.Collections.Generic;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using Managers;

public class RocinanteAnimation : MonoBehaviour
{

    [SerializeField] private float horseYTarget;
    [SerializeField] private float horseRotationTime;
    private RectTransform _horse;
    private RectTransform _rocinanteText;
    

    void Start()
    {
        _horse = transform.GetChild(0).GetComponent<RectTransform>();
        _rocinanteText = transform.GetChild(1).GetComponent<RectTransform>();
    
        LMotion.Create(Vector3.zero, Vector3.one, .5f)
            .WithEase(Ease.OutBack)
            .WithDelay(1f)
            .BindToLocalScale(_rocinanteText);
        
        var startPosition = _horse.localPosition;
        var targetPosition = startPosition;
        targetPosition.y = horseYTarget;
        LMotion.Create(startPosition, targetPosition, .75f)
            .WithEase(Ease.InQuad)
            .BindToLocalPosition(_horse);

        LMotion.Create(0f, 15f, .5f)
            .WithEase(Ease.OutQuad)
            .WithDelay(.75f)
            .WithOnComplete(HorseRotationLoop)
            .BindToEulerAnglesZ(_horse);
        
        
        Invoke(nameof(PlaySfx),.3f);

    }

    private void HorseRotationLoop(){
        LMotion.Create(15f, -15f, .75f)
            .WithEase(Ease.OutQuad)
            .BindToEulerAnglesZ(_horse);
    }

    private void PlaySfx(){
        AudioManager.Instance.PlayRocinanteSfx();
    }



}