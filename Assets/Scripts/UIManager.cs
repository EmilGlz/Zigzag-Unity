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
    PauseMenuController pauseMenuController;
    SettingsMenuController settingsMenuController;
    CustomToggle autopilotToggle;
    private void Start()
    {
        _playerMovement = PlayerMovement.Instance;
        _pooler = ObjectPooler.Instance;
        mainMenuController = new MainMenuController(CommonObjects.Instance.mainMenuUpObjectsParent, CommonObjects.Instance.mainMenuBottomObjectsParent);
        gameOverMenuController = new GameOverMenuController(CommonObjects.Instance.CurrentScoreText_GameOver, CommonObjects.Instance.BestScoreText_GameOver);
        pauseMenuController = new PauseMenuController();
        settingsMenuController = new SettingsMenuController();
        ProjectController.Instance.UIStateChanged += UpdateCurrentCrystalCountText;
        SetDefaultCanvases();
        ProjectController.Instance.UIState = UIState.MainMenu;
        autopilotToggle = new CustomToggle(CommonObjects.Instance.autopilotToggle_Settings, ProjectController.Instance.AutopilotOn, AutopilotToggleChanged);
        CommonObjects.Instance.soundImageMainMenu.fillAmount = ProjectController.Instance.SoundOn ? 1f : 0.6f;
        mainMenuController.Open();
    }
    private void SetDefaultCanvases()
    {
        CommonObjects.Instance.GameCanvas.gameObject.SetActive(true);
        CommonObjects.Instance.MainMenuCanvas.gameObject.SetActive(true);
        CommonObjects.Instance.GameOverCanvas.gameObject.SetActive(false);
        CommonObjects.Instance.PauseCanvas.gameObject.SetActive(false);
        CommonObjects.Instance.SettingsCanvas.gameObject.SetActive(false);
    }
    public void TouchPressed()
    {
        if (!_playerMovement.movingAllowed)
            return;
        if (!_playerMovement.hasStarted)
        {
            _playerMovement.hasStarted = true;
            mainMenuController.Close();
            ProjectController.Instance.UIState = UIState.Playing;
        }
        if (!ProjectController.Instance.AutopilotOn)
            _playerMovement.movingRight = !_playerMovement.movingRight;
    }
    public void ShowGameOver()
    {
        gameOverMenuController.Open(ProjectController.Instance.CurrentCrystalCount, ProjectController.Instance.BestScore);
        ProjectController.Instance.UIState = UIState.GameOver;
    }
    public void ShowPlusOneText(Transform crystalObj)
    {
        Vector3 objectiveScreenPos = Camera.main.WorldToScreenPoint(crystalObj.position);
        _pooler.SpawnFromPool("PlusOne", objectiveScreenPos, CommonObjects.Instance.GameCanvas);
    }
    public void RetryPressed()
    {
        gameOverMenuController.Close();
        mainMenuController.Open();
        ProjectController.Instance.UIState = UIState.MainMenu;
        ResetEverything();
    }
    public void SoundTogglePressed()
    {
        ProjectController.Instance.SoundOn = !ProjectController.Instance.SoundOn;
        CommonObjects.Instance.soundImageMainMenu.fillAmount = ProjectController.Instance.SoundOn ? 1f : 0.6f;
    }
    public void UpdateCurrentCrystalCountText()
    {
        var isOn = ProjectController.Instance.UIState == UIState.Playing;
        CommonObjects.Instance.PauseButtonInGame.SetActive(isOn);
        CommonObjects.Instance.CurrentCrystalCountText.gameObject.SetActive(isOn);
        CommonObjects.Instance.CurrentCrystalCountText.text = ProjectController.Instance.CurrentCrystalCount.ToString();
    }
    public void PauseGame()
    {
        _playerMovement.StopMoving();
        pauseMenuController.Open();
    }
    public void ResumeGame()
    {
        _playerMovement.movingAllowed = true;
        pauseMenuController.Close();
    }
    public void OpenMainMenuFromPauseMenu()
    {
        pauseMenuController.Close();
        mainMenuController.Open();
        ProjectController.Instance.UIState = UIState.MainMenu;
        ResetEverything();
    }

    private void ResetEverything()
    {
        _pooler.DequeueAllObjectsFromPool("mapItem");
        CommonObjects.Instance.StartMapItem.ResetPosition();
        CommonObjects.Instance.FirstMapItem.ResetPosition();
        _playerMovement.ResetPlayer();
        MapGenerator.Instance.ResetItems();
        ProjectController.Instance.CurrentCrystalCount = 0;
        UpdateCurrentCrystalCountText();
    }

    public void OpenSettingsPressed()
    {
        CommonObjects.Instance.SettingsCanvas.gameObject.SetActive(true);
        mainMenuController.Close();
        settingsMenuController.Open();
    }
    public void CloseSettingsPressed()
    {
        mainMenuController.Open();
        settingsMenuController.Close();
    }
    void AutopilotToggleChanged(bool isOn)
    {
        ProjectController.Instance.AutopilotOn = isOn;
    }
}
