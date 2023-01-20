using UnityEngine;
using DG.Tweening;
public class MainMenuController
{
    RectTransform _upObjectsParent;
    RectTransform _bottomObjectsParent;
    private readonly float animTime = 1f;

    public MainMenuController(RectTransform upObjectsParent, RectTransform bottomObjectsParent)
    {
        _upObjectsParent = upObjectsParent;
        _bottomObjectsParent = bottomObjectsParent;
    }

    public void Open()
    {
        _upObjectsParent.anchoredPosition = new Vector2(0, ProjectController.Instance.ScreenHeight);
        _upObjectsParent.DOMoveY(0, animTime);
        _bottomObjectsParent.anchoredPosition = new Vector2(0, -ProjectController.Instance.ScreenHeight);
        _bottomObjectsParent.DOMoveY(0, animTime);
    }

    public void Close()
    {
        _upObjectsParent.anchoredPosition = new Vector2(0, 0);
        _upObjectsParent.DOMoveY(ProjectController.Instance.ScreenHeight, animTime);
        _bottomObjectsParent.anchoredPosition = new Vector2(0, 0);
        _bottomObjectsParent.DOMoveY(-ProjectController.Instance.ScreenHeight, animTime);
    }
}
