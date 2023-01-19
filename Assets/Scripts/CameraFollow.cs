using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform target;
    Vector3 offset;
    void Start()
    {
        target = PlayerMovement.Instance.transform;
        offset = transform.position - target.position;
    }

    void Update()
    {
        transform.position = new Vector3(target.position.x + offset.x,
            offset.y, target.position.z + offset.z);
    }
}
