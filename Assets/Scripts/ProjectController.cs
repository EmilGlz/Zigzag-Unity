public class ProjectController
{
    #region Singleton
    public static ProjectController Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion
    public UserDatas UserDatas;
    public bool CanAddNewCrystal = true;
    public int CurrentCrystalCount;
}
