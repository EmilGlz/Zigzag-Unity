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
    MainMenuController mainMenuController;
    GameOverMenuController gameOverMenuController;
    private void Start()
    {
        _playerMovement = PlayerMovement.Instance;
        _pooler = ObjectPooler.Instance;
        mainMenuController = new MainMenuController(CommonObjects.Instance.mainMenuUpObjectsParent, CommonObjects.Instance.mainMenuBottomObjectsParent);
        gameOverMenuController = new GameOverMenuController();
    }
    public void TouchPressed()
    {
        if (!_playerMovement.hasStarted)
        { 
            _playerMovement.hasStarted = true;
            mainMenuController.Close();
        }
        _playerMovement.movingRight = !_playerMovement.movingRight;
    }
    public void ShowGameOver()
    {
        gameOverMenuController.Open();
    }
    public void ShowPlusOneText(Transform crystalObj)
    {
        Vector3 objectiveScreenPos = Camera.main.WorldToScreenPoint(crystalObj.position);
        _pooler.SpawnFromPool("PlusOne", objectiveScreenPos, CommonObjects.Instance.GameCanvas);
    }
    public void RetryPressed()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverMenuController.Close();
        OpenMainMenu();
    }
    public void OpenMainMenu()
    {
        mainMenuController.Open();
    }
    public void SoundTogglePressed()
    {
        ProjectController.Instance.SoundOn = !ProjectController.Instance.SoundOn;
        EventSystem.current.currentSelectedGameObject.transform.GetComponentInChildren<Image>().fillAmount = ProjectController.Instance.SoundOn ? 1f : 0.6f;
    }
}
