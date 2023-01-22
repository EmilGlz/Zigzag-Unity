using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverMenuController
{
    private readonly float openAnimTime = 0.2f;
    private readonly float closeAnimTime = 0.08f;
    private readonly float outsidePosX = Screen.width;
    private readonly float insidePosX = 0f;
    private TMP_Text _currentScoreText;
    private TMP_Text _bestScoreText;

    public GameOverMenuController(TMP_Text currentScoreText, TMP_Text bestScoreText)
    {
        _currentScoreText = currentScoreText;
        _bestScoreText = bestScoreText;
    }

    public void Open(int currentScore, int bestScore, bool withAnimation = true)
    {
        _currentScoreText.text = currentScore.ToString();
        _bestScoreText.text = bestScore.ToString();
        CommonObjects.Instance.GameOverCanvas.gameObject.SetActive(true);
        if(withAnimation)
        {
            Sequence mySequence = DOTween.Sequence();
            for (int i = 0; i < CommonObjects.Instance.GameOverCanvas.childCount; i++)
            {
                Tween tween = CommonObjects.Instance.GameOverCanvas.GetChild(i).GetComponent<RectTransform>().DOAnchorPosX(insidePosX, openAnimTime).SetDelay(0);
                mySequence.Append(tween);
            }
            mySequence.Play();
        }else
        {
            for (int i = 0; i < CommonObjects.Instance.GameOverCanvas.childCount; i++)
            {
                var rect = CommonObjects.Instance.GameOverCanvas.GetChild(i).GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector2(0, rect.anchoredPosition.y);
            }
        }
    }
    public void Close(bool withAnimation = true)
    {
        CommonObjects.Instance.GameOverCanvas.gameObject.SetActive(true);
        if(withAnimation)
        {
            Sequence mySequence = DOTween.Sequence();
            for (int i = 0; i < CommonObjects.Instance.GameOverCanvas.childCount; i++)
            {
                Tween tween = CommonObjects.Instance.GameOverCanvas.GetChild(i).GetComponent<RectTransform>().DOAnchorPosX(outsidePosX, closeAnimTime).SetDelay(0);
                mySequence.Append(tween);
            }
            mySequence.Play();
        }
        else{
            for (int i = 0; i < CommonObjects.Instance.GameOverCanvas.childCount; i++)
            {
                var rect = CommonObjects.Instance.GameOverCanvas.GetChild(i).GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector2(outsidePosX, rect.anchoredPosition.y);
            }
        }
    }
}
