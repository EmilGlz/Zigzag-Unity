using UnityEngine;

public class ProjectController
{
    private static ProjectController instance;
    public static ProjectController Instance { get => instance; set => instance = value; }
    public ProjectController()
    {
        instance = this;
    }
    public UserDatas UserDatas;
    public bool CanAddNewCrystal = true;
    public int CurrentCrystalCount;
    private bool _soundOn;

    public bool SoundOn { 
        get => _soundOn;
        set {
            _soundOn = value;
            Camera.main.GetComponent<AudioListener>().enabled = value;
        } 
    }

}
