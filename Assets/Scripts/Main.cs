using UnityEngine;

public class Main : MonoBehaviour
{
    void Start()
    {
        ProjectController.Instance = new ProjectController();
        var res = StorageHandler.GetUser();
        if (res != null) // getting datas from local
        {
            ProjectController.Instance.UserDatas = res;
        }
        else // there is no data in local
        {
            ProjectController.Instance.UserDatas = new UserDatas
            {
                BestScore = 0
            };
        }
    }
}
