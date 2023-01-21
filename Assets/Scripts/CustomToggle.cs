using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CustomToggle
{
    public CustomToggle(GameObject toggle, bool isOn = true, Action<bool> onClick = null, float animTime = 0.2f)
    {
        this.AnimTime = animTime;
        this.IsOn = isOn;
        OnClick = onClick;
        Circle = GuiUtils.FindGameObject("Circle", toggle).GetComponent<RectTransform>();
        OnText = GuiUtils.FindGameObject("OnText", toggle);
        OfText = GuiUtils.FindGameObject("OfText", toggle);
        var button = GuiUtils.FindGameObject("Toggle", toggle).GetComponent<Button>();
        _circleStartPos = Math.Abs(Circle.anchoredPosition.x);
        button.onClick.AddListener(() => { Clicked(); });


        if (!IsOn)
        {// turn off
            OnText.SetActive(false);
            OfText.SetActive(true);
            Circle.DOLocalMoveX(-1 * _circleStartPos, 0f);
        }
        else
        {// turn on
            OnText.SetActive(true);
            OfText.SetActive(false);
            Circle.DOLocalMoveX(1 * _circleStartPos, 0f);
        }

    }
    public RectTransform Circle { get; set; }
    public GameObject OnText { get; set; }
    public GameObject OfText { get; set; }
    public bool IsOn { get; set; }
    public Action<bool> OnClick { get; set; }
    public float AnimTime { get; set; } = 0.2f;
    private readonly float _circleStartPos;
    public void Clicked()
    {
        IsOn = !IsOn;
        if (!IsOn)
        {// turn off
            OnText.SetActive(false);
            OfText.SetActive(true);
            Circle.DOLocalMoveX(-1 * _circleStartPos, AnimTime);
        }
        else
        {// turn on
            OnText.SetActive(true);
            OfText.SetActive(false);
            Circle.DOLocalMoveX(1 * _circleStartPos, AnimTime);
        }
        OnClick?.Invoke(IsOn);
    }
}
