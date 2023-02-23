using UnityEngine;

public class PlayerFollower : MonoBehaviour, IUpdateable
{
    Transform target;
    Vector3 offset;

    public int interval => 1;

    void Start()
    {
        target = PlayerMovement.Instance.transform;
        offset = transform.position - target.position;
    }

    void OnEnable()
    {
        Main.Register(this);
    }

    void OnDisable()
    {
        Main.Unregister(this);
    }

    public void Tick()
    {
        transform.position = new Vector3(target.position.x + offset.x,
            offset.y, target.position.z + offset.z);
    }
}
