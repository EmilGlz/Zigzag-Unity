using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommonObjects : MonoBehaviour
{
    #region Singleton
    private static CommonObjects _instance;
    public static CommonObjects Instance { get { return _instance; } }
    private void Awake()
    {
        _instance = this;
    }
    #endregion
    public RectTransform mainMenuUpObjectsParent;
    public RectTransform mainMenuBottomObjectsParent;
    public Transform GameCanvas;
    public Transform MainMenuCanvas;
    public Transform GameOverCanvas;
    public Transform PauseCanvas;
    public Transform SettingsCanvas;
    public MapItem StartMapItem;
    public MapItem FirstMapItem;
    public TMP_Text CurrentCrystalCountText;
    public GameObject PauseButtonInGame;
    public TMP_Text CurrentScoreText_GameOver;
    public TMP_Text BestScoreText_GameOver;
    public GameObject autopilotToggle_Settings;
    public Image soundImageMainMenu;
    public List<Color> mapColorsEvery25Crystals;
    public Material mapItemMaterial;
}
