using UnityEngine;
using DG.Tweening;
using TMPro;

public class MainMenuController
{
    private RectTransform _upObjectsParent;
    private RectTransform _bottomObjectsParent;
    private TMP_Text _scoreText;
    private readonly float animTime = 1f;
    public MainMenuController(RectTransform upObjectsParent, RectTransform bottomObjectsParent)
    {
        _upObjectsParent = upObjectsParent;
        _bottomObjectsParent = bottomObjectsParent;
        _scoreText = _bottomObjectsParent.GetChild(0).GetComponent<TMP_Text>();
    }

    public void Open()
    {
        _scoreText.text = $"BEST SCORE: {ProjectController.Instance.BestScore}\n" +
            $"GAMES PLAYED: {ProjectController.Instance.GamesPlayed}";
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
