using UnityEngine;

public class UIManager : MonoBehaviour
{
    PlayerMovement playerMovement;
    private void Start()
    {
        playerMovement = PlayerMovement.Instance;
    }
    public void TouchPressed()
    {
        if (!playerMovement.hasStarted)
            playerMovement.hasStarted = true;
        playerMovement.movingRight = !playerMovement.movingRight;
    }
}
