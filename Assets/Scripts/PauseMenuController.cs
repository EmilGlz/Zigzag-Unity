using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController
{
    List<RectTransform> movingObjects;
    private readonly float openAnimTime = 0.2f;
    private readonly float closeAnimTime = 0.08f;
    private readonly float outsidePosX = Screen.width;
    private readonly float insidePosX = 0f;
    public void Open()
    {
        CommonObjects.Instance.PauseCanvas.gameObject.SetActive(true);
        Sequence mySequence = DOTween.Sequence();
        for (int i = 0; i < CommonObjects.Instance.PauseCanvas.childCount; i++)
        {
            Tween tween = CommonObjects.Instance.PauseCanvas.GetChild(i).GetComponent<RectTransform>().DOAnchorPosX(insidePosX, openAnimTime).SetDelay(0);
            mySequence.Append(tween);
        }
        mySequence.Play();
    }
    public void Close(bool withAnimation = true)
    {
        CommonObjects.Instance.PauseCanvas.gameObject.SetActive(true);
        if (withAnimation)
        {
            Sequence mySequence = DOTween.Sequence();
            for (int i = 0; i < CommonObjects.Instance.PauseCanvas.childCount; i++)
            {
                Tween tween = CommonObjects.Instance.PauseCanvas.GetChild(i).GetComponent<RectTransform>().DOAnchorPosX(outsidePosX, closeAnimTime).SetDelay(0);
                mySequence.Append(tween);
            }
            mySequence.Play();
        }
        else
        {
            for (int i = 0; i < CommonObjects.Instance.PauseCanvas.childCount; i++)
            {
                //CommonObjects.Instance.PauseCanvas.GetChild(i).GetComponent<RectTransform>().anchoredPosition = new  outsidePosX
            }
        }
    }
}
