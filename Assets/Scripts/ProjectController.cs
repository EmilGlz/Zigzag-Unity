using System;
using UnityEngine;

public class ProjectController
{
    private static ProjectController instance;
    public static ProjectController Instance { get => instance; set => instance = value; }
    public ProjectController()
    {
        instance = this;
        _bestScore = PlayerPrefs.GetInt("BestScore");
        _gamesPlayed = PlayerPrefs.GetInt("GamesPlayed");
        _autopilotOn = PlayerPrefs.GetInt("Autopilot") == 1;
        _soundOn = PlayerPrefs.GetInt("SoundOn") == 1;
    }
    private int _bestScore;
    private int _gamesPlayed;
    private int _currentScore;
    private UIState _uiState;
    private bool _soundOn;
    private bool _autopilotOn;
    public bool CanAddNewCrystal = true;
    public int ChangeColorEveryCrystalCount { get => 10; }
    public bool SoundOn
    {
        get => _soundOn;
        set
        {
            _soundOn = value;
            Camera.main.GetComponent<AudioListener>().enabled = value;
            PlayerPrefs.SetInt("SoundOn", value ? 1 : 0);
        }
    }
    public int BestScore
    {
        get => _bestScore;
        set
        {
            _bestScore = value;
            PlayerPrefs.SetInt("BestScore", _bestScore);
        }
    }
    public int CurrentCrystalCount
    {
        get => _currentScore;
        set
        {
            _currentScore = value;
            if (_currentScore > _bestScore)
                BestScore = _currentScore;
        }
    }
    public float ScreenHeight
    {
        get => Screen.height;
    }
    public float ScreenWidth
    {
        get => Screen.width;
    }
    public UIState UIState
    {
        get => _uiState; set
        {
            _uiState = value;
            UIStateChanged?.Invoke();
        }
    }
    public bool AutopilotOn
    {
        get => _autopilotOn; 
        set
        {
            _autopilotOn = value;
            PlayerPrefs.SetInt("Autopilot", value ? 1 : 0);
        }
    }
    public int GamesPlayed
    {
        get => _gamesPlayed; 
        set { 
            _gamesPlayed = value;
            PlayerPrefs.SetInt("GamesPlayed", _gamesPlayed);
        }
    }
    public Action UIStateChanged;
}
public enum UIState
{
    Playing,
    Paused,
    MainMenu,
    GameOver
}