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
    Rigidbody rb;
    public bool hasStarted;
    public bool isAlive;
    public bool movingRight;
    private Vector3 startPos;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isAlive = true;
        startPos = transform.position;
    }
    void Update()
    {
        if (hasStarted && isAlive)
        {
            SetMovementDirection();
        }
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
        }
        else if (other.CompareTag("Crystal"))
        {
            other.gameObject.SetActive(false);
            UIManager.Instance.ShowPlusOneText(other.transform);
        }
    }
}
