using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    [SerializeField] Transform GameCanvas;
    [SerializeField] Transform MainMenuCanvas;
    [SerializeField] Transform GameOverCanvas;
    [SerializeField] Transform UIButtonPositionsParent;
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
        _pooler.SpawnFromPool("PlusOne", objectiveScreenPos, GameCanvas);
    }
    public void RetryPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OpenMainMenu()
    {
        
    }

    public void SoundTogglePressed()
    {
        ProjectController.Instance.SoundOn = !ProjectController.Instance.SoundOn;
        EventSystem.current.currentSelectedGameObject.transform.GetComponentInChildren<Image>().fillAmount = ProjectController.Instance.SoundOn ? 1f : 0.6f;
    }
}
