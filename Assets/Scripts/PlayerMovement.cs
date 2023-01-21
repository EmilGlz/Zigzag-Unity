using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Singleton
    private static PlayerMovement _instance;
    public static PlayerMovement Instance { get { return _instance; } }
    private void Awake()
    {
        _instance = this;
    }
    #endregion
    [SerializeField] private float _speed;
    [SerializeField] private Transform test;
    Rigidbody rb;
    public bool hasStarted;
    public bool isAlive;
    public bool movingAllowed; // for pause/resume game
    public bool movingRight;
    private Vector3 startPos;
    private Vector3 lastMoveDirection;
    public Transform nextCornerDestination;
    public bool xReached, zReached;

    void Start()
    {
        //MapGenerator.Instance.pathCorners.Enqueue(currentItem);

        rb = GetComponent<Rigidbody>();
        isAlive = true;
        movingAllowed = true;
        startPos = transform.position;
        nextCornerDestination = MapGenerator.Instance.firstItem;
    }
    void Update()
    {
        if (!movingAllowed)
            return;
        if (ProjectController.Instance.AutopilotOn)
        { 
            CalculateXZReachers();
            movingRight = zReached && !xReached;
        }
        if (hasStarted && isAlive)
            SetMovementDirection();
    }
    private void SetMovementDirection()
    {
        if (movingRight)
        {
            rb.velocity = (Vector3.right * _speed) + Physics.gravity;
        }
        else
        {
            rb.velocity = (Vector3.forward * _speed) + Physics.gravity;
        }
    }
    public void ResetPlayer()
    {
        hasStarted = false;
        isAlive = true;
        movingRight = true;
        transform.position = startPos;
        rb.velocity = Vector3.zero;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death"))
        {
            isAlive = false;
            rb.velocity = Physics.gravity;
            UIManager.Instance.ShowGameOver();
            AudioManager.Instance.Play(0);
        }
        else if (other.CompareTag("Crystal"))
        {
            other.gameObject.SetActive(false);
            UIManager.Instance.ShowPlusOneText(other.transform);
            ProjectController.Instance.CurrentCrystalCount++;
            UIManager.Instance.UpdateCurrentCrystalCountText();
            AudioManager.Instance.Play(2);
        }
    }
    public void StopMoving()
    {
        movingAllowed = false;
        lastMoveDirection = rb.velocity;
        rb.velocity = Vector3.zero;
    }
    public void ContinueMoving()
    {
        movingAllowed = false;
        rb.velocity = lastMoveDirection;
    }
    void CalculateXZReachers()
    {
        var res = (nextCornerDestination.position - transform.position);
        if (res.z <= 0 && !zReached)
        {
            zReached = true;
        }
        if (res.x <= 0 && !xReached)
        {
            xReached = true;
        }
    }

    public void OnCornerPassed()
    {
        var res = (nextCornerDestination.position - transform.position);
        zReached = res.z <= 0;
        xReached = res.x <= 0;
    }
}
