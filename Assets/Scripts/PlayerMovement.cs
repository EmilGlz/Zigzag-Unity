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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isAlive = true;
    }

    // Update is called once per frame
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
}
