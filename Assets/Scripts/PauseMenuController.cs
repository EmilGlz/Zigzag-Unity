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
    public void Open(bool withAnimation = true)
    {
        CommonObjects.Instance.PauseCanvas.gameObject.SetActive(true);
        if (withAnimation)
        {
            Sequence mySequence = DOTween.Sequence();
            for (int i = 0; i < CommonObjects.Instance.PauseCanvas.childCount; i++)
            {
                Tween tween = CommonObjects.Instance.PauseCanvas.GetChild(i).GetComponent<RectTransform>().DOAnchorPosX(insidePosX, openAnimTime).SetDelay(0);
                mySequence.Append(tween);
            }
            mySequence.Play();
        }
        else 
        {
            for (int i = 0; i < CommonObjects.Instance.PauseCanvas.childCount; i++)
            {
                var rect = CommonObjects.Instance.PauseCanvas.GetChild(i).GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector2(0, rect.anchoredPosition.y);
            }
        }
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
                var rect = CommonObjects.Instance.PauseCanvas.GetChild(i).GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector2(outsidePosX, rect.anchoredPosition.y);
            }
        }
    }
}
