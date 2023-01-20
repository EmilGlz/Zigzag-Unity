using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    #region Singleton
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }
    private void Awake()
    {
        _instance = this;
    }
    #endregion
    ObjectPooler _pooler;
    PlayerMovement _playerMovement;
    [SerializeField] Transform Canvas;
    [SerializeField] Transform GameOverCanvas;
    private void Start()
    {
        _playerMovement = PlayerMovement.Instance;
        _pooler = ObjectPooler.Instance;
    }
    public void TouchPressed()
    {
        if (!_playerMovement.hasStarted)
            _playerMovement.hasStarted = true;
        _playerMovement.movingRight = !_playerMovement.movingRight;
    }
    public void ShowGameOver()
    {
        GameOverCanvas.gameObject.SetActive(true);
        float animTime = .2f;
        Sequence mySequence = DOTween.Sequence();
        for (int i = 0; i < GameOverCanvas.childCount; i++)
        {
            Tween tween = GameOverCanvas.GetChild(i).GetComponent<RectTransform>().DOAnchorPosX(0f, animTime).SetDelay(0);
            mySequence.Append(tween);
        }
        mySequence.Play();
    }
    public void ShowPlusOneText(Transform crystalObj)
    {
        Vector3 objectiveScreenPos = Camera.main.WorldToScreenPoint(crystalObj.position);
        _pooler.SpawnFromPool("PlusOne", objectiveScreenPos, Canvas);
    }
}
