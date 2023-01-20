using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenuController
{
    private float openAnimTime = 0.2f;
    private float closeAnimTime = 0.2f;
    public void Open()
    {
        CommonObjects.Instance.GameOverCanvas.gameObject.SetActive(true);
        Sequence mySequence = DOTween.Sequence();
        for (int i = 0; i < CommonObjects.Instance.GameOverCanvas.childCount; i++)
        {
            Tween tween = CommonObjects.Instance.GameOverCanvas.GetChild(i).GetComponent<RectTransform>().DOAnchorPosX(0f, openAnimTime).SetDelay(0);
            mySequence.Append(tween);
        }
        mySequence.Play();
    }
    public void Close()
    {
        CommonObjects.Instance.GameOverCanvas.gameObject.SetActive(true);
        Sequence mySequence = DOTween.Sequence();
        for (int i = 0; i < CommonObjects.Instance.GameOverCanvas.childCount; i++)
        {
            Tween tween = CommonObjects.Instance.GameOverCanvas.GetChild(i).GetComponent<RectTransform>().DOAnchorPosX(0f, closeAnimTime).SetDelay(0);
            mySequence.Append(tween);
        }
        mySequence.Play();
    }
}
