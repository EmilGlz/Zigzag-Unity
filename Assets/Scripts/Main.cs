using UnityEngine;

public class Main : MonoBehaviour
{
    void Start()
    {
        ProjectController.Instance = new ProjectController();
    }
}
