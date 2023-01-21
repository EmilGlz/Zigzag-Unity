using DG.Tweening;
using UnityEngine;
public class SettingsMenuController 
{
    private readonly float openAnimTime = 0.2f;
    private readonly float closeAnimTime = 0.08f;
    private readonly float outsidePosX = -Screen.width;
    private readonly float insidePosX = 0f;
    public void Open()
    {
        CommonObjects.Instance.SettingsCanvas.gameObject.SetActive(true);
        Sequence mySequence = DOTween.Sequence();
        for (int i = 0; i < CommonObjects.Instance.SettingsCanvas.childCount; i++)
        {
            Tween tween = CommonObjects.Instance.SettingsCanvas.GetChild(i).GetComponent<RectTransform>().DOAnchorPosX(insidePosX, openAnimTime).SetDelay(0);
            mySequence.Append(tween);
        }
        mySequence.Play();
    }

    public void Close()
    {
        CommonObjects.Instance.SettingsCanvas.gameObject.SetActive(true);
        Sequence mySequence = DOTween.Sequence();
        for (int i = 0; i < CommonObjects.Instance.SettingsCanvas.childCount; i++)
        {
            Tween tween = CommonObjects.Instance.SettingsCanvas.GetChild(i).GetComponent<RectTransform>().DOAnchorPosX(outsidePosX, closeAnimTime).SetDelay(0);
            mySequence.Append(tween);
        }
        mySequence.Play();
    }
}
